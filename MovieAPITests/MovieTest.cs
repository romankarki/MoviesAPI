using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieAPITests
{

    public class MovieTest: IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        public MovieTest(TestingWebAppFactory<Program> factory) => _client = factory.CreateClient();
       

        [Fact]
        public async Task GetMovies_ShouldReturnOk()
        {
            var response = await _client.GetAsync("api/Movies/GetMovies");
            var content = await response.Content.ReadAsStringAsync();
            Assert.True(response.IsSuccessStatusCode);
            // count \
            // is ok object result 
        }

        [Fact]
        public async Task PostMovies_ShouldCreateMovies() 
        {
            var model = new MovieRequest() { Name ="new movie 100", Rating = 4.5}; // payload 
            var response = await _client.PostAsync("/api/Movies/PostMovie",
                       new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8)
                       {
                           Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                       }).ConfigureAwait(false); // creating a API call 
            response.EnsureSuccessStatusCode(); // should create 
            var jsonContent = await response.Content.ReadAsStringAsync();

            Assert.Contains("1", jsonContent);
        }
    }
}