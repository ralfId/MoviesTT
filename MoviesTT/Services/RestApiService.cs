using MoviesTT.Services;
using MoviesTT.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly:Dependency(typeof(RestApiService))]
namespace MoviesTT.Services
{
    public class RestApiService : IRestApiService
    {
        HttpClient _httpClient;

        public RestApiService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(Constants.BaseUrl)
            };
        }
  

        public async Task<T> GetCategory<T>(string category)
        {
            try
            {
                var rul = $"{category}?api_key={Constants.ApiKey}&language=en-US&page=1";
                var response = await _httpClient.GetAsync(rul); ;
                if (response.IsSuccessStatusCode)
                {
                    var objs = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(objs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error to get category: " + ex.ToString());
            }

            return default(T);
        }

        public async Task<T> GetMovieDetails<T>(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{id}?api_key={Constants.ApiKey}&language=en-US"); ;
                if (response.IsSuccessStatusCode)
                {
                    var objs = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(objs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error to get category: " + ex.ToString());
            }

            return default(T);
        }

        public async Task<T> GetMovieCredits<T>(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{id}/credits?api_key={Constants.ApiKey}&language=en-US"); ;
                if (response.IsSuccessStatusCode)
                {
                    var objs = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(objs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error to get category: " + ex.ToString());
            }

            return default(T);
        }

        public async Task<T> SearchMovie<T>(string search)
        {
            try
            {
                var url = $"https://api.themoviedb.org/3/search/movie?api_key={Constants.ApiKey}&language=en-US&query={search}&page=1";
                var response = await _httpClient.GetAsync(url); ;
                if (response.IsSuccessStatusCode)
                {
                    var objs = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(objs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error to serach a movie: " + ex.ToString());
            }

            return default(T);
        }

        public async Task<T> GetVideo<T>(int movieId)
        {
            try
            {
                var url = $"{movieId}/videos?api_key={Constants.ApiKey}&language=en-US";
                var response = await _httpClient.GetAsync(url); ;
                if (response.IsSuccessStatusCode)
                {
                    var objs = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(objs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error to serach a movie: " + ex.ToString());
            }

            return default(T);
        }
    }
}
