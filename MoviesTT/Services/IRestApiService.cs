using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesTT.Services
{
    public interface IRestApiService
    {
        Task<T> GetCategory<T>(string category);
        Task<T> GetMovieDetails<T>(int id);
        Task<T> GetMovieCredits<T>(int id);

        

    }
}
