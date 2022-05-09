namespace JuniperAssignment.Models.Taxes.CalculateTaxes
{
    public class CalculateTaxesDto<TJurisdiction, TBreakDown>
    {
        public TaxResponse<TJurisdiction, TBreakDown> Tax { get; set; }
    }

    public class TaxResponse<TJurisdiction, TBreakDown>
    {
        public double order_total_amount { get; set; } //	float	Total amount of the order.
        public double shipping { get; set; } //	float	Total amount of shipping for the order.
        public double taxable_amount { get; set; } //	float	Amount of the order to be taxed.
        public double amount_to_collect { get; set; } //	float	Amount of sales tax to collect.

        public double
            rate { get; set; } //	float	Overall sales tax rate of the order (amount_to_collect ÷ taxable_amount).

        public bool
            has_nexus
        {
            get;
            set;
        } //	bool	Whether or not you have nexus for the order based on an address on file, nexus_addresses parameter, or from_ parameters.

        public bool freight_taxable { get; set; } //	bool	Freight taxability for the order.
        public string tax_source { get; set; } //	string	Origin-based or destination-based sales tax collection.

        public string
            exemption_type
        {
            get;
            set;
        } //	string	Type of exemption for the order: wholesale, government, marketplace, other, or non_exempt. If no customer_id or exemption_type is provided, no exemption_type is returned in the response.

        public TJurisdiction jurisdictions { get; set; } //	object	Jurisdiction names for the order.

        public TBreakDown
            breakdown
        {
            get;
            set;
        } //	object	Breakdown of rates by jurisdiction for the order, shipping, and individual line items. If has_nexus is false or no line items are provided, no breakdown is returned in the response.
    }
}