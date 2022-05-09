namespace JuniperAssignment.Models.Taxes.CalculateTaxes
{
    public class EuropeanUnionLineItemDetailDto
    {
        public double combined_tax_rate { get; set; } // 0.24,
        public double country_tax_collectable { get; set; } // 4.07,
        public double country_tax_rate { get; set; } // 0.24,
        public double country_taxable_amount { get; set; } // 16.95,
        public double id { get; set; } // 1,
        public double tax_collectable { get; set; } // 4.07,
        public double taxable_amount { get; set; } // 16.95
    }
}