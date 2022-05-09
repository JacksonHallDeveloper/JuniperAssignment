using System;
using System.Linq;
using System.Threading.Tasks;
using JuniperAssignment.Models.Orders;
using JuniperAssignment.Models.User;

namespace JuniperAssignment.Services.Taxes.TaxCalculators
{
    public class FlatRateTaxCalculator : ITaxCalculator
    {
        private const double FlatTaxRate = 0.5599;

        public virtual Task<double> GetTaxRate(Order order)
        {
            return Task.FromResult(FlatTaxRate);
        }

        public virtual Task<double> CalculateTaxes(Order order)
        {
            return Task.FromResult(order.CalculateSubtotal() * FlatTaxRate);
        }
    }
}