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
    public class RecommendationService
    {
        const string Url = "http://10.0.2.2:5097/api/Recommendation/";
        Uri uri = new Uri(Url);
        HttpClient client;

        public RecommendationService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        //GET: /api/Recommendation/{userId}
        public async Task<List<Movie>> GetRecommendationsByUserId(int userId)
        {
            TrainMLModel();
            List<Movie> movies = new List<Movie>();
            try
            {
                var response = await client.GetAsync(uri + $"{userId}");

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

        //GET: /api/Recommendation/train
        public async Task TrainMLModel()
        {
            try
            {
                var response = await client.GetAsync(uri + $"train");

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("\tSuccess!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
        }
    }
}
