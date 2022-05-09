using System.Collections.Generic;
using JuniperAssignment.Models.Cart;
using JuniperAssignment.Models.Orders;

namespace JuniperAssignment.Services.Cart
{
    public interface ICartService : IBuilder<Order>
    {
        int ItemsInCart { get; }

        int? ManufacturerId { get; }
        void Add(int manufacturerId, string productId, int quantity);
        bool CanAdd(int manufacturerId, string productId);
        void Remove(string productId);
        void Cancel();
    }
}