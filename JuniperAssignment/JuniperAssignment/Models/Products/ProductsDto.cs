using System.Collections.Generic;
using JuniperAssignment.Models.User;
using PropertyChanged;

namespace JuniperAssignment.Models.Products
{
    [AddINotifyPropertyChangedInterface]
    public class Photo
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    [AddINotifyPropertyChangedInterface]
    public class Prices
    {
        public double customerPrice { get; set; }
        public string formattedCustomerPrice { get; set; }
        public int defaultPrice { get; set; }
        public string formattedDefaultPrice { get; set; }
        public string formattedMsrpPrice { get; set; }
        public int? msrpPrice { get; set; }
    }

    [AddINotifyPropertyChangedInterface]
    public class Product
    {
        public string ProductTaxCode = "31000";
        public int manufacturerId { get; set; }
        public string itemId { get; set; }
        public string itemName { get; set; }
        public double price { get; set; }
        public string formattedPrice { get; set; }
        public Photo photo { get; set; }
        public Photo photoLarge { get; set; }
        public Prices prices { get; set; }
        public string formattedMsrpPrice { get; set; }
        public bool isValidProduct { get; set; }
        public int? msrpPrice { get; set; }
    }

    [AddINotifyPropertyChangedInterface]
    public class Brand
    {
        public int manufacturerId { get; set; }
        public string brandName { get; set; }
        public Photo photo { get; set; }
        public int totalProductCount { get; set; }
        public IList<Product> products { get; set; }
        public IList<object> shownByAgencies { get; set; }
        public bool userHasAccess { get; set; }
        public bool pendingAccess { get; set; }
        public bool isMultiline { get; set; }
        public bool isDirectSeller { get; set; }
        public bool accessRevoked { get; set; }
        public string biography { get; set; }
        public bool isValidBrand { get; set; }
        public int orderMinimum { get; set; }
        public string formattedOrderMinimum { get; set; }
        public Address Address { get; set; }
    }
}