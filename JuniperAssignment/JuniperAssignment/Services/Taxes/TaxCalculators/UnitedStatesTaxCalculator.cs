using System;
using System.Threading.Tasks;
using JuniperAssignment.Models.Orders;
using JuniperAssignment.Models.Taxes.CalculateTaxes;
using JuniperAssignment.Models.Taxes.TaxRate;
using JuniperAssignment.Models.User;
using JuniperAssignment.Services.Core;

namespace JuniperAssignment.Services.Taxes.TaxCalculators
{
    public class UnitedStatesTaxCalculator : ITaxCalculator
    {
        private readonly IRestService _restService;

        public UnitedStatesTaxCalculator(IRestService restService)
        {
            _restService = restService;
            _restService.Client =
                TaxJarHttpClient
                    .Instance; // todo refactor this into JsonRestService, rename jsonRestService as TaxJarRestService, and test taxJarRestService 
        }

        public virtual async Task<double> GetTaxRate(Order order)
        {
            if (string.IsNullOrWhiteSpace(order.to_zip))
                throw new ArgumentNullException(nameof(order.to_zip));

            var taxRateDto = await _restService.GetAsync<TaxRateDto<UnitedStatesTaxRateDto>>(
                $"rates/{order.to_zip}?country={order.to_country}&city={order.to_city}&street={order.to_street}");
            return taxRateDto.Rate.combined_rate;
        }

        public virtual async Task<double> CalculateTaxes(Order order)
        {
            ValidateOrder(order);

            var calculateTaxesDto =
                await _restService
                    .PostAsync<CalculateTaxesDto<UnitedStatesJurisdictionDto, UnitedStatesTaxBreakdownDto>>("taxes",
                        order);
            return calculateTaxesDto.Tax.amount_to_collect;
        }

        private void ValidateOrder(Order order)
        {
            if (order.amount == 0 && (order.line_items is null || order.line_items.Count == 0))
                throw new ArgumentNullException(nameof(order.amount), "amount or line items parameters are required");

            if (string.IsNullOrWhiteSpace(order.to_zip))
                throw new ArgumentNullException(nameof(order.to_zip));

            if (string.IsNullOrWhiteSpace(order.to_country))
                throw new ArgumentNullException(nameof(order.to_country));

            if (string.IsNullOrWhiteSpace(order.to_state))
                throw new ArgumentNullException(nameof(order.to_state));

            if (string.IsNullOrWhiteSpace(order.from_country))
                throw new ArgumentNullException(nameof(order.from_country));

            if (string.IsNullOrWhiteSpace(order.from_state))
                throw new ArgumentNullException(nameof(order.from_state));

            if (string.IsNullOrWhiteSpace(order.from_zip))
                throw new ArgumentNullException(nameof(order.from_zip));

            if (string.IsNullOrWhiteSpace(order.from_city))
                throw new ArgumentNullException(nameof(order.from_city));

            if (string.IsNullOrWhiteSpace(order.from_street))
                throw new ArgumentNullException(nameof(order.from_street));
        }
    }
}