using System;
using System.Collections.Generic;
using System.Linq;
using JuniperAssignment.Models.Taxes.CalculateTaxes;
using PropertyChanged;

namespace JuniperAssignment.Models.Orders
{
    [AddINotifyPropertyChangedInterface]
    public class Order
    {
        public string
            from_country
        {
            get;
            set;
        } //	string	optional	Two-letter ISO country code of the country where the order shipped from. View Note

        public string
            from_zip { get; set; } //	string	optional	Postal code where the order shipped from (5-Digit ZIP or ZIP+4).

        public string
            from_state { get; set; } //	string	optional	Two-letter ISO state code where the order shipped from.

        public string from_city { get; set; } //	string	optional	City where the order shipped from.
        public string from_street { get; set; } //	string	optional	Street address where the order shipped from.

        public string
            to_country
        {
            get;
            set;
        } //	string	required	Two-letter ISO country code of the country where the order shipped to.

        public string
            to_zip { get; set; } //	string	conditional	Postal code where the order shipped to (5-Digit ZIP or ZIP+4).

        public string to_state { get; set; } //	string	conditional	Two-letter ISO state code where the order shipped to.
        public string to_city { get; set; } //	string	optional	City where the order shipped to.
        public string to_street { get; set; } //	string	optional	Street address where the order shipped to. View Note
        public double amount { get; set; } //	float	optional	Total amount of the order, excluding shipping. View Note
        public double shipping { get; set; } //	float	required	Total amount of shipping for the order.

        public string
            customer_id { get; set; } //	string	optional	Unique identifier of the given customer for exemptions.

        public string
            exemption_type
        {
            get;
            set;
        } //	string	optional	Type of exemption for the order: wholesale, government, marketplace, other, or non_exempt. View Note

        public List<NexusAddressDto> nexus_addresses { get; set; }
        public List<LineItemDto> line_items { get; set; }

        public double CalculateSubtotal()
        {
            var lineItemAmount = line_items?.Sum(l => l.unit_price * l.quantity) ?? 0;
            return Math.Max(amount, lineItemAmount) + shipping;
        }
    }
}