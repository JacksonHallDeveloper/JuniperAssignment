using FreshMvvm;
using JuniperAssignment.Models;
using JuniperAssignment.Services.Brands;
using JuniperAssignment.Services.Cart;
using JuniperAssignment.Services.Core;
using JuniperAssignment.Services.Taxes;
using JuniperAssignment.Services.Taxes.TaxCalculators;
using JuniperAssignment.Services.User;
using JuniperAssignment.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: ExportFont("FontAwesome6BrandsRegular.otf", Alias = "FAB")]
[assembly: ExportFont("FontAwesome6Regular.otf", Alias = "FAR")]
[assembly: ExportFont("FontAwesome6Solid.otf", Alias = "FAS")]

namespace JuniperAssignment
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SetFreshViewModelMapper();
            RegisterServices();
            SetMainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void SetFreshViewModelMapper()
        {
            FreshPageModelResolver.PageModelMapper = new FreshViewModelMapper();
        }

        private void RegisterServices()
        {
            FreshIOC.Container.Register<IJsonConverter, JsonStringConverter>();
            FreshIOC.Container.Register<IRestService, JsonRestService>();

            FreshIOC.Container.Register(new UnitedStatesTaxCalculator(FreshIOC.Container.Resolve<IRestService>()));
            FreshIOC.Container.Register(new EuropeanUnionTaxCalculator(FreshIOC.Container.Resolve<IRestService>()));
            FreshIOC.Container.Register(new FlatRateTaxCalculator());
            FreshIOC.Container.Register<IOrderBuilder, OrderBuilder>();
            FreshIOC.Container.Register<IUserService, UserService>();
            FreshIOC.Container.Register<IProductService, ProductService>();
            FreshIOC.Container.Register<IBrandService, BrandService>();

            FreshIOC.Container.Register<IOrderBuilder, OrderBuilder>().AsMultiInstance();
            FreshIOC.Container.Register<ICartService, SingleBrandCartService>();
            FreshIOC.Container.Register<ITaxCalculator, TaxService>();
        }

        private void SetMainPage()
        {
            var mainPage = FreshPageModelResolver.ResolvePageModel<ProductsViewModel>();
            MainPage = new FreshNavigationContainer(mainPage);
        }
    }
}