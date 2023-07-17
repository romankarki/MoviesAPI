using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MovieAPITests.IntegrationHelper
{
    public class MovieHelper
    {
        public static async Task<List<Movies>> GetMovies(HttpClient _client)
        {
            var response = await _client.GetAsync("/api/Movies/GetMovies");

            response.EnsureSuccessStatusCode();
            var jsonContent = await response.Content.ReadAsStringAsync();
            var webObj = JsonConvert.DeserializeObject<List<Movies>>(jsonContent);
            return webObj;
        }

        public static async Task<Movies> PostMovie(HttpClient _client, MovieRequest movie)
        {
            var response = await _client.PostAsync("/api/Movies/PostMovie", 
                new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8)
                {
                    Headers = {ContentType = new MediaTypeHeaderValue("application/json")}
                }).ConfigureAwait(false);
            var jsonContent = await response.Content.ReadAsStringAsync();
            var webObj = JsonConvert.DeserializeObject<Movies>(jsonContent);
            return webObj;
        }

        public static async Task<Movies> UpdateMovie(HttpClient _client, MovieRequest movie)
        {
            var response = await _client.PutAsync($"/api/Movies/EditMovie", 
                new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8)
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                }).ConfigureAwait(false);
            var strContent = await response.Content.ReadAsStringAsync();
            var webObj = JsonConvert.DeserializeObject<Movies>(strContent);
            return webObj;
        }

        public static async Task<string> DeleteMovie(HttpClient _client, int id)
        {
            var response = await _client.DeleteAsync($"/api/Movies/Delete/{id}");
            var strContent = await response.Content.ReadAsStringAsync();
            return strContent;
        }
    }
}
