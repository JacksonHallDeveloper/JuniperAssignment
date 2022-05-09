namespace JuniperAssignment.Models.Taxes.TaxRate
{
    public class EuropeanUnionTaxRateDto
    {
        public string country { get; set; } //": "FI",
        public double distance_sale_threshold { get; set; } //": "0.0",
        public bool freight_taxable { get; set; } //": true,
        public string name { get; set; } //": "Finland",
        public double parking_rate { get; set; } //": "0.0",
        public double reduced_rate { get; set; } //": "0.14",
        public double standard_rate { get; set; } //": "0.24",
        public double super_reduced_rate { get; set; } //": "0.1"
    }
}