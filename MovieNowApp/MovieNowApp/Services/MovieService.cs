using MovieNowApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieNowApp.Services
{
    public class MovieService
    {
        const string Url = "http://10.0.2.2:5097/api/Movie/";
        Uri uri = new Uri(Url);
        HttpClient client;

        public MovieService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        //GET: /api/Movie
        public async Task<List<Movie>> GetAllMovies()
        {
            List<Movie> movies = new List<Movie>();
            try
            {
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    movies = JsonConvert.DeserializeObject<List<Movie>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return movies;
        }

        //GET: /api/Movie/{id}
        public async Task<Movie> GetMovieById(int id)
        {
            Movie movie = new Movie();
            try
            {
                var response = await client.GetAsync(uri + $"{id}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    movie = JsonConvert.DeserializeObject<Movie>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return movie;
        }

        //GET: /api/Movie/recent
        public async Task<List<Movie>> GetRecentMovies()
        {
            List<Movie> movies = new List<Movie>();
            try
            {
                var response = await client.GetAsync(uri + $"recent");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    movies = JsonConvert.DeserializeObject<List<Movie>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return movies;
        }

        //GET: /api/Movie/filter?title={title}&genre={genre}&year={year}
        public async Task<List<Movie>> FilterMovies(string title, string genre, string year)
        {
            List<Movie> movies = new List<Movie>();
            try
            {
                var response = await client.GetAsync(uri + $"filter?title={title}&genre={genre}&year={year}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    movies = JsonConvert.DeserializeObject<List<Movie>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return movies;
        }


        //GET: /api/Movie/rated/{userId}
        public async Task<List<RatedMovie>> GetRatedMoviesByUser(int userId)
        {
            List<RatedMovie> movies = new List<RatedMovie>();
            try
            {
                var response = await client.GetAsync(uri + $"rated/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    movies = JsonConvert.DeserializeObject<List<RatedMovie>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return movies;
        }
    }
}
