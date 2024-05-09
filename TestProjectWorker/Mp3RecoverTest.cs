using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using WorkerService1;

namespace TestProjectWorker
{
    public class Mp3RecoverTest
    {

        [Fact]
        public void Recover_ShouldReturnListOfMp3Files_WhenFilesExist()
        {
            // Arrange
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(c => c.GetSection("PathFiles").Value).Returns("C:\\Test\\Documents\\Vocali\\Test");
            
            var expectedFiles = new List<string> { "file1.mp3", "file2.mp3" };
            var mp3Recover = new Mp3Recover(configuration.Object);

            // Act
            var result = mp3Recover.Recover();

            // Assert
            Assert.Equal(expectedFiles.Count, result.Count);
        }
    }
}
