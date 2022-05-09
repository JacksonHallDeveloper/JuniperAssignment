namespace JuniperAssignment.Models.Taxes.CalculateTaxes
{
    public class NexusAddressDto
    {
        public string id { get; set; } // string	optional	Unique identifier of the given nexus address. View Note
        public string country { get; set; } //	string	conditional	Two-letter ISO country code for the nexus address.
        public string zip { get; set; } //	string	optional	Postal code for the nexus address.
        public string state { get; set; } //string	conditional	Two-letter ISO state code for the nexus address.
        public string city { get; set; } //string	optional	City for the nexus address.
        public string street { get; set; } //	string	optional	Street address for the nexus address.
    }
}