using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FreshMvvm;
using JuniperAssignment.Models.Cart;
using JuniperAssignment.Models.Orders;
using JuniperAssignment.Models.Products;
using JuniperAssignment.Services.Brands;
using JuniperAssignment.Services.Cart;
using JuniperAssignment.Services.Taxes;
using PropertyChanged;
using Xamarin.Forms;

namespace JuniperAssignment.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CheckOutViewModel : FreshBasePageModel
    {
        private readonly ICartService _cartService;
        private readonly ITaxCalculator _taxCalculator;
        private readonly IBrandService _brandService;

        public CheckOutViewModel()
        {
            _taxCalculator = FreshIOC.Container.Resolve<ITaxCalculator>();
            _cartService = FreshIOC.Container.Resolve<ICartService>();
            _brandService = FreshIOC.Container.Resolve<IBrandService>();

            ContinueToPaymentCommand = new Command(ContinueToPayment);
        }

        public Order Order { get; set; }
        public Brand Brand { get; set; }

        public List<CartItem> ItemsInCart { get; set; }
        public double TaxRate { get; set; }
        public double OrderTaxes { get; set; }

        public double Subtotal => Order?.CalculateSubtotal() ?? 0.0;
        public double Total => Subtotal + Order?.shipping + OrderTaxes ?? 0.0;

        public ICommand ContinueToPaymentCommand { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            Order = GetOrder();
            Brand = GetBrand(_cartService.ManufacturerId);
            ItemsInCart = GetItemsInCart(Order);

            Task.Run(async () => await LoadTaxes(Order));
        }

        private Brand GetBrand(int? manufacturerId)
        {
            return _brandService.GetBrand(manufacturerId.Value);
        }

        private async Task LoadTaxes(Order order)
        {
            TaxRate = await GetTaxRate(order);
            OrderTaxes = await CalculateOrderTaxes(order);
        }

        private async Task<double> GetTaxRate(Order order)
        {
            return await _taxCalculator.GetTaxRate(order);
        }

        private Order GetOrder()
        {
            return _cartService.Build();
        }

        private async Task<double> CalculateOrderTaxes(Order order)
        {
            return await _taxCalculator.CalculateTaxes(order);
        }

        private List<CartItem> GetItemsInCart(Order order)
        {
            var products = _brandService.GetProducts(order.line_items.Select(l => l.id));
            var productsDictionary = new Dictionary<string, Product>(products.ToDictionary(p => p.itemId));

            return order.line_items.Select(l => new CartItem
            {
                id = l.id,
                image = productsDictionary[l.id].photo.url,
                name = productsDictionary[l.id].itemName,
                price = l.unit_price,
                quantity = l.quantity
            }).ToList();
        }

        private void ContinueToPayment()
        {
            //thank you for your order, now i can put gas in my truck.
        }
    }
}