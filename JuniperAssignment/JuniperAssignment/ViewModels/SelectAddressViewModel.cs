using System.Collections.Generic;
using System.Windows.Input;
using FreshMvvm;
using JuniperAssignment.Models.User;
using JuniperAssignment.Services.User;
using PropertyChanged;
using Xamarin.Forms;

namespace JuniperAssignment.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class SelectAddressViewModel : FreshBasePageModel
    {
        private readonly IUserService _userService;

        public SelectAddressViewModel()
        {
            _userService = FreshIOC.Container.Resolve<IUserService>();

            AddressSelectedCommand = new Command(AddressSelected);
        }

        public Address SelectedAddress { get; set; }
        public List<Address> Addresses { get; set; }
        public ICommand AddressSelectedCommand { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            Addresses = _userService.GetCurrentUser().Addresses;
            SelectedAddress = _userService.GetCurrentUser().Address;
        }

        private void AddressSelected(object param)
        {
            if (param is Address address)
                SelectedAddress = address;

            _userService.SetCurrentAddress(SelectedAddress);
            CoreMethods.PopPageModel(SelectedAddress);
        }
    }
}