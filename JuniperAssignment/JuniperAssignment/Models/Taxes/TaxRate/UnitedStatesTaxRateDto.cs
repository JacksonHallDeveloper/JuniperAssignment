namespace JuniperAssignment.Models.Taxes.TaxRate
{
    public class UnitedStatesTaxRateDto
    {
        public string city; // "LEHI",
        public string city_rate { get; set; } //": "0.001",
        public double combined_district_rate { get; set; } //": "0.0",
        public double combined_rate { get; set; } //": "0.0725",
        public string country { get; set; } //": "US",
        public double country_rate { get; set; } //": "0.0",
        public string county { get; set; } //": "UTAH",
        public double county_rate { get; set; } //": "0.023",
        public bool freight_taxable { get; set; } //": false,
        public string state { get; set; } //": "UT",
        public double state_rate { get; set; } //": "0.0485",
        public string zip { get; set; } //": "84043"
    }
}