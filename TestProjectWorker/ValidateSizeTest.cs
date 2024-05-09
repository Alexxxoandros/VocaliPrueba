using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using WorkerService1;

namespace TestProjectWorker
{
    public class ValidateSizeTest
    {
        [Fact]
        public void Validate_ShouldReturnTrue_WhenFileSizeIsWithinRange()
        {
            // Arrange
            var configuration = new Mock<IConfiguration>();
            var logger = new Mock<ILogger<ValidateSize>>();
            configuration.Setup(c => c.GetSection("MinSize").Value).Returns("50000");
            configuration.Setup(c => c.GetSection("MaxSize").Value).Returns("3000000");

            var validateSize = new ValidateSize(logger.Object, configuration.Object);
            var file = "testAyudio1.mp3";

            // Act
            var result = validateSize.Validate(file);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenFileSizeIsBelowMinSize()
        {
            // Arrange
            var configuration = new Mock<IConfiguration>();
            var logger = new Mock<ILogger<ValidateSize>>();
            configuration.Setup(c => c.GetSection("MinSize").Value).Returns("100");
            configuration.Setup(c => c.GetSection("MaxSize").Value).Returns("1000");

            var validateSize = new ValidateSize(logger.Object, configuration.Object);
            var file = "testAyudio1.mp3";

            // Act
            var result = validateSize.Validate(file);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenFileSizeIsAboveMaxSize()
        {
            // Arrange
            var configuration = new Mock<IConfiguration>();
            var logger = new Mock<ILogger<ValidateSize>>();
            configuration.Setup(c => c.GetSection("MinSize").Value).Returns("100");
            configuration.Setup(c => c.GetSection("MaxSize").Value).Returns("1000");

            var validateSize = new ValidateSize(logger.Object, configuration.Object);
            var file = "testAyudio1.mp3";


            // Act
            var result = validateSize.Validate(file);

            // Assert
            Assert.False(result);
        }
    }
}
