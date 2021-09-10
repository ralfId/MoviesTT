﻿using MoviesTT.Models;
using MoviesTT.Services;
using MoviesTT.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesTT.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {




        //private string _name;

        //public string Name
        //{
        //    get => _name;
        //    set => SetValue(ref _name, value);
        //}


        IRestApiService _restApiService;
        public MainPageViewModel()
        {
            _restApiService = DependencyService.Get<IRestApiService>();

            Init();
        }

        #region Properties
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
        #endregion

        private async void Init()
        {
            await GetPupularMovies();
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
                        title = mov.title,
                        vote_average = mov.vote_average,
                        poster_path = $"{Constants.ImagesBaseUrl}{mov.poster_path}"
                        }).ToList());
            }
        }

    }
}
