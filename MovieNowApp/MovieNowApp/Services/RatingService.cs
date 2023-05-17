using MovieNowApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieNowApp.Services
{
    public class RatingService
    {
        const string Url = "http://10.0.2.2:5097/api/Rating/";
        Uri uri = new Uri(Url);
        HttpClient client;

        public RatingService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        //GET: /api/Rating
        public async Task<List<Rating>> GetAllRatings()
        {
            List<Rating> ratings = new List<Rating>();
            try
            {
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    ratings = JsonConvert.DeserializeObject<List<Rating>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return ratings;
        }

        //GET: /api/Rating/{id}
        public async Task<Rating> GetRatingById(int id)
        {
            Rating rating = new Rating();
            try
            {
                var response = await client.GetAsync(uri + $"{id}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    rating = JsonConvert.DeserializeObject<Rating>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return rating;
        }

        //GET: /api/Rating/movieId/{id}
        public async Task<List<Rating>> GetRatingsByMovieId(int id)
        {
            List<Rating> ratings = new List<Rating>();
            try
            {
                var response = await client.GetAsync(uri + $"movieId/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    ratings = JsonConvert.DeserializeObject<List<Rating>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return ratings;
        }

        //GET: /api/Rating/userId/{id}
        public async Task<List<Rating>> GetRatingsByUserId(int id)
        {
            List<Rating> ratings = new List<Rating>();
            try
            {
                var response = await client.GetAsync(uri + $"userId/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    ratings = JsonConvert.DeserializeObject<List<Rating>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return ratings;
        }

        //POST: /api/Rating
        public async Task<HttpStatusCode> CreateRating(Rating rating)
        {
            string json = JsonConvert.SerializeObject(rating);
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
                return HttpStatusCode.BadRequest;
            }
            return HttpStatusCode.Created;
        }

        //PUT: /api/Rating
        public async Task<HttpStatusCode> UpdateRating(Rating rating)
        {
            string json = JsonConvert.SerializeObject(rating);
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, uri);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
                return HttpStatusCode.BadRequest;
            }
            return HttpStatusCode.OK;
        }

        //DELETE: /api/Rating/{id}
        public async Task<HttpStatusCode> DeleteRating(int id)
        {
            try
            {
                var response = await client.DeleteAsync(uri + $"{id}");

                if (response.IsSuccessStatusCode)
                {
                    return HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
                return HttpStatusCode.BadRequest;
            }
            return HttpStatusCode.BadRequest;
        }
    }
}
