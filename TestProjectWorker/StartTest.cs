using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService1;

namespace TestProjectWorker
{
    public class StartTest
    {
        private readonly Mock<ILogger<Start>> _loggerMock;
        private readonly Mock<Mp3Recover> _mp3RecoverMock;
        private readonly Mock<SendToInvoxBatch> _sendToInvoxMock;
        private readonly Mock<Validations> _validationsMock;

        public StartTest()
        {
            _loggerMock = new Mock<ILogger<Start>>();
            _mp3RecoverMock = new Mock<Mp3Recover>();
            _sendToInvoxMock = new Mock<SendToInvoxBatch>();
            _validationsMock = new Mock<Validations>();
        }

        [Fact]
        public async Task StartMethod_ShouldCallLoggerInformation()
        {
            // Arrange
            var validations = new Validations();
            var mp3Recover = new Mp3Recover();
            var sendToInvox = new SendToInvoxBatch();
            var start = new Start(_loggerMock.Object, _validationsMock.Object, _mp3RecoverMock.Object, _sendToInvoxMock.Object);

            // Act
            await start.StartMethod();

            // Assert
            _loggerMock.Verify(x => x.LogInformation("StartMethod called"), Times.Once);
        }

        [Fact]
        public async Task StartMethod_ShouldCallMp3RecoverRecover()
        {
            // Arrange
            var start = new Start(_loggerMock.Object, _validationsMock.Object, _mp3RecoverMock.Object, _sendToInvoxMock.Object);

            // Act
            await start.StartMethod();

            // Assert
            _mp3RecoverMock.Verify(x => x.Recover(), Times.Once);
        }

        [Fact]
        public async Task StartMethod_ShouldCallValidationsValidateFiles()
        {
            // Arrange
            var start = new Start(_loggerMock.Object, _validationsMock.Object, _mp3RecoverMock.Object, _sendToInvoxMock.Object);
            var files = new List<string> { "file1.mp3", "file2.mp3" };
            _mp3RecoverMock.Setup(x => x.Recover()).Returns(files);

            // Act
            await start.StartMethod();

            // Assert
            _validationsMock.Verify(x => x.ValidateFiles(files), Times.Once);
        }

        [Fact]
        public async Task StartMethod_ShouldCallSendToInvoxBatchProcessBatch()
        {
            // Arrange
            var start = new Start(_loggerMock.Object, _validationsMock.Object, _mp3RecoverMock.Object, _sendToInvoxMock.Object);
            var files = new List<string> { "file1.mp3", "file2.mp3" };
            var validatedFiles = new List<string> { "file1.mp3" };
            _mp3RecoverMock.Setup(x => x.Recover()).Returns(files);
            _validationsMock.Setup(x => x.ValidateFiles(files)).Returns(validatedFiles);

            // Act
            await start.StartMethod();

            // Assert
            _sendToInvoxMock.Verify(x => x.ProcessBatch(validatedFiles), Times.Once);
        }
    }
}
