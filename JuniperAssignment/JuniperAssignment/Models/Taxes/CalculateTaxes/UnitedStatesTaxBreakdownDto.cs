using System.Collections.Generic;

namespace JuniperAssignment.Models.Taxes.CalculateTaxes
{
    public class UnitedStatesTaxBreakdownDto
    {
        public double taxable_amount { get; set; } //	float	Total amount of the order to be taxed.
        public double tax_collectable { get; set; } //	float	Total amount of sales tax to collect.

        public double
            combined_tax_rate
        {
            get;
            set;
        } //	float	Overall sales tax rate of the breakdown which includes state, county, city and district tax for the order and shipping if applicable.

        public double state_taxable_amount { get; set; } //	float	Amount of the order to be taxed at the state tax rate.
        public double state_tax_rate { get; set; } //	float	State sales tax rate for given location.
        public double state_tax_collectable { get; set; } //	float	Amount of sales tax to collect for the state.

        public double
            county_taxable_amount { get; set; } //	float	Amount of the order to be taxed at the county tax rate.

        public double county_tax_rate { get; set; } //	float	County sales tax rate for given location.
        public double county_tax_collectable { get; set; } //	float	Amount of sales tax to collect for the county.
        public double city_taxable_amount { get; set; } //	float	Amount of the order to be taxed at the city tax rate.
        public double city_tax_rate { get; set; } //	float	City sales tax rate for given location.
        public double city_tax_collectable { get; set; } //	float	Amount of sales tax to collect for the city.

        public double
            special_district_taxable_amount
        {
            get;
            set;
        } //	float	Amount of the order to be taxed at the special district tax rate.

        public double special_tax_rate { get; set; } //	float	Special district sales tax rate for given location.

        public double
            special_district_tax_collectable
        {
            get;
            set;
        } //	float	Amount of sales tax to collect for the special district.

        public UnitedStatesShippingDto ShippingDto { get; set; } //	object	Breakdown of shipping rates if applicable.

        public List<UnitedStatesLineItemDetailDto>
            line_items { get; set; } //	object	Breakdown of rates by line item if applicable.
    }
}