using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using JuniperAssignment.Models.Cart;
using JuniperAssignment.Models.Orders;
using PropertyChanged;
using Xamarin.Forms.Internals;

namespace JuniperAssignment.Services.Cart
{
    [AddINotifyPropertyChangedInterface]
    public class SingleBrandCartService : ICartService
    {
        private readonly IOrderBuilder _orderBuilder;
        private Dictionary<string, int> _productQuantities = new();

        public SingleBrandCartService(IOrderBuilder orderBuilder)
        {
            _orderBuilder = orderBuilder;
        }

        public int? ManufacturerId { get; private set; }
        public int ItemsInCart => _productQuantities?.Values.Sum() ?? 0;

        public void Add(int manufacturerId, string productId, int quantity)
        {
            if (!_productQuantities.ContainsKey(productId))
            {
                AddManufacturer(manufacturerId);
                AddProduct(productId, quantity);
            }
            else
            {
                AddAdditional(productId, quantity);
            }
        }

        public bool CanAdd(int manufacturerId, string productId)
        {
            return !ManufacturerId.HasValue || ManufacturerId.Value == manufacturerId;
        }

        public void Remove(string productId)
        {
            if (_productQuantities.ContainsKey(productId))
                _productQuantities.Remove(productId);
        }

        public void Cancel()
        {
            _productQuantities = new Dictionary<string, int>();
            ManufacturerId = null;
        }

        public Order Build()
        {
            return _orderBuilder
                .New()
                .SetBuyer()
                .SetSeller(ManufacturerId.Value)
                .SetShipping(27.8)
                .AddProducts(_productQuantities)
                .Build();
        }

        private void AddAdditional(string productId, int quantity)
        {
            _productQuantities[productId] += quantity;
        }

        private void AddProduct(string productId, int quantity)
        {
            _productQuantities.Add(productId, quantity);
        }

        private void AddManufacturer(int manufacturerId)
        {
            ManufacturerId ??= manufacturerId;
        }
    }
}