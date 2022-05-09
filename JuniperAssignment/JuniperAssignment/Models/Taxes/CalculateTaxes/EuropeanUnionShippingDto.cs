namespace JuniperAssignment.Models.Taxes.CalculateTaxes
{
    public class EuropeanUnionShippingDto
    {
        public double combined_tax_rate { get; set; } // 0.24,
        public double country_tax_collectable { get; set; } // 2.4,
        public double country_tax_rate { get; set; } // 0.24,
        public double country_taxable_amount { get; set; } // 10.0,
        public double tax_collectable { get; set; } // 2.4,
        public double taxable_amount { get; set; } // 10.0
    }
}