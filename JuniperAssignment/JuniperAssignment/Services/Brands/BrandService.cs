using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using JuniperAssignment.Models.Products;
using JuniperAssignment.Models.User;
using JuniperAssignment.Services.Core;
using JuniperAssignment.Views;

namespace JuniperAssignment.Services.Brands
{
    public class BrandService : IBrandService
    {
        private readonly List<Brand> _brands;
        private readonly IJsonConverter _jsonConverter;

        public BrandService(IJsonConverter jsonConverter)
        {
            _jsonConverter = jsonConverter;
            _brands = LoadBrandsFromJson();
            AddBrandAddresses();
        }

        public List<Brand> GetBrands()
        {
            return _brands;
        }

        public Brand GetBrand(int manufacturerId)
        {
            return _brands.Find(b => b.manufacturerId == manufacturerId);
        }

        public Product GetProduct(string productId)
        {
            return _brands.SelectMany(b => b.products)
                .First(p => p.itemId == productId);
        }

        public List<Product> GetProducts(IEnumerable<string> productIds)
        {
            return _brands.SelectMany(b => b.products)
                .Where(p => productIds.Contains(p.itemId))
                .ToList();
        }

        private List<Brand> LoadBrandsFromJson()
        {
            return _jsonConverter.Deserialize<List<Brand>>(ReadJsonFileAsString("Products.json"));
        }

        private string ReadJsonFileAsString(string fileName)
        {
            //this violates SRP
            //todo refactor into IJsonFileReader and pass in as dependency

            var assembly = typeof(ProductsView).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{fileName}");

            if (stream == null) return "";

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        private void AddBrandAddresses()
        {
            _brands[0].Address = new Address
            {
                Street1 = "Franklin Turnpike",
                City = "Allendale",
                State = "NJ",
                Country = "US",
                PostalCode = "07401"
            };

            _brands[1].Address = new Address
            {
                Street1 = "62 Granite Springs Rd",
                City = "Granite Springs",
                State = "NY",
                Country = "US",
                PostalCode = "10527"
            };

            _brands[2].Address = new Address
            {
                Street1 = "C / O Ravintola Fennia",
                City = "Kerava",
                Country = "FI",
                PostalCode = "04200"
            };
        }
    }
}