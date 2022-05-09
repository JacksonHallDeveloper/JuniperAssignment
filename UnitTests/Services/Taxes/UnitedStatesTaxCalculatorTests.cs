using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreshMvvm;
using JuniperAssignment.Models.Orders;
using JuniperAssignment.Models.Taxes.CalculateTaxes;
using JuniperAssignment.Models.Taxes.TaxRate;
using JuniperAssignment.Models.User;
using JuniperAssignment.Services.Core;
using JuniperAssignment.Services.Taxes;
using JuniperAssignment.Services.Taxes.TaxCalculators;
using Moq;
using NUnit.Framework;

namespace UnitTests.Services.Taxes
{
    [TestFixture]
    public class UnitedStatesTaxCalculatorTests : TestFixtureBase
    {
        private ITaxCalculator _unitedStatesTaxCalculator;

        private const double TaxRate = .11;
        private const double TaxesTotal = 57.81;

        [SetUp]
        public void Setup()
        {
            _unitedStatesTaxCalculator = new UnitedStatesTaxCalculator(RestService.Object);

            RestService.Setup(restService =>
                    restService.GetAsync<TaxRateDto<UnitedStatesTaxRateDto>>(It.Is<string>(s =>
                        s.StartsWith("rates/"))))
                .Returns(Task.FromResult(CreateTaxRateResponse(TaxRate)));

            RestService.Setup(restService =>
                    restService.PostAsync<CalculateTaxesDto<UnitedStatesJurisdictionDto, UnitedStatesTaxBreakdownDto>>(
                        It.Is<string>(s => s.StartsWith("taxes")), It.IsAny<Order>()))
                .Returns(Task.FromResult(CreateCalculateTaxesResponse(TaxesTotal)));
        }

        [Test]
        public void UnitedStatesTaxCalculator_GetTaxRate_ReturnsTaxRate()
        {
            var address = CreateAddress("US", "84043", "UT", "Lehi", "1153 W 1425 N");
            var order = CreateOrder(address);
            var expectedResource = "rates/84043?country=US&city=Lehi&street=1153 W 1425 N";

            var rate = _unitedStatesTaxCalculator.GetTaxRate(order).Result;

            Assert.That(rate, Is.EqualTo(TaxRate));
            RestService.Verify(
                restService =>
                    restService.GetAsync<TaxRateDto<UnitedStatesTaxRateDto>>(It.Is<string>(s =>
                        s.Equals(expectedResource))), Times.Once);
        }

        [Test]
        public void UnitedStatesTaxCalculator_GetTaxRate_ThrowsArgumentNullExceptionZip()
        {
            var address = CreateAddress("US", "", "UT", "Lehi", "1153 W 1425 N");
            var order = CreateOrder(address);

            var ex = Assert.Throws<AggregateException>(() =>
            {
                var fails = _unitedStatesTaxCalculator.GetTaxRate(order).Result;
            });

            Assert.That(ex.InnerException is ArgumentNullException innerException &&
                        innerException.Message.Contains("to_zip"));
        }

        [Test]
        public void UnitedStatesTaxCalculator_CalculateTaxes_ReturnsTaxRate()
        {
            var address = CreateAddress("US", "84043", "UT", "Lehi", "1153 W 1425 N");
            var sellerAddress = CreateAddress("US", "84043", "UT", "Lehi", "1153 W 1425 N");
            var order = CreateOrder(address, sellerAddress, 10.05);

            var taxes = _unitedStatesTaxCalculator.CalculateTaxes(order).Result;

            Assert.That(taxes, Is.EqualTo(TaxesTotal));
            RestService.Verify(
                restService =>
                    restService.PostAsync<CalculateTaxesDto<UnitedStatesJurisdictionDto, UnitedStatesTaxBreakdownDto>>(
                        "taxes", order), Times.Once);
        }

        [Test]
        public void UnitedStatesTaxCalculator_CalculateTaxes_ThrowsArgumentNullExceptionZipCode()
        {
            var address = CreateAddress("US", "", "UT", "Lehi", "1153 W 1425 N");
            var sellerAddress = CreateAddress("US", "84043", "UT", "Lehi", "1153 W 1425 N");
            var order = CreateOrder(address, sellerAddress, 10.05);

            var ex = Assert.Throws<AggregateException>(() =>
            {
                var fails = _unitedStatesTaxCalculator.CalculateTaxes(order).Result;
            });

            Assert.That(ex.InnerException is ArgumentNullException innerException &&
                        innerException.Message.Contains("to_zip"));
        }

        [Test]
        public void UnitedStatesTaxCalculator_CalculateTaxes_ThrowsArgumentNullExceptionCountry()
        {
            var address = CreateAddress("", "12345", "UT", "Lehi", "1153 W 1425 N");
            var sellerAddress = CreateAddress("US", "84043", "UT", "Lehi", "1153 W 1425 N");
            var order = CreateOrder(address, sellerAddress, 10.05);

            var ex = Assert.Throws<AggregateException>(() =>
            {
                var fails = _unitedStatesTaxCalculator.CalculateTaxes(order).Result;
            });

            Assert.That(ex.InnerException is ArgumentNullException innerException &&
                        innerException.Message.Contains("to_country"));
        }

        [Test]
        public void UnitedStatesTaxCalculator_CalculateTaxes_ThrowsArgumentNullExceptionAmount()
        {
            var address = CreateAddress("US", "12345", "UT", "Lehi", "1153 W 1425 N");
            var sellerAddress = CreateAddress("US", "84043", "UT", "Lehi", "1153 W 1425 N");
            var order = CreateOrder(address, sellerAddress, 0);

            var ex = Assert.Throws<AggregateException>(() =>
            {
                var fails = _unitedStatesTaxCalculator.CalculateTaxes(order).Result;
            });

            Assert.That(ex.InnerException is ArgumentNullException innerException &&
                        innerException.Message.Contains("amount or line items"));
        }

        [Test]
        public void UnitedStatesTaxCalculator_CalculateTaxes_ThrowsArgumentNullExceptionLineItems()
        {
            var address = CreateAddress("US", "12345", "UT", "Lehi", "1153 W 1425 N");
            var sellerAddress = CreateAddress("US", "84043", "UT", "Lehi", "1153 W 1425 N");
            var order = CreateOrder(address, sellerAddress, null);

            var ex = Assert.Throws<AggregateException>(() =>
            {
                var fails = _unitedStatesTaxCalculator.CalculateTaxes(order).Result;
            });

            Assert.That(ex.InnerException is ArgumentNullException innerException &&
                        innerException.Message.Contains("amount or line items"));
        }

        [Test]
        public void UnitedStatesTaxCalculator_CalculateTaxes_ThrowsArgumentNullExceptionState()
        {
            var address = CreateAddress("US", "12345", "", "Lehi", "1153 W 1425 N");
            var sellerAddress = CreateAddress("US", "84043", "UT", "Lehi", "1153 W 1425 N");
            var order = CreateOrder(address, sellerAddress, 10.00);

            var ex = Assert.Throws<AggregateException>(() =>
            {
                var fails = _unitedStatesTaxCalculator.CalculateTaxes(order).Result;
            });

            Assert.That(ex.InnerException is ArgumentNullException innerException &&
                        innerException.Message.Contains("to_state"));
        }

        [Test]
        public void UnitedStatesTaxCalculator_CalculateTaxes_ThrowsArgumentNullExceptionFromCountry()
        {
            var address = CreateAddress("US", "12345", "UT", "Lehi", "1153 W 1425 N");
            var sellerAddress = CreateAddress("", "84043", "UT", "Lehi", "1153 W 1425 N");
            var order = CreateOrder(address, sellerAddress, 10.00);

            var ex = Assert.Throws<AggregateException>(() =>
            {
                var fails = _unitedStatesTaxCalculator.CalculateTaxes(order).Result;
            });

            Assert.That(ex.InnerException is ArgumentNullException innerException &&
                        innerException.Message.Contains("from_country"));
        }

        [Test]
        public void UnitedStatesTaxCalculator_CalculateTaxes_ThrowsArgumentNullExceptionFromState()
        {
            var address = CreateAddress("US", "12345", "UT", "Lehi", "1153 W 1425 N");
            var sellerAddress = CreateAddress("US", "84043", "", "Lehi", "1153 W 1425 N");
            var order = CreateOrder(address, sellerAddress, 10.00);

            var ex = Assert.Throws<AggregateException>(() =>
            {
                var fails = _unitedStatesTaxCalculator.CalculateTaxes(order).Result;
            });

            Assert.That(ex.InnerException is ArgumentNullException innerException &&
                        innerException.Message.Contains("from_state"));
        }

        [Test]
        public void UnitedStatesTaxCalculator_CalculateTaxes_ThrowsArgumentNullExceptionFromZip()
        {
            var address = CreateAddress("US", "12345", "UT", "Lehi", "1153 W 1425 N");
            var sellerAddress = CreateAddress("US", "", "UT", "Lehi", "1153 W 1425 N");
            var order = CreateOrder(address, sellerAddress, 10.00);

            var ex = Assert.Throws<AggregateException>(() =>
            {
                var fails = _unitedStatesTaxCalculator.CalculateTaxes(order).Result;
            });

            Assert.That(ex.InnerException is ArgumentNullException innerException &&
                        innerException.Message.Contains("from_zip"));
        }

        [Test]
        public void UnitedStatesTaxCalculator_CalculateTaxes_ThrowsArgumentNullExceptionFromCity()
        {
            var address = CreateAddress("US", "12345", "UT", "Lehi", "1153 W 1425 N");
            var sellerAddress = CreateAddress("US", "84043", "UT", "", "1153 W 1425 N");
            var order = CreateOrder(address, sellerAddress, 10.00);

            var ex = Assert.Throws<AggregateException>(() =>
            {
                var fails = _unitedStatesTaxCalculator.CalculateTaxes(order).Result;
            });

            Assert.That(ex.InnerException is ArgumentNullException innerException &&
                        innerException.Message.Contains("from_city"));
        }

        [Test]
        public void UnitedStatesTaxCalculator_CalculateTaxes_ThrowsArgumentNullExceptionFromStreet()
        {
            var address = CreateAddress("US", "12345", "UT", "Lehi", "1153 W 1425 N");
            var sellerAddress = CreateAddress("US", "84043", "UT", "Lehi", "");
            var order = CreateOrder(address, sellerAddress, 10.00);

            var ex = Assert.Throws<AggregateException>(() =>
            {
                var fails = _unitedStatesTaxCalculator.CalculateTaxes(order).Result;
            });

            Assert.That(ex.InnerException is ArgumentNullException innerException &&
                        innerException.Message.Contains("from_street"));
        }

        private Address CreateAddress(string country, string zip, string state = "", string city = "",
            string street = "")
        {
            return new Address
            {
                Country = country,
                PostalCode = zip,
                State = state,
                City = city,
                Street1 = street
            };
        }

        private Order CreateOrder(Address userAddress)
        {
            return new Order
            {
                to_zip = userAddress.PostalCode,
                to_country = userAddress.Country,
                to_city = userAddress.City,
                to_street = userAddress.Street1
            };
        }

        private Order CreateOrder(Address userAddress, Address sellerAddress, double amount)
        {
            return new Order
            {
                to_zip = userAddress.PostalCode,
                to_country = userAddress.Country,
                to_city = userAddress.City,
                to_street = userAddress.Street1,
                to_state = userAddress.State,
                from_zip = sellerAddress.PostalCode,
                from_country = sellerAddress.Country,
                from_city = sellerAddress.City,
                from_street = sellerAddress.Street1,
                from_state = sellerAddress.State,
                amount = amount
            };
        }

        private Order CreateOrder(Address userAddress, Address sellerAddress, List<LineItemDto> lineItems)
        {
            return new Order
            {
                to_zip = userAddress.PostalCode,
                to_country = userAddress.Country,
                to_city = userAddress.City,
                to_street = userAddress.Street1,
                from_zip = sellerAddress.PostalCode,
                from_country = sellerAddress.Country,
                from_city = sellerAddress.City,
                from_street = sellerAddress.Street1,
                from_state = sellerAddress.State,
                line_items = lineItems
            };
        }

        private CalculateTaxesDto<UnitedStatesJurisdictionDto, UnitedStatesTaxBreakdownDto>
            CreateCalculateTaxesResponse(double total)
        {
            return new CalculateTaxesDto<UnitedStatesJurisdictionDto, UnitedStatesTaxBreakdownDto>
            {
                Tax = new TaxResponse<UnitedStatesJurisdictionDto, UnitedStatesTaxBreakdownDto>
                {
                    amount_to_collect = total
                }
            };
        }

        private TaxRateDto<UnitedStatesTaxRateDto> CreateTaxRateResponse(double rate)
        {
            return new TaxRateDto<UnitedStatesTaxRateDto>
            {
                Rate = new UnitedStatesTaxRateDto
                {
                    combined_rate = rate
                }
            };
        }
    }
}