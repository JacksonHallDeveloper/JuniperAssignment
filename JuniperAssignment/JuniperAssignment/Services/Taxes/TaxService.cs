using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JuniperAssignment.Models.Orders;
using JuniperAssignment.Models.Taxes.TaxRate;
using JuniperAssignment.Models.User;
using JuniperAssignment.Services.Taxes.TaxCalculators;
using JuniperAssignment.Services.User;

namespace JuniperAssignment.Services.Taxes
{
    public class TaxService : ITaxCalculator
    {
        private readonly IUserService _userService;
        private readonly FlatRateTaxCalculator _flatRateTaxCalculator;
        private readonly EuropeanUnionTaxCalculator _europeanUnionTaxCalculator;
        private readonly UnitedStatesTaxCalculator _unitedStatesTaxCalculator;


        public TaxService(IUserService userService, UnitedStatesTaxCalculator unitedStatesTaxCalculator,
            EuropeanUnionTaxCalculator europeanUnionTaxCalculator, FlatRateTaxCalculator flatRateTaxCalculator)
        {
            _userService = userService;
            _unitedStatesTaxCalculator = unitedStatesTaxCalculator;
            _europeanUnionTaxCalculator = europeanUnionTaxCalculator;
            _flatRateTaxCalculator = flatRateTaxCalculator;
        }

        public Task<double> GetTaxRate(Order order)
        {
            return GetTaxCalculator(_userService.GetCurrentUser(), order).GetTaxRate(order);
        }

        public Task<double> CalculateTaxes(Order order)
        {
            return GetTaxCalculator(_userService.GetCurrentUser(), order).CalculateTaxes(order);
        }

        private ITaxCalculator GetTaxCalculator(Models.User.User user)
        {
            return user.Address.Country switch
            {
                "US" => _unitedStatesTaxCalculator,
                var x when GetEuropeanUnionCountryCodes().ContainsKey(x) => _europeanUnionTaxCalculator,
                _ => _flatRateTaxCalculator
            };
        }

        private ITaxCalculator GetTaxCalculator(Models.User.User user, Order order)
        {
            //I found that when the to_ and from_ address are in different states, the /taxes api does not return amount_to_Collect
            //Idk if this is the expected behaviour or if i'm missing something. Either way, it gives us a great opportunity to calculate taxes using different methods.

            return InTheSameState(user, order) ? GetTaxCalculator(user) : _flatRateTaxCalculator;
        }

        private static bool InTheSameState(Models.User.User user, Order orderDto)
        {
            return user.Address.Country == orderDto.from_country && user.Address.State == orderDto.from_state;
        }

        private static Dictionary<string, string> GetEuropeanUnionCountryCodes()
        {
            //let's just pretend this is in some service call and not hard coded in the tax service :)
            return new Dictionary<string, string>
            {
                { "AT", "Austria" },
                { "BE", "Belgium" },
                { "BG", "Bulgaria" },
                { "HR", "Croatia" },
                { "CY", "Cyprus" },
                { "CZ", "Czech Republic" },
                { "DK", "Denmark" },
                { "EE", "Estonia" },
                { "FI", "Finland" },
                { "FR", "France" },
                { "DE", "Germany" },
                { "GR", "Greece" },
                { "HU", "Hungary" },
                { "IE", "Ireland, Republic of (EIRE)" },
                { "IT", "Italy" },
                { "LV", "Latvia" },
                { "LT", "Lithuania" },
                { "LU", "Luxembourg" },
                { "MT", "Malta" },
                { "NL", "Netherlands" },
                { "PL", "Poland" },
                { "PT", "Portugal" },
                { "RO", "Romania" },
                { "SK", "Slovakia" },
                { "SI", "Slovenia" },
                { "ES", "Spain" },
                { "SE", "Sweden" },
                { "GB", "United Kingdom" }
            };
        }
    }
}