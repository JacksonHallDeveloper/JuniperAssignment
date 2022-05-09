using System.Threading.Tasks;
using FreshMvvm;
using JuniperAssignment.Models.Orders;
using JuniperAssignment.Models.User;
using JuniperAssignment.Services.Taxes;
using Moq;
using NUnit.Framework;

namespace UnitTests.Services.Taxes
{
    [TestFixture]
    public class TaxServiceTests : TestFixtureBase
    {
        private ITaxCalculator _taxService;

        private const double UnitedStatesRate = 0.1;
        private const double EuropeanUnionRate = 0.2;
        private const double FlatRate = 0.3;

        [SetUp]
        public void Setup()
        {
            _taxService = new TaxService(UserService.Object, UnitedStatesTaxCalculator.Object,
                EuropeanUnionTaxCalculator.Object, FlatRateTaxCalculator.Object);

            UnitedStatesTaxCalculator.Setup(u => u.GetTaxRate(It.IsAny<Order>()))
                .Returns(Task.FromResult(UnitedStatesRate));
            EuropeanUnionTaxCalculator.Setup(e => e.GetTaxRate(It.IsAny<Order>()))
                .Returns(Task.FromResult(EuropeanUnionRate));
            FlatRateTaxCalculator.Setup(f => f.GetTaxRate(It.IsAny<Order>())).Returns(Task.FromResult(FlatRate));

            UnitedStatesTaxCalculator.Setup(u => u.CalculateTaxes(It.IsAny<Order>()))
                .Returns(Task.FromResult(UnitedStatesRate));
            EuropeanUnionTaxCalculator.Setup(e => e.CalculateTaxes(It.IsAny<Order>()))
                .Returns(Task.FromResult(EuropeanUnionRate));
            FlatRateTaxCalculator.Setup(f => f.CalculateTaxes(It.IsAny<Order>())).Returns(Task.FromResult(FlatRate));
        }

        [Test]
        public void TaxService_GetTaxRate_ReturnsUnitedStatesTaxCalculator()
        {
            var user = CreateUser(CreateAddress("NJ", "US"));
            var order = CreateOrder(CreateAddress("NJ", "US"), user.Address);

            UserService.Setup(u => u.GetCurrentUser()).Returns(user);

            var rate = _taxService.GetTaxRate(order).Result;

            Assert.That(rate, Is.EqualTo(UnitedStatesRate));
            UnitedStatesTaxCalculator.Verify(taxCalculator => taxCalculator.GetTaxRate(order), Times.Once);
        }

        [TestCase("AT")]
        [TestCase("BE")]
        [TestCase("BG")]
        [TestCase("HR")]
        [TestCase("CY")]
        [TestCase("CZ")]
        [TestCase("DK")]
        [TestCase("EE")]
        [TestCase("FI")]
        [TestCase("FR")]
        [TestCase("DE")]
        [TestCase("GR")]
        [TestCase("HU")]
        [TestCase("IE")]
        [TestCase("IT")]
        [TestCase("LV")]
        [TestCase("LT")]
        [TestCase("LU")]
        [TestCase("MT")]
        [TestCase("NL")]
        [TestCase("PL")]
        [TestCase("PT")]
        [TestCase("RO")]
        [TestCase("SK")]
        [TestCase("SI")]
        [TestCase("ES")]
        [TestCase("SE")]
        [TestCase("GB")]
        public void TaxService_GetTaxRate_ReturnsEuropeanUnionTaxCalculator(string country)
        {
            var user = CreateUser(CreateAddress("", country));
            var order = CreateOrder(CreateAddress("", country), user.Address);
            UserService.Setup(u => u.GetCurrentUser()).Returns(user);

            var rate = _taxService.GetTaxRate(order).Result;

            Assert.That(rate, Is.EqualTo(EuropeanUnionRate));
            EuropeanUnionTaxCalculator.Verify(taxCalculator => taxCalculator.GetTaxRate(order), Times.Once);
        }

        [TestCase("CA")]
        [TestCase("AU")]
        [TestCase("Some random country code")]
        public void TaxService_GetTaxRate_ReturnsFlatRateTaxCalculator(string country)
        {
            var user = CreateUser(CreateAddress("", country));
            var order = CreateOrder(CreateAddress("", country), user.Address);
            UserService.Setup(u => u.GetCurrentUser()).Returns(user);

            var rate = _taxService.GetTaxRate(order).Result;

            Assert.That(rate, Is.EqualTo(FlatRate));
            FlatRateTaxCalculator.Verify(taxCalculator => taxCalculator.GetTaxRate(order), Times.Once);
        }

        [TestCase("NJ", "US", "UT", "US")]
        [TestCase("NJ", "US", "", "FI")]
        [TestCase("NJ", "FI", "", "FI")]
        public void TaxService_GetTaxRate_DifferentCountryAndStateReturnsFlatRateTaxCalculator(string state1,
            string country1, string state2, string country2)
        {
            var user = CreateUser(CreateAddress(state1, country1));
            var order = CreateOrder(CreateAddress(state2, country2));

            UserService.Setup(u => u.GetCurrentUser()).Returns(user);

            var rate = _taxService.GetTaxRate(order).Result;

            Assert.That(rate, Is.EqualTo(FlatRate));
            FlatRateTaxCalculator.Verify(taxCalculator => taxCalculator.GetTaxRate(order), Times.Once);
        }

        [Test]
        public void TaxService_CalculateTaxes_ReturnsUnitedStatesTaxCalculator()
        {
            var user = CreateUser(CreateAddress("NJ", "US"));
            var order = CreateOrder(CreateAddress("NJ", "US"), user.Address);

            UserService.Setup(u => u.GetCurrentUser()).Returns(user);

            var rate = _taxService.CalculateTaxes(order).Result;

            Assert.That(rate, Is.EqualTo(UnitedStatesRate));
            UnitedStatesTaxCalculator.Verify(taxCalculator => taxCalculator.CalculateTaxes(order), Times.Once);
        }

        [TestCase("AT")]
        [TestCase("BE")]
        [TestCase("BG")]
        [TestCase("HR")]
        [TestCase("CY")]
        [TestCase("CZ")]
        [TestCase("DK")]
        [TestCase("EE")]
        [TestCase("FI")]
        [TestCase("FR")]
        [TestCase("DE")]
        [TestCase("GR")]
        [TestCase("HU")]
        [TestCase("IE")]
        [TestCase("IT")]
        [TestCase("LV")]
        [TestCase("LT")]
        [TestCase("LU")]
        [TestCase("MT")]
        [TestCase("NL")]
        [TestCase("PL")]
        [TestCase("PT")]
        [TestCase("RO")]
        [TestCase("SK")]
        [TestCase("SI")]
        [TestCase("ES")]
        [TestCase("SE")]
        [TestCase("GB")]
        public void TaxService_CalculateTaxes_ReturnsEuropeanUnionTaxCalculator(string country)
        {
            var user = CreateUser(CreateAddress("", country));
            var order = CreateOrder(CreateAddress("", country), user.Address);
            UserService.Setup(u => u.GetCurrentUser()).Returns(user);

            var rate = _taxService.CalculateTaxes(order).Result;

            Assert.That(rate, Is.EqualTo(EuropeanUnionRate));
            EuropeanUnionTaxCalculator.Verify(taxCalculator => taxCalculator.CalculateTaxes(order), Times.Once);
        }

        [TestCase("CA")]
        [TestCase("AU")]
        [TestCase("Some random country code")]
        public void TaxService_CalculateTaxes_ReturnsFlatRateTaxCalculator(string country)
        {
            var user = CreateUser(CreateAddress("", country));
            var order = CreateOrder(user.Address);
            UserService.Setup(u => u.GetCurrentUser()).Returns(user);

            var rate = _taxService.CalculateTaxes(order).Result;

            Assert.That(rate, Is.EqualTo(FlatRate));
            FlatRateTaxCalculator.Verify(taxCalculator => taxCalculator.CalculateTaxes(order), Times.Once);
        }

        [TestCase("NJ", "US", "UT", "US")]
        [TestCase("NJ", "US", "", "FI")]
        [TestCase("NJ", "FI", "", "FI")]
        public void TaxService_CalculateTaxes_DifferentCountryAndStateReturnsFlatRateTaxCalculator(string state1,
            string country1, string state2, string country2)
        {
            var user = CreateUser(CreateAddress(state1, country1));
            var order = CreateOrder(CreateAddress(state2, country2), user.Address);

            UserService.Setup(u => u.GetCurrentUser()).Returns(user);

            var taxes = _taxService.CalculateTaxes(order).Result;

            Assert.That(taxes, Is.EqualTo(FlatRate));
            FlatRateTaxCalculator.Verify(taxCalculator => taxCalculator.CalculateTaxes(order), Times.Once);
        }

        public Order CreateOrder(Address sellerAddress, Address buyerAddress)
        {
            return new Order
            {
                from_state = sellerAddress.State,
                from_country = sellerAddress.Country,
                to_state = buyerAddress.State,
                to_country = buyerAddress.Country
            };
        }

        public Order CreateOrder(Address buyerAddress)
        {
            return new Order
            {
                to_state = buyerAddress.State,
                to_country = buyerAddress.Country
            };
        }

        private User CreateUser(Address address)
        {
            return new User
            {
                Address = address
            };
        }

        private Address CreateAddress(string state, string country)
        {
            return new Address
            {
                State = state,
                Country = country
            };
        }
    }
}