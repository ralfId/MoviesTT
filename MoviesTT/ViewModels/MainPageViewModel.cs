using MoviesTT.Models;
using MoviesTT.Services;
using MoviesTT.Utils;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MoviesTT.Views;

namespace MoviesTT.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        IRestApiService _restApiService;

        public MainPageViewModel(INavigation navigation) : base(navigation)
        {
            _restApiService = DependencyService.Get<IRestApiService>();

            OnSelectedItemCommand = new Command(NavigateTo);

            Init();
        }

        
        public ICommand OnSelectedItemCommand { get; private set; }
        #region Properties

        private ObservableCollection<Movie> _obtopRatedCatg;

        public ObservableCollection<Movie> ObTopRatedCatg
        {
            get { return _obtopRatedCatg; }
            set { SetValue(ref _obtopRatedCatg, value); }
        }


        private ObservableCollection<Movie> _obUpcomingCatg;

        public ObservableCollection<Movie> ObUpcomingCatg
        {
            get { return _obUpcomingCatg; }
            set { SetValue(ref _obUpcomingCatg, value); }
        }

        private ObservableCollection<Movie> _obPupularCatg;

        public ObservableCollection<Movie> ObPupularCatg
        {
            get { return _obPupularCatg; }
            set { SetValue(ref _obPupularCatg, value); }
        }

        private string _title;

        public string title
        {
            get { return _title; }
            set { SetValue(ref _title, value); }
        }

        private double _vote_average;

        public double vote_average
        {
            get { return _vote_average; }
            set { SetValue(ref _vote_average, value); }
        }

        private string _poster_path;

        public string poster_path
        {
            get { return _poster_path; }
            set { SetValue(ref _poster_path, value); }
        }

        private Movie _selectedMovie;

        public Movie SelectedMovie
        {
            get { return _selectedMovie; }
            set {SetValue(ref _selectedMovie, value); }
        }


        #endregion

        public async void Init()
        {
            await GetPupularMovies();
            await GetTopRatedMovies();
            await GetUpcomingMovies();
        }

        public async Task GetPupularMovies()
        {
            var categResp = await _restApiService.GetCategory<Category>(Constants.PopularCategory);

            if (categResp != null)
            {
                ObPupularCatg = new ObservableCollection<Movie>
                    (categResp.results
                    .Take(10)
                    .Select(mov => new Movie() {
                        id= mov.id,
                        title = mov.title,
                        vote_average = mov.vote_average,
                        poster_path = $"{Constants.ImgUrlW200}{mov.poster_path}"
                        }).ToList());
            }
        }

        public async Task GetTopRatedMovies()
        {
            var categResp = await _restApiService.GetCategory<Category>(Constants.TopRatedCategory);

            if (categResp != null)
            {
                ObTopRatedCatg = new ObservableCollection<Movie>
                    (categResp.results
                    .Take(10)
                    .Select(mov => new Movie()
                    {
                        title = mov.title,
                        vote_average = mov.vote_average,
                        poster_path = $"{Constants.ImgUrlW200}{mov.poster_path}"
                    }).ToList());
            }
        }

        public async Task GetUpcomingMovies()
        {
            var categResp = await _restApiService.GetCategory<Category>(Constants.UpcomingCategory);

            if (categResp != null)
            {
                ObUpcomingCatg = new ObservableCollection<Movie>
                    (categResp.results
                    .Take(10)
                    .Select(mov => new Movie()
                    {
                        title = mov.title,
                        vote_average = mov.vote_average,
                        poster_path = $"{Constants.ImgUrlW200}{mov.poster_path}"
                    }).ToList());
            }
        }

       

        private async void NavigateTo()
        {
            try
            {
                if (SelectedMovie != null)
                {
                    await Navigation.PushAsync(new MovieDetails(SelectedMovie.id));
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine( ex.ToString());
            }
        }
    }
}
