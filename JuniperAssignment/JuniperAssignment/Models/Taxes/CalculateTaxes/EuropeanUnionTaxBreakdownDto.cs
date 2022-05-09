using System.Collections.Generic;

namespace JuniperAssignment.Models.Taxes.CalculateTaxes
{
    public class EuropeanUnionTaxBreakdownDto
    {
        public double combined_tax_rate { get; set; } // 0.24,
        public double country_tax_collectable { get; set; } // 6.47,
        public double country_tax_rate { get; set; } // 0.24,
        public double country_taxable_amount { get; set; } // 26.95,
        public double tax_collectable { get; set; } // 6.47,
        public double taxable_amount { get; set; } // 26.95

        public List<EuropeanUnionLineItemDetailDto> line_items { get; set; } // 
        public EuropeanUnionShippingDto shipping { get; set; } // 
    }
}