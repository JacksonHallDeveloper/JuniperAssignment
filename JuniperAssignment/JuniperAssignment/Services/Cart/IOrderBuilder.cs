using System.Collections.Generic;
using JuniperAssignment.Models.Orders;

namespace JuniperAssignment.Services.Cart
{
    public interface IOrderBuilder : IBuilder<Order>
    {
        public new Order Build();
        public OrderBuilder SetBuyer();
        public OrderBuilder SetShipping(double shipping);
        public OrderBuilder SetSeller(int brandId);
        public OrderBuilder AddProduct(string product, int quantity);
        public OrderBuilder AddProducts(Dictionary<string, int> productQuantities);
        public OrderBuilder New();
    }
}