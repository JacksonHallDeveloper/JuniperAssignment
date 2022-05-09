using System.Collections.Generic;
using JuniperAssignment.Models.Orders;
using JuniperAssignment.Models.Products;
using JuniperAssignment.Models.Taxes.CalculateTaxes;
using JuniperAssignment.Models.User;
using JuniperAssignment.Services.Brands;
using JuniperAssignment.Services.User;
using PropertyChanged;
using Xamarin.Forms.Internals;

namespace JuniperAssignment.Services.Cart
{
    [AddINotifyPropertyChangedInterface]
    public class OrderBuilder : IOrderBuilder
    {
        private readonly IBrandService _brandService;
        private readonly IUserService _userService;
        private Order _order;

        public OrderBuilder(IBrandService brandService, IUserService userService)
        {
            _brandService = brandService;
            _userService = userService;

            CreateNewOrder();
        }

        public Order Build()
        {
            return _order;
        }

        public OrderBuilder SetBuyer()
        {
            UpdateBuyerAddress(_userService.GetCurrentUser().Address);
            return this;
        }

        public OrderBuilder SetShipping(double shipping)
        {
            _order.shipping = shipping;
            return this;
        }

        public OrderBuilder SetSeller(int brandId)
        {
            UpdateSellerAddress(_brandService.GetBrand(brandId).Address);
            return this;
        }

        public OrderBuilder AddProduct(string productId, int quantity)
        {
            AddItem(CreateLineItem(_brandService.GetProduct(productId), quantity));
            return this;
        }

        public OrderBuilder AddProducts(Dictionary<string, int> productQuantities)
        {
            productQuantities.ForEach(pair => AddProduct(pair.Key, pair.Value));
            return this;
        }

        public OrderBuilder New()
        {
            CreateNewOrder();
            return this;
        }

        private LineItemDto CreateLineItem(Product product, int quantity)
        {
            return new LineItemDto
            {
                id = product.itemId,
                quantity = quantity,
                unit_price = product.price,
                product_tax_code = product.ProductTaxCode
            };
        }

        private void UpdateBuyerAddress(Address address)
        {
            _order.to_country = address.Country;
            _order.to_city = address.City;
            _order.to_state = address.State;
            _order.to_zip = address.PostalCode;
            _order.to_street = address.Street1;
        }

        public void UpdateSellerAddress(Address address)
        {
            _order.from_country = address.Country;
            _order.from_city = address.City;
            _order.from_state = address.State;
            _order.from_zip = address.PostalCode;
            _order.from_street = address.Street1;
        }

        private void AddItem(LineItemDto itemDto)
        {
            _order.line_items.Add(itemDto);
        }

        private void CreateNewOrder()
        {
            _order = new Order
            {
                line_items = new List<LineItemDto>()
            };
        }
    }
}