using Codeology_Tests.Controllers;
using Codeology_Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MovieAPITests
{
    [Trait("Category", "Unit")]
    //dotnet test --filter "Category=Unit"
    public class UnitTest
    {
        private DbContextOptions<MoviesContext> MockContext()
        {
            var options = new DbContextOptionsBuilder<MoviesContext>()
              .UseSqlServer("Server=localhost;Database=Movies;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False;")
              .Options;
            return options;
        }

        [Fact]
        public async Task Get_ReturnsListOfMovies()
        {

            var options = MockContext();
            //var mockContext = new Mock<IMoviesContext>();
            //mockContext.Setup(c => c.MoviesDB).Returns(MockDbSet(moviesData));

            using var context = new MoviesContext(options);


            var controller = new MoviesController(context);

            // Act
            var result = await controller.Get();

            // Assert
           Assert.NotNull(result);
           Assert.IsType<List<Movies>>(result);

        }
       

    }
}
