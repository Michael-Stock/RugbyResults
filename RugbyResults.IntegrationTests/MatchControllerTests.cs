/*
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RugbyResults.DAL.Matches;
using System.Collections.Generic;
using System.Linq;

namespace RugbyResults.IntegrationTests
{
    public class MatchControllerTests
    {
        private static WebApplicationFactory<Startup> _factory;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting("http_port", "5001").UseEnvironment("Testing");
                    builder.ConfigureServices(services =>
                    {
                        services.AddDbContext<MatchContext>(options => options.UseInMemoryDatabase("TestDb"));
                    });
                });
        }

        [TestMethod]
        public void GetAllMatches_ReturnsAllMatches()
        {

        }
    }
}
*/
