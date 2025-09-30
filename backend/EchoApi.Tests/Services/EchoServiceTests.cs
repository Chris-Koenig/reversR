using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using EchoApi.Models;
using EchoApi.Services;

namespace EchoApi.Tests.Services;

public class EchoServiceTests
{
    private readonly Mock<ILogger<EchoService>> _mockLogger;
    private readonly EchoService _echoService;

    public EchoServiceTests()
    {
        _mockLogger = new Mock<ILogger<EchoService>>();
        _echoService = new EchoService(_mockLogger.Object);
    }

    [Fact]
    public void ProcessEcho_ValidRequest_ReturnsCorrectResponse()
    {
        // Arrange
        var request = new EchoRequest("Hello World");

        // Act
        var result = _echoService.ProcessEcho(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Hello World", result.Text);
    }

    [Fact]
    public void ProcessEcho_EmptyText_ReturnsEmptyResponse()
    {
        // Arrange
        var request = new EchoRequest("");

        // Act
        var result = _echoService.ProcessEcho(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("", result.Text);
    }

    [Fact]
    public void ProcessEcho_LogsCorrectly()
    {
        // Arrange
        var request = new EchoRequest("Test message");

        // Act
        _echoService.ProcessEcho(request);

        // Assert
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => o.ToString()!.Contains("Processing echo request")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}