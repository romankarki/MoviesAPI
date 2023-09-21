using MovieAPITests.IntegrationHelper;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MovieAPITests.IntegrationTests
{
    [Trait("Category", "Integration")]
    //dotnet test --filter "Category=Integration"
    public class MovieScenarioTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        private List<int> _createdMoviesId;
        public MovieScenarioTest(TestingWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _createdMoviesId = new List<int>();
        }

        [Fact]
        //task_result
        public async Task PostMovie_GetMovies_UpdateMovies_ShouldSucceed()
        {
            var model = new MovieRequest
            {
                Name = "Integration test movie 100",
                Rating = 4.2
            };
            //total 3 calls 
            //post movie 
            var postResult  = await PostMovie_Should_Succeed(model);
            //get movies
            await GetMovies_Should_Succeed();
            if(postResult != null)
            {
                //edit 
                var editModel = new MovieRequest { Name = "updated name here", Rating = postResult.Rating, Id = postResult.Id };
                await UpdateMovie_Should_Succed(editModel);

                //await DeleteMovie_Should_Succed(editModel.Id);
            }
            await CleanUp();
        }

        private async Task GetMovies_Should_Succeed()
        {
            var response = await MovieHelper.GetMovies(_client);
            Assert.True(response != null);
        }

        private async Task<Movies?> PostMovie_Should_Succeed(MovieRequest model)
        {
            var response = await MovieHelper.PostMovie(_client, model);
            Assert.True(response != null);
            if(response != null) _createdMoviesId.Add(response.Id);
            return response;
        }

        private async Task<Movies?> UpdateMovie_Should_Succed(MovieRequest model)
        {
            var response = await MovieHelper.UpdateMovie(_client, model);
            Assert.True(response != null);
            return response;
        }

        private async Task DeleteMovie_Should_Succed(int id)
        {
            var response = await MovieHelper.DeleteMovie(_client, id);
            Assert.True(response != null);
        }

        protected async Task CleanUp()
        {
            foreach(var movieId in _createdMoviesId)
            {
                await DeleteMovie_Should_Succed(movieId);
            }
            
        }
    }
}
