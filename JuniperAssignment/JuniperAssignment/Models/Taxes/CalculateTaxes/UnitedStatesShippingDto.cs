namespace JuniperAssignment.Models.Taxes.CalculateTaxes
{
    public class UnitedStatesShippingDto
    {
        public double city_amount { get; set; } // 0.0,
        public double city_tax_rate { get; set; } // 0.0,
        public double city_taxable_amount { get; set; } // 0.0,
        public double combined_tax_rate { get; set; } // 0.06625,
        public double county_amount { get; set; } // 0.0,
        public double county_tax_rate { get; set; } // 0.0,
        public double county_taxable_amount { get; set; } // 0.0,
        public double special_district_amount { get; set; } // 0.0,
        public double special_tax_rate { get; set; } // 0.0,
        public double special_taxable_amount { get; set; } // 0.0,
        public double state_amount { get; set; } // 0.1,
        public double state_sales_tax_rate { get; set; } // 0.06625,
        public double state_taxable_amount { get; set; } // 1.5,
        public double tax_collectable { get; set; } // 0.1,
        public double taxable_amount { get; set; } // 1.5
    }
}