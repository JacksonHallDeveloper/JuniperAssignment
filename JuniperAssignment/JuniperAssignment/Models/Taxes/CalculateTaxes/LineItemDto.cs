namespace JuniperAssignment.Models.Taxes.CalculateTaxes
{
    public class LineItemDto
    {
        public string id { get; set; } //	string	optional	Unique identifier of the given line item. View Note
        public int quantity { get; set; } //	integer	optional	Quantity for the item.

        public string
            product_tax_code
        {
            get;
            set;
        } //	string	optional	Product tax code for the item. If omitted, the item will remain fully taxable.

        public double unit_price { get; set; } //	float	optional	Unit price for the item.
        public double discount { get; set; } //	float	optional	Total discount (non-unit) for the item.
    }
}