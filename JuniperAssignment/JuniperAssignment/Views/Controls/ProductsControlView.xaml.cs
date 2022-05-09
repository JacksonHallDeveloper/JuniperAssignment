using System.Collections.Generic;
using System.Windows.Input;
using JuniperAssignment.Models.Products;
using PropertyChanged;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JuniperAssignment.Views.Controls
{
    [AddINotifyPropertyChangedInterface]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsControlView : ContentView
    {
        public static readonly BindableProperty ProductsProperty = BindableProperty.Create(nameof(Products),
            typeof(IEnumerable<Product>), typeof(ProductsControlView), defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty TappedCommandProperty = BindableProperty.Create(nameof(TappedCommand),
            typeof(ICommand), typeof(ProductsControlView), defaultBindingMode: BindingMode.TwoWay);

        public ProductsControlView()
        {
            InitializeComponent();
        }

        public IEnumerable<Product> Products
        {
            get => (IEnumerable<Product>)GetValue(ProductsProperty);
            set => SetValue(ProductsProperty, value);
        }

        public ICommand TappedCommand
        {
            get => (ICommand)GetValue(TappedCommandProperty);
            set => SetValue(TappedCommandProperty, value);
        }
    }
}