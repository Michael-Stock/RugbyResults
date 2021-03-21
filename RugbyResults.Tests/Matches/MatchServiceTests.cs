using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using RugbyResults.Configuration;
using RugbyResults.Domain.Matches;
using RugbyResults.Matches;
using RugbyResults.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RugbyResults.Tests
{
    [TestClass]
    public class MatchServiceTests
    {
        [TestMethod]
        public async Task GetByTeamId_Success()
        {
            List<RugbyMatch> matchResponse = RugbyMatchMocks.GetData();
            string matchResponseJson = JsonSerializer.Serialize(matchResponse);

            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(matchResponseJson)
            };

            IHttpClientFactory mockFactory = BuildMockHttpClient(response);
            IOptions<ExternalApiOptions> externalApiOptions = CreateMockConfiguration();
            IMatchService matchService = new MatchService(externalApiOptions, mockFactory);

            List<RugbyMatch> result = await matchService.GetByTeamId(103969);

            Assert.AreEqual(3, result.Count);

            RugbyMatch firstMatch = result.First(m => m.matchId == 1);
            Assert.AreEqual("W", firstMatch.result);
            Assert.AreEqual("Old Trafford", firstMatch.stadiumName);

            RugbyMatch secondMatch = result.First(m => m.matchId == 2);
            Assert.AreEqual("W", secondMatch.result);
            Assert.AreEqual("Signal Iduna Park", secondMatch.stadiumName);

            RugbyMatch thirdMatch = result.First(m => m.matchId == 3);
            Assert.AreEqual("L", thirdMatch.result);
            Assert.AreEqual("Bernabeu", thirdMatch.stadiumName);
        }

        [TestMethod]
        public async Task GetByTeamId_Error()
        {
            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("Failed to retrieve matches")
            };

            IHttpClientFactory mockFactory = BuildMockHttpClient(response);
            IOptions<ExternalApiOptions> externalApiOptions = CreateMockConfiguration();
            IMatchService matchService = new MatchService(externalApiOptions, mockFactory);

            Exception result = null;

            try
            {
                await matchService.GetByTeamId(103969);
            }
            catch (Exception ex)
            {
                result = ex;
            }

            Assert.AreEqual("BadRequest - Failed to retrieve matches", result.Message);
        }

        private IHttpClientFactory BuildMockHttpClient(HttpResponseMessage httpResponseMessage)
        {
            Mock<IHttpClientFactory> result = new Mock<IHttpClientFactory>();
            Mock<HttpMessageHandler> mockHandler = new Mock<HttpMessageHandler>();

            mockHandler
                .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(httpResponseMessage);

            HttpClient mockClient = new HttpClient(mockHandler.Object);

            result
                .Setup(m => m.CreateClient(""))
                .Returns(mockClient);

            return result.Object;
        }

        private IOptions<ExternalApiOptions> CreateMockConfiguration()
        {
            ExternalApiOptions externalApiOptions = new ExternalApiOptions()
            {
                Url = "http://locahost:9001",
                Season = "2019-2020"
            };

            IOptions<ExternalApiOptions> result = Options.Create<ExternalApiOptions>(externalApiOptions);

            return result;
        }
    }
}
