using System.Collections.Generic;
using JuniperAssignment.Models.Products;

namespace JuniperAssignment.Services.Brands
{
    public interface IBrandService
    {
        List<Brand> GetBrands();
        Brand GetBrand(int manufacturerId);
        Product GetProduct(string productId);
        List<Product> GetProducts(IEnumerable<string> productIds);
    }
}