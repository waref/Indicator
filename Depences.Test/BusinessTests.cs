using Castle.Core.Configuration;
using Depences.Application.IMangers;
using Depences.Domain.Enums;
using Depences.Domain.Models;
using Depences.Infrastructure.Interfaces.IRepositories;
using DepencesApi.BusinessLogic;
using Moq;

namespace Depences.Test
{
    [TestFixture]
    public class BusinessTests
    {
        private DepencesBusiness _depencesBusiness;
        private Mock<DepencesBusiness> _depencesBusinessMock;
        private Mock<IDepenceManager<Depence>> _depenceManagerMock;
        private Mock<IDepenceRepository> _depenceRepositoryMock;
        private Mock<IConfiguration> _configurationMock;

        [SetUp]
        public void Setup()
        {//_configurationMock.Object
            _depencesBusinessMock = new Mock<DepencesBusiness>();
            _depenceRepositoryMock = new Mock<IDepenceRepository>();
            _depenceManagerMock = new Mock<IDepenceManager<Depence>>();
            _depencesBusiness = new DepencesBusiness(_depenceManagerMock.Object, configuration: (Microsoft.Extensions.Configuration.IConfiguration)It.IsAny<IConfiguration>());
            

            User user1 = new User { UserId = 1, NomFamille = "Stark", Prenom = "Anthony", DeviseId = 1 };
            Nature nature1 = new Nature { NatureId = 1, Code = "Restaurant" };
            var expectedDepencesList = new List<Depence>() {
                new Depence(1,user1,nature1,100,"test 1",DateTime.Now.AddDays(-3),1),
                };
            _depenceManagerMock.Setup(manager => manager.GetDepenceAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedDepencesList);

            _depenceRepositoryMock.Setup(repo =>repo.SaveChangesAsync() ).Returns(Task.CompletedTask);



        }
        [Test]
        public async Task IsValidSaveDepence_RetunsSuccessResult()
        {
            //Arrange 
            User user2 = new User { UserId = 2, NomFamille = "Romanova", Prenom = "Natasha", DeviseId = 2 };
            Nature nature2 = new Nature { NatureId = 2, Code = "Hotel" };
            Depence depenceToSave= new Depence(2, user2, nature2, 100.3, "test 2", DateTime.Now.AddDays(-10), 1);
            var cancellationToken = new CancellationToken();

            _depenceManagerMock.Setup(manager => manager.FindADuplicatedsync(depenceToSave, cancellationToken))
                .ReturnsAsync(Array.Empty<Depence>());

            _depenceManagerMock.Setup(manager => manager.IsCurrencyMatchingAsync(depenceToSave, cancellationToken))
                .Returns(true);

            _depenceManagerMock.Setup(manager => manager.IsExistingNature(depenceToSave.NatureId))
                .Returns(true);

            _depenceManagerMock.Setup(manager => manager.SeveDepenceAsync(depenceToSave))
                .ReturnsAsync(new ExecutionResult(ExecutionStatus.Success));

            // Act
            var result = await _depencesBusiness.Save(depenceToSave, cancellationToken);

            // Assert
            Assert.AreEqual(ExecutionStatus.Success, result.Status);
            _depenceManagerMock.Verify(manager => manager.SeveDepenceAsync(depenceToSave), Times.Once);
        }
        [Test]
        public async Task Save_DuplicatedDepence_ReturnsErrorResult()
        {
            // Arrange
            User user1 = new User { UserId = 1, NomFamille = "Stark", Prenom = "Anthony", DeviseId = 1 };
            Nature nature2 = new Nature { NatureId = 10, Code = "Sport" };
            Depence depenceToSave = new Depence(2, user1, nature2, 100, "test 1", DateTime.Now.AddDays(-3), 1);
            var cancellationToken = new CancellationToken();

            _depenceManagerMock.Setup(manager => manager.FindADuplicatedsync(depenceToSave, cancellationToken))
                .ReturnsAsync(new[] { depenceToSave });

            // Act
            var result = await _depencesBusiness.Save(depenceToSave, cancellationToken);

            // Assert
            Assert.That(result.Status, Is.EqualTo(expected: ExecutionStatus.Error));
            StringAssert.Contains("Depences existe déjà", result.Message);

        }

        [Test]
        public async Task Save_NonMatchingCurrency_ReturnsErrorResult()
        {
            // Arrange
            User user1 = new User { UserId = 1, NomFamille = "Stark", Prenom = "Anthony", DeviseId = 1 };
            Nature nature2 = new Nature { NatureId = 2, Code = "Hotel" };
            Depence depenceToSave = new Depence(2, user1, nature2, 100.3, "test 2", DateTime.Now.AddDays(-10), 2);
            var cancellationToken = new CancellationToken();

            _depenceManagerMock.Setup(manager => manager.FindADuplicatedsync(depenceToSave, cancellationToken))
                .ReturnsAsync(Array.Empty<Depence>());

            _depenceManagerMock.Setup(manager => manager.IsCurrencyMatchingAsync(depenceToSave, cancellationToken))
                .Returns(false);


            // Act
            var result = await _depencesBusiness.Save(depenceToSave, cancellationToken);

            // Assert
            Assert.That(result.Status, Is.EqualTo(expected: ExecutionStatus.Error));
            StringAssert.Contains("Devises ne matches pas", result.Message);
        }

        [Test]
        public async Task Save_InvalidNatureId_ReturnsErrorResult()
        {
            // Arrange
            User user1 = new User { UserId = 1, NomFamille = "Stark", Prenom = "Anthony", DeviseId = 1 };
            Nature nature2 = new Nature { NatureId = 2, Code = "Hotel" };
            Depence depenceToSave = new Depence(2, user1, nature2, 100.3, "test 2", DateTime.Now.AddDays(-10), 2);
            var cancellationToken = new CancellationToken();

            _depenceManagerMock.Setup(manager => manager.FindADuplicatedsync(depenceToSave, cancellationToken))
                .ReturnsAsync(Array.Empty<Depence>());

            _depenceManagerMock.Setup(manager => manager.IsCurrencyMatchingAsync(depenceToSave, cancellationToken))
                .Returns(true);

            _depenceManagerMock.Setup(manager => manager.IsExistingNature(depenceToSave.NatureId))
                .Returns(false);

            // Act
            var result = await _depencesBusiness.Save(depenceToSave, cancellationToken);

            // Assert
            Assert.That(result.Status, Is.EqualTo(ExecutionStatus.Error));
            StringAssert.Contains("Nature ID non valide", result.Message);

        }

    }
}
