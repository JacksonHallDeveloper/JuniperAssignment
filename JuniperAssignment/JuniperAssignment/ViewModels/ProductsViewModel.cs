using System.Collections.Generic;
using System.Windows.Input;
using FreshMvvm;
using JuniperAssignment.Models.Products;
using JuniperAssignment.Models.User;
using JuniperAssignment.Services.Brands;
using JuniperAssignment.Services.User;
using PropertyChanged;
using Xamarin.Forms;

namespace JuniperAssignment.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ProductsViewModel : FreshBasePageModel
    {
        private readonly IBrandService _brandService;
        private readonly IUserService _userService;

        public ProductsViewModel()
        {
            _userService = FreshIOC.Container.Resolve<IUserService>();
            _brandService = FreshIOC.Container.Resolve<IBrandService>();

            ViewProductDetailCommand = new Command(ViewProductDetail);
            ChangeAddressCommand = new Command(ChangeAddress);
        }

        public List<Brand> Brands { get; set; }
        public User CurrentUser { get; set; }
        public Address SelectedAddress { get; set; }
        public ICommand ChangeAddressCommand { get; set; }
        public ICommand ViewProductDetailCommand { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            Brands = _brandService.GetBrands();
            CurrentUser = _userService.GetCurrentUser();
            SelectedAddress = CurrentUser.Address;
        }

        public override void ReverseInit(object returnedData)
        {
            if (returnedData is Address address)
                SelectedAddress = address;

            base.ReverseInit(returnedData);
        }

        private void ViewProductDetail(object param)
        {
            CoreMethods.PushPageModel<ProductDetailViewModel>(param);
        }

        private void ChangeAddress()
        {
            CoreMethods.PushPageModel<SelectAddressViewModel>();
        }
    }
}