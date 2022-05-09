using FreshMvvm;
using JuniperAssignment.Services.Brands;
using JuniperAssignment.Services.Cart;
using JuniperAssignment.Services.Core;
using JuniperAssignment.Services.Taxes;
using JuniperAssignment.Services.Taxes.TaxCalculators;
using JuniperAssignment.Services.User;
using Moq;
using NUnit.Framework;

namespace UnitTests.Services
{
    [TestFixture]
    public class TestFixtureBase
    {
        protected Mock<IJsonConverter> JsonConverter;
        protected Mock<IRestService> RestService;
        protected Mock<UnitedStatesTaxCalculator> UnitedStatesTaxCalculator;
        protected Mock<EuropeanUnionTaxCalculator> EuropeanUnionTaxCalculator;
        protected Mock<FlatRateTaxCalculator> FlatRateTaxCalculator;
        protected Mock<IUserService> UserService;
        protected Mock<IProductService> ProductService;
        protected Mock<IBrandService> BrandService;
        protected Mock<IOrderBuilder> OrderBuilder;
        protected Mock<ICartService> SingleBrandCartService;
        protected Mock<ITaxCalculator> TaxService;

        [SetUp]
        public void BaseSetUp()
        {
            JsonConverter = new Mock<IJsonConverter>();
            RestService = new Mock<IRestService>();

            FreshIOC.Container.Register(JsonConverter.Object);
            FreshIOC.Container.Register(RestService.Object);

            UnitedStatesTaxCalculator = new Mock<UnitedStatesTaxCalculator>(RestService.Object);
            EuropeanUnionTaxCalculator = new Mock<EuropeanUnionTaxCalculator>(RestService.Object);
            FlatRateTaxCalculator = new Mock<FlatRateTaxCalculator>();
            UserService = new Mock<IUserService>();
            ProductService = new Mock<IProductService>();
            BrandService = new Mock<IBrandService>();
            OrderBuilder = new Mock<IOrderBuilder>();
            SingleBrandCartService = new Mock<ICartService>();
            TaxService = new Mock<ITaxCalculator>();


            FreshIOC.Container.Register(UnitedStatesTaxCalculator.Object);
            FreshIOC.Container.Register(EuropeanUnionTaxCalculator.Object);

            FreshIOC.Container.Register(FlatRateTaxCalculator.Object);

            FreshIOC.Container.Register(UserService.Object);
            FreshIOC.Container.Register(ProductService.Object);
            FreshIOC.Container.Register(BrandService.Object);

            FreshIOC.Container.Register(OrderBuilder.Object);
            FreshIOC.Container.Register(SingleBrandCartService.Object);
            FreshIOC.Container.Register(TaxService.Object);
        }

        [TearDown]
        public void BaseTearDown()
        {
            JsonConverter.Reset();
            RestService.Reset();
            UnitedStatesTaxCalculator.Reset();
            EuropeanUnionTaxCalculator.Reset();
            FlatRateTaxCalculator.Reset();
            UserService.Reset();
            ProductService.Reset();
            BrandService.Reset();
            OrderBuilder.Reset();
            SingleBrandCartService.Reset();
            TaxService.Reset();

            FreshIOC.Container.Unregister<IJsonConverter>();
            FreshIOC.Container.Unregister<IRestService>();
            FreshIOC.Container.Unregister<UnitedStatesTaxCalculator>();
            FreshIOC.Container.Unregister<EuropeanUnionTaxCalculator>();
            FreshIOC.Container.Unregister<FlatRateTaxCalculator>();
            FreshIOC.Container.Unregister<IUserService>();
            FreshIOC.Container.Unregister<IProductService>();
            FreshIOC.Container.Unregister<IBrandService>();
            FreshIOC.Container.Unregister<IOrderBuilder>();
            FreshIOC.Container.Unregister<ICartService>();
            FreshIOC.Container.Unregister<ITaxCalculator>();
        }
    }
}