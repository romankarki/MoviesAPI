using Codeology_Tests.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MovieAPITests
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<MoviesContext>));
                if (descriptor != null)
                    services.Remove(descriptor);
                services.AddEntityFrameworkInMemoryDatabase().AddDbContext<MoviesContext>((sp, options) =>
                {
                    options.UseSqlServer("Server=localhost;Database=Movies;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False;");
                    // options.UseInMemoryDatabase("InMemoryEmployeeTest").UseInternalServiceProvider(sp);
                });
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<MoviesContext>())
                {
                    try
                    {
                        appContext.Database.EnsureCreated();
                    }
                    catch (Exception ex)
                    {
                        // log the exception if required
                        throw;
                    }
                }
            });
        }
    }
}
