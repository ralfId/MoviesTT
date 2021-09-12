using MoviesTT.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoviesTT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
      
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(Navigation);
        }
    }
}