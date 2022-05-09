using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using FreshMvvm;
using JuniperAssignment.Models.Products;
using JuniperAssignment.Services.Brands;
using JuniperAssignment.Services.Cart;
using PropertyChanged;
using Xamarin.Forms;

namespace JuniperAssignment.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ProductDetailViewModel : FreshBasePageModel
    {
        private const int QuantityMin = 1;
        private const int QuantityMax = 100;

        private readonly IBrandService _brandService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public ProductDetailViewModel()
        {
            _brandService = FreshIOC.Container.Resolve<IBrandService>();
            _productService = FreshIOC.Container.Resolve<IProductService>();
            _cartService = FreshIOC.Container.Resolve<ICartService>();

            ViewProductDetailCommand = new Command(ViewProductDetail);
            ViewCartCommand = new Command(ViewCart);
            AddToCardCommand = new Command(AddToCart);
        }

        public Product ProductDetail { get; set; }
        public Brand Brand { get; set; }
        public List<Product> RelatedProducts { get; set; }
        public int SelectedQuantity { get; set; }
        public List<int> QuantityOptions { get; set; }
        public bool ShowCart { get; set; }
        public bool ShowAddToCart { get; set; }
        public ICommand ViewProductDetailCommand { get; set; }
        public ICommand ViewCartCommand { get; set; }
        public ICommand AddToCardCommand { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            if (initData is not Product product) return;

            ProductDetail = product;
            Brand = GetBrand(product.manufacturerId);
            RelatedProducts = GetRelatedProducts(Brand, product.itemId);
            QuantityOptions = GetQuantityList(QuantityMin, QuantityMax);
            SelectedQuantity = QuantityMin;
            ShowCart = ShouldShowCart(_cartService.ItemsInCart);
            ShowAddToCart = CanAddToCart(Brand.manufacturerId, ProductDetail.itemId);
        }

        private void ViewProductDetail(object param)
        {
            CoreMethods.PushPageModel<ProductDetailViewModel>(param);
        }

        private Brand GetBrand(int manufacturerId)
        {
            return _brandService.GetBrand(manufacturerId);
        }

        private List<Product> GetRelatedProducts(Brand brand, string productId)
        {
            return _productService.GetRelatedProducts(brand, productId);
        }

        private List<int> GetQuantityList(int min, int max)
        {
            return Enumerable.Range(min, max).ToList();
        }

        private void ViewCart()
        {
            CoreMethods.PushPageModel<CheckOutViewModel>();
        }

        private void AddToCart()
        {
            _cartService.Add(Brand.manufacturerId, ProductDetail.itemId, SelectedQuantity);
            ShowCart = ShouldShowCart(_cartService.ItemsInCart);
        }

        private bool CanAddToCart(int manufacturerId, string productId)
        {
            return _cartService.CanAdd(manufacturerId, productId);
        }

        private bool ShouldShowCart(int items)
        {
            return items > 0;
        }
    }
}