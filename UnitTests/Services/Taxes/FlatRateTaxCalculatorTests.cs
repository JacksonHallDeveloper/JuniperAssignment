using System.Collections.Generic;
using JuniperAssignment.Models.Orders;
using JuniperAssignment.Models.Taxes.CalculateTaxes;
using JuniperAssignment.Services.Taxes;
using JuniperAssignment.Services.Taxes.TaxCalculators;
using Moq;
using NUnit.Framework;

namespace UnitTests.Services.Taxes
{
    [TestFixture]
    public class FlatRateTaxCalculatorTests : TestFixtureBase
    {
        private ITaxCalculator _flatTaxRateCalculator;

        private const double TaxRate = 0.5599;

        [SetUp]
        public void Setup()
        {
            _flatTaxRateCalculator = new FlatRateTaxCalculator();
        }

        [Test]
        public void FlatRateTaxCalculator_GetTaxRate_ReturnsFlatTaxRate()
        {
            var rate = _flatTaxRateCalculator.GetTaxRate(It.IsAny<Order>()).Result;
            Assert.That(rate, Is.EqualTo(TaxRate));
        }

        [Test]
        public void FlatRateTaxCalculator_CalculateTaxes_ReturnsOrderAmountTaxes()
        {
            var order = CreateOrder(10.00, 20.00);
            var taxes = _flatTaxRateCalculator.CalculateTaxes(order).Result;
            Assert.That(taxes, Is.EqualTo(30.0 * TaxRate));
        }

        [Test]
        public void FlatRateTaxCalculator_CalculateTaxes_ReturnsOrderLineItemsTaxes()
        {
            var lineItems = new List<LineItemDto>
            {
                CreateLineItem(10.0, 5),
                CreateLineItem(100.0, 2)
            };
            var order = CreateOrder(lineItems, 10.0);
            var taxes = _flatTaxRateCalculator.CalculateTaxes(order).Result;
            Assert.That(taxes, Is.EqualTo(260.0 * TaxRate));
        }

        private Order CreateOrder(double amount, double shipping)
        {
            return new Order
            {
                amount = amount,
                shipping = shipping
            };
        }

        private Order CreateOrder(List<LineItemDto> lineItems, double shipping)
        {
            return new Order
            {
                line_items = lineItems,
                shipping = shipping
            };
        }

        private LineItemDto CreateLineItem(double price, int quantity)
        {
            return new LineItemDto
            {
                unit_price = price,
                quantity = quantity
            };
        }
    }
}