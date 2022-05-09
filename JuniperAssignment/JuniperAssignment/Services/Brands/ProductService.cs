using System.Collections.Generic;
using System.Linq;
using JuniperAssignment.Models.Products;

namespace JuniperAssignment.Services.Brands
{
    public class ProductService : IProductService
    {
        public List<Product> GetRelatedProducts(Brand brand, string productId)
        {
            return brand.products.Where(p => p.itemId != productId).ToList();
        }
    }
}