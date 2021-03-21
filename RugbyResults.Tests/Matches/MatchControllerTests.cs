using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RugbyResults.DAL.Matches;
using RugbyResults.Domain.Matches;
using RugbyResults.Matches;
using RugbyResults.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RugbyResults.Tests.Matches
{
    [TestClass]
    public class MatchControllerTests
    {
        [TestMethod]
        public void Get_ReturnsMatches()
        {
            Mock<IMatchDal> mockDal = new Mock<IMatchDal>();
            mockDal.Setup(m => m.GetAll()).Returns(RugbyMatchMocks.GetData());
            MatchController controller = new MatchController(mockDal.Object);

            List<RugbyMatch> result = controller.Get();

            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void GetById_ReturnsMatch()
        {
            RugbyMatch toReturn = RugbyMatchMocks.GetData().First();
            Mock<IMatchDal> mockDal = new Mock<IMatchDal>();
            mockDal.Setup(m => m.GetById(It.IsAny<int>())).Returns(toReturn);

            MatchController controller = new MatchController(mockDal.Object);

            OkObjectResult actionResult = controller.GetById(1) as OkObjectResult;
            Assert.AreEqual(200, actionResult.StatusCode);

            RugbyMatch rugbyMatchResult = actionResult.Value as RugbyMatch;
            Assert.AreEqual(toReturn, rugbyMatchResult);
        }

        [TestMethod]
        public void GetById_NotFound_ReturnsNotFound()
        {
            Mock<IMatchDal> mockDal = new Mock<IMatchDal>();
            mockDal.Setup(m => m.GetById(It.IsAny<int>()));

            MatchController controller = new MatchController(mockDal.Object);

            NotFoundResult actionResult = controller.GetById(1) as NotFoundResult;

            Assert.AreEqual(404, actionResult.StatusCode);
        }
    }
}
