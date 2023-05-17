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
    public class UserService
    {
        const string Url = "http://10.0.2.2:5097/api/User/";
        Uri uri = new Uri(Url);
        HttpClient client;

        public UserService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        //GET: /api/User/{id}
        public async Task<User> GetUserById(int id)
        {
            User user = new User();
            try
            {
                var response = await client.GetAsync(uri + $"{id}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return user;
        }

        //GET: /api/User/username/{username}
        public async Task<User> GetUserByUsername(string username)
        {
            User user = new User();
            try
            {
                var response = await client.GetAsync(uri + $"username/{username}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return user;
        }

        //POST: /api/User
        public async Task<HttpStatusCode> CreateUser(User user)
        {
            string json = JsonConvert.SerializeObject(user);
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

        //PUT: /api/User
        public async Task<HttpStatusCode> UpdateUser(User user)
        {
            string json = JsonConvert.SerializeObject(user);
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

        //DELETE: /api/User/{id}
        public async Task<HttpStatusCode> DeleteUser(int id)
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
