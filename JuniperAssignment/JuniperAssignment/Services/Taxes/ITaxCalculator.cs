using System.Threading.Tasks;
using JuniperAssignment.Models.Orders;
using JuniperAssignment.Models.User;

namespace JuniperAssignment.Services.Taxes
{
    public interface ITaxCalculator
    {
        Task<double> GetTaxRate(Order order);
        Task<double> CalculateTaxes(Order order);
    }
}