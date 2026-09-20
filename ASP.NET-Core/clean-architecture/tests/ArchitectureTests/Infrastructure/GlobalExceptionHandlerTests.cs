#nullable disable
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Web.Api.Infrastructure;
using Xunit;


namespace Web.Api.Infrastructure.UnitTests;

/// <summary>
/// Unit tests for the <see cref="GlobalExceptionHandler"/> class.
/// </summary>
public sealed class GlobalExceptionHandlerTests
{
    /// <summary>
    /// Tests that TryHandleAsync returns true when handling an exception.
    /// </summary>
    [Fact]
    public async Task TryHandleAsync_WithValidException_ReturnsTrue()
    {
        // Arrange
        Mock<ILogger<GlobalExceptionHandler>> loggerMock = new();
        Mock<HttpContext> httpContextMock = new();
        Mock<HttpResponse> httpResponseMock = new();
        Exception exception = new InvalidOperationException("Test exception");
        CancellationToken cancellationToken = CancellationToken.None;

        httpContextMock.Setup(x => x.Response).Returns(httpResponseMock.Object);
        httpResponseMock.SetupProperty(x => x.StatusCode);
        httpResponseMock.Setup(x => x.Body).Returns(new MemoryStream());

        GlobalExceptionHandler handler = new(loggerMock.Object);

        // Act
        bool result = await handler.TryHandleAsync(httpContextMock.Object, exception, cancellationToken);

        // Assert
        result.ShouldBeTrue();
    }

    /// <summary>
    /// Tests that TryHandleAsync sets the HTTP response status code to 500 (Internal Server Error).
    /// </summary>
    [Fact]
    public async Task TryHandleAsync_WithValidException_SetsStatusCodeTo500()
    {
        // Arrange
        Mock<ILogger<GlobalExceptionHandler>> loggerMock = new();
        Mock<HttpContext> httpContextMock = new();
        Mock<HttpResponse> httpResponseMock = new();
        var exception = new Exception("Test exception");
        CancellationToken cancellationToken = CancellationToken.None;

        httpContextMock.Setup(x => x.Response).Returns(httpResponseMock.Object);
        httpResponseMock.SetupProperty(x => x.StatusCode);
        httpResponseMock.Setup(x => x.Body).Returns(new MemoryStream());

        GlobalExceptionHandler handler = new(loggerMock.Object);

        // Act
        await handler.TryHandleAsync(httpContextMock.Object, exception, cancellationToken);

        // Assert
        httpResponseMock.Object.StatusCode.ShouldBe(500);
    }

}
