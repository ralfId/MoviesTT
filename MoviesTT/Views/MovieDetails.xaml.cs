using MoviesTT.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoviesTT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetails : ContentPage
    {
     
        public MovieDetails(int id)
        {
            InitializeComponent();
            BindingContext = new MovieDetailsViewModel(Navigation, id);
        }
    }
}