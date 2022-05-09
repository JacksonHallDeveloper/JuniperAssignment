using System.Collections.Generic;
using JuniperAssignment.Models.Products;

namespace JuniperAssignment.Services.Brands
{
    public interface IProductService
    {
        List<Product> GetRelatedProducts(Brand brand, string productId);
    }
}