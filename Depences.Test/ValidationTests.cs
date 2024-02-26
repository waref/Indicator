using Depences.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Depences.Test
{
    [TestFixture]
    public class ValidationTests
    {
        #region Date Validation

        [Test]
        public void IsValid_WithValidDate_ReturnsSuccess()
        {
            // Arrange
            var attribute = new DepenceDateValidationAttribute();
            var validDate = DateTime.Now;

            // Act
            var validationResult = attribute.GetValidationResult(validDate, new ValidationContext(new object()));

            // Assert
            Assert.That(validationResult, Is.Null);
        }
        [Test]
        public void IsValid_WithNullDate_ReturnsValidationError()
        {
            // Arrange
            var attribute = new DepenceDateValidationAttribute();
            DateTime? nullDate = null;

            // Act
            var validationResult = attribute.GetValidationResult(nullDate, new ValidationContext(new object()));

            // Assert
            Assert.That(validationResult.ErrorMessage, Is.EqualTo(expected: "Date est vide!"));
        }

        [Test]
        public void IsValid_WithFutureDate_ReturnsValidationError()
        {
            // Arrange
            var attribute = new DepenceDateValidationAttribute();
            var futureDate = DateTime.Now.AddDays(1);

            // Act
            var validationResult = attribute.GetValidationResult(futureDate, new ValidationContext(new object()));

            // Assert
            Assert.AreEqual("La date de dépence ne peux pas être au future.", validationResult.ErrorMessage);
        }

        [Test]
        public void IsValid_WithDateMoreThanThreeMonthsAgo_ReturnValidationError()
        {
            // Arrange
            var attribute = new DepenceDateValidationAttribute();
            var pastDate = DateTime.Now.AddMonths(-4);

            // Act
            var validationResult = attribute.GetValidationResult(pastDate, new ValidationContext(new object()));

            // Assert
            Assert.IsNotNull(validationResult);
            Assert.That(validationResult.ErrorMessage, Is.EqualTo(expected: "La date de dépence ne peux pas dater plus que 3 mois."));
        }

        #endregion

        #region Nature Validation

        [Test]
        public void IsValid_WithValidNature_ReturnSuccess()
        {
            // Arrange
            var attribute = new DepenceNatureValidationAttribute();
            var validNature = "Restaurant";

            // Act
            var validationResult = attribute.GetValidationResult(validNature, new ValidationContext(new object()));

            // Assert
            Assert.IsNull(validationResult);
        }

        [Test]
        public void IsValid_WithNullNature_ReturnValidationError()
        {
            // Arrange
            var attribute = new DepenceNatureValidationAttribute();
            string? nullNature = null;

            // Act
            var validationResult = attribute.GetValidationResult(nullNature, new ValidationContext(new object()));

            // Assert
            Assert.IsNotNull(validationResult);
            Assert.That(validationResult.ErrorMessage, Is.EqualTo(expected: "Champ Nature est vide !"));
        }

        [Test]
        public void IsValid_WithEmptyNature_ReturnValidationError()
        {
            // Arrange
            var attribute = new DepenceNatureValidationAttribute();
            var emptyNature = string.Empty;

            // Act
            var validationResult = attribute.GetValidationResult(emptyNature, new ValidationContext(new object()));

            // Assert
            Assert.IsNotNull(validationResult);
            Assert.That(validationResult.ErrorMessage, Is.EqualTo(expected: "Champ Nature est vide !"));
        }

        [Test]
        public void IsValid_WithInvalidNature_ReturnsValidationError()
        {
            // Arrange
            var attribute = new DepenceNatureValidationAttribute();
            var invalidNature = "InvalidNature";

            // Act
            var validationResult = attribute.GetValidationResult(invalidNature, new ValidationContext(new object()));

            // Assert
            Assert.IsNotNull(validationResult);
            Assert.That(validationResult.ErrorMessage, Is.EqualTo(expected: "Champs Nature est invalide , les valeurs possibles sont: Restaurant, Hotel, et Misc."));
        }
        #endregion
    }

}
