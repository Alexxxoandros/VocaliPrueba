using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using WorkerService1;

namespace TestProjectWorker
{
    public class ValidateFormatTest
    {
        [Fact]
        public void Validate_ValidFile_ReturnsTrue()
        {
            // Arrange
            var logger = new Mock<ILogger<ValidateFormat>>();
            string validFile = "testAyudio1.mp3";

            var validateFormat = new ValidateFormat(logger.Object);
            // Act
            bool result = validateFormat.Validate(validFile);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_InvalidFile_ReturnsFalse()
        {
            // Arrange
            var logger = new Mock<ILogger<ValidateFormat>>();
            string validFile = "InvalidFile.mp3";

            var validateFormat = new ValidateFormat(logger.Object);
            // Act
            bool result = validateFormat.Validate(validFile);

            // Assert
            Assert.False(result);
        }
    }
}
