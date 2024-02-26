using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestDepences
{
    [TestClass]
    public class DeponcesControllerTests
    {
        private DeponcesController _controller;
        private Mock<ILogger<DeponcesController>> _loggerMock;
        private Mock<IDepenceManager<Depence>> _depenceManagerMock;
        private Mock<DepencesBusiness> _depencesBusinessMock;

        [TestMethod]
        public void GetDepences()
        {

        }
    }
}
