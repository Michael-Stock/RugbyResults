using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RugbyResults.DAL.Matches;
using RugbyResults.Domain.Matches;
using RugbyResults.Matches;
using RugbyResults.Tests.Mocks;
using RugbyResults.Warmup;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RugbyResults.Tests.Warmup
{
    [TestClass]
    public class WarmupServiceTests
    {
        [TestMethod]
        public async Task Execute_NoData_CachesData()
        {
            List<RugbyMatch> matches = RugbyMatchMocks.GetData();
            Mock<IMatchService> matchService = new Mock<IMatchService>();
            matchService.Setup(m => m.GetByTeamId(It.IsAny<int>())).ReturnsAsync(matches);

            Mock<IMatchDal> matchDal = new Mock<IMatchDal>();
            matchDal.Setup(m => m.GetAll()).Returns(new List<RugbyMatch>());

            IWarmupService warmupService = new WarmupService(matchDal.Object, matchService.Object);

            await warmupService.Execute();

            matchDal.Verify(m => m.Add(matches));
        }

        [TestMethod]
        public async Task Execute_ExistingData_DoesNotCacheData()
        {
            List<RugbyMatch> matches = RugbyMatchMocks.GetData();
            Mock<IMatchDal> matchDal = new Mock<IMatchDal>();
            matchDal.Setup(m => m.GetAll()).Returns(matches);

            Mock<IMatchService> matchService = new Mock<IMatchService>();

            IWarmupService warmupService = new WarmupService(matchDal.Object, matchService.Object);

            await warmupService.Execute();

            matchService.Verify(m => m.GetByTeamId(It.IsAny<int>()), Times.Never);
            matchDal.Verify(m => m.Add(matches), Times.Never);
        }
    }
}
