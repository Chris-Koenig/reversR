using Xunit;
using EchoApi.Models;
using EchoApi.Validators;

namespace EchoApi.Tests.Validators;

public class EchoRequestValidatorTests
{
    private readonly EchoRequestValidator _validator;

    public EchoRequestValidatorTests()
    {
        _validator = new EchoRequestValidator();
    }

    [Fact]
    public void Validate_ValidRequest_IsValid()
    {
        // Arrange
        var request = new EchoRequest("Hello World");

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Validate_EmptyText_IsInvalid()
    {
        // Arrange
        var request = new EchoRequest("");

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Text cannot be empty");
    }

    [Fact]
    public void Validate_NullText_IsInvalid()
    {
        // Arrange
        var request = new EchoRequest(null!);

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Text cannot be empty");
    }

    [Fact]
    public void Validate_TextTooLong_IsInvalid()
    {
        // Arrange
        var longText = new string('a', 501); // 501 characters
        var request = new EchoRequest(longText);

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Text cannot exceed 500 characters");
    }

    [Fact]
    public void Validate_TextExactly500Characters_IsValid()
    {
        // Arrange
        var text500 = new string('a', 500); // Exactly 500 characters
        var request = new EchoRequest(text500);

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}