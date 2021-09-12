using MoviesTT.Models;
using MoviesTT.Services;
using MoviesTT.Utils;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MoviesTT.Views;
using System;
using System.Collections.Generic;

namespace MoviesTT.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        IRestApiService _restApiService;

        List<Movie> PopularLst;
        List<Movie> TopRatedLst;
        List<Movie> UpComingLst;
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

        private string _movieTitle;

        public string MovieTitle
        {
            get { return _movieTitle; }
            set {SetValue(ref _movieTitle, value);
                if (value.Length >= 3)
                {
                    SearchTheMovie();
                }
            }
        }


        private string _emptyViewMessage = "No results found";

        public string EmptyViewMessage
        {
            get { return _emptyViewMessage; }
            set { SetValue(ref _emptyViewMessage, value); }
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
                PopularLst = categResp.results
                    .Take(10)
                    .Select(mov => new Movie()
                    {
                        id = mov.id,
                        title = mov.title,
                        vote_average = mov.vote_average,
                        poster_path = $"{Constants.ImgUrlW200}{mov.poster_path}"
                    }).ToList();
                ObPupularCatg = new ObservableCollection<Movie>(PopularLst);
            }
        }

        public async Task GetTopRatedMovies()
        {
            var categResp = await _restApiService.GetCategory<Category>(Constants.TopRatedCategory);

            if (categResp != null)
            {
                TopRatedLst = categResp.results
                    .Take(10)
                    .Select(mov => new Movie()
                    {
                        title = mov.title,
                        vote_average = mov.vote_average,
                        poster_path = $"{Constants.ImgUrlW200}{mov.poster_path}"
                    }).ToList();

                ObTopRatedCatg = new ObservableCollection<Movie>(TopRatedLst);
            }
        }

        public async Task GetUpcomingMovies()
        {
            var categResp = await _restApiService.GetCategory<Category>(Constants.UpcomingCategory);

            if (categResp != null)
            {
                UpComingLst = categResp.results
                    .Take(10)
                    .Select(mov => new Movie()
                    {
                        title = mov.title,
                        vote_average = mov.vote_average,
                        poster_path = $"{Constants.ImgUrlW200}{mov.poster_path}"
                    }).ToList();

                ObUpcomingCatg = new ObservableCollection<Movie>(TopRatedLst);
            }
        }

        private void SearchTheMovie()
        {
            var filterPopular = PopularLst.Where(mov => mov.title.ToLower().Contains(MovieTitle.ToLower()));
            ObPupularCatg = new ObservableCollection<Movie>(filterPopular);

            var filterTopRated = TopRatedLst.Where(mov => mov.title.ToLower().Contains(MovieTitle.ToLower()));
            ObTopRatedCatg = new ObservableCollection<Movie>(filterTopRated);

            var filterUpcoming = UpComingLst.Where(mov => mov.title.ToLower().Contains(MovieTitle.ToLower()));
            ObUpcomingCatg = new ObservableCollection<Movie>(filterUpcoming);
        }

        private async void NavigateTo()
        {
            try
            {
                if (SelectedMovie != null)
                {
                    await Navigation.PushAsync(new MovieDetails(SelectedMovie.id));
                    SelectedMovie = null;
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine( ex.ToString());
            }
        }
    }
}
