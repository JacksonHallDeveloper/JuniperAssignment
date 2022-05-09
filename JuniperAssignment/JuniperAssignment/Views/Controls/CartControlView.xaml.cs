using System.Windows.Input;
using PropertyChanged;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JuniperAssignment.Views.Controls
{
    [AddINotifyPropertyChangedInterface]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartControlView : ContentView
    {
        public static readonly BindableProperty TappedCommandProperty = BindableProperty.Create(nameof(TappedCommand),
            typeof(ICommand), typeof(CartControlView), defaultBindingMode: BindingMode.TwoWay);

        public CartControlView()
        {
            InitializeComponent();
        }

        public ICommand TappedCommand
        {
            get => (ICommand)GetValue(TappedCommandProperty);
            set => SetValue(TappedCommandProperty, value);
        }
    }
}