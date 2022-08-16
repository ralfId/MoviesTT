using MoviesTT.Models;
using MoviesTT.Services;
using MoviesTT.Utils;
using MoviesTT.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MoviesTT.ViewModels
{
    public class MovieDetailsViewModel : BaseViewModel
    {
        IRestApiService _restApiService;
        List<Cast> castsList = new List<Cast>();

        public MovieDetailsViewModel(INavigation navigation, int id) : base(navigation)
        {
            _restApiService = DependencyService.Get<IRestApiService>();

            MovieID = id;
            Init();

            viewTrilerCommand = new Command(GetTrailer);
            GoBackCommand = new Command(GoBack);
        }

        public int MovieID { get; set; }
        public ICommand GoBackCommand { get; private set; }
        public ICommand viewTrilerCommand { get; private set; }


        private ObservableCollection<Cast> _obActorsCast;

        public ObservableCollection<Cast> ObActorsCast
        {
            get { return _obActorsCast; }
            set { SetValue(ref _obActorsCast, value); }
        }

        private MovieDetail _movieD;

        public MovieDetail MovieD
        {
            get { return _movieD; }
            set { SetValue(ref _movieD, value); }
        }

        private string _profile_path;
        public string profile_path
        {
            get { return _profile_path; ; }
            set { SetValue(ref _profile_path, value); }
        }

        private string _name;

        public string name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }


        private ProductionCompany _prodCompany;

        public ProductionCompany ProdCompany
        {
            get { return _prodCompany; }
            set { SetValue(ref _prodCompany, value); }
        }

        private string _genrList;

        public string GenrList
        {
            get { return _genrList; }
            set { SetValue(ref _genrList, value); }
        }



        public async void Init()
        {
            await LoadMovieDetail();
            await LoadMovieCredits();
        }

        private async Task LoadMovieCredits()
        {
            try
            {
                var movCred = await _restApiService.GetMovieCredits<MovieCredits>(MovieID);
                if (movCred != null)
                {
                    //ACTORS-CAST

                    castsList = movCred.cast.Take(10)
                        .Where(p => p.profile_path != null)
                        .Select(actor => new Cast()
                        {
                            name = actor.name,
                            profile_path = $"{Constants.ImgUrlW200}{actor.profile_path}"
                        }).ToList();

                    ObActorsCast = new ObservableCollection<Cast>(castsList);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async Task LoadMovieDetail()
        {

            try
            {
                var movDetail = await _restApiService.GetMovieDetails<MovieDetail>(MovieID);

                if (movDetail != null)
                {
                    MovieD = new MovieDetail()
                    {
                        title = movDetail.title,
                        vote_average = movDetail.vote_average,
                        overview = movDetail.overview,
                        poster_path = $"{Constants.ImgUrlOriginal}{movDetail.poster_path}",
                        release_date = DateTime.Parse(movDetail.release_date).Year.ToString()

                    };

                    ProdCompany = movDetail.production_companies.FirstOrDefault();
                    var genList = movDetail.genres.Select(g => g.name).ToList();
                    GenrList = string.Join(", ", genList);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async void GoBack()
        {
            await Navigation.PopToRootAsync();
        }

        private async void GetTrailer()
        {
            try
            {
                var videos = await _restApiService.GetVideo<Video>(MovieID);

                if (videos != null && videos.results.Count > 0)
                {
                    var trailer = videos.results.Where(v =>  v.type == Constants.Trailer || v.type == Constants.Clip || v.type == Constants.Teaser && v.site == Constants.Youtube).FirstOrDefault();

                    var url = $"{Constants.YoutubeUrl}{trailer.key}";

                    await Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("No trailer", "This movie it not has a trailer", "Ok");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}