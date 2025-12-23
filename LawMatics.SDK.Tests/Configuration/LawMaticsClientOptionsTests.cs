using System;
using LawMatics.SDK.Configuration;
using Xunit;

namespace LawMatics.SDK.Tests.Configuration
{
    /// <summary>
    /// Unit tests for the <see cref="LawMaticsClientOptions"/> class.
    /// </summary>
    public class LawMaticsClientOptionsTests
    {
        [Fact]
        public void Default_ReturnsDefaultOptions()
        {
            // Act
            var options = LawMaticsClientOptions.Default;

            // Assert
            Assert.Equal("https://api.lawmatics.com", options.BaseUrl);
            Assert.Equal("v1", options.ApiVersion);
            Assert.Equal(30, options.TimeoutSeconds);
            Assert.Equal(3, options.MaxRetryAttempts);
            Assert.Equal(1000, options.RetryDelayMilliseconds);
            Assert.True(options.EnableRetry);
            Assert.True(options.IncludeErrorDetails);
            Assert.False(options.EnableLogging);
            Assert.NotNull(options.CustomHeaders);
            Assert.Empty(options.CustomHeaders);
            Assert.Equal("LawMatics-SDK/1.0.0", options.UserAgent);
            Assert.Equal("https://app.lawmatics.com/oauth/authorize", options.AuthorizationUrl);
            Assert.Equal("https://api.lawmatics.com/oauth/token", options.TokenUrl);
        }

        [Fact]
        public void Development_ReturnsDevelopmentOptions()
        {
            // Act
            var options = LawMaticsClientOptions.Development;

            // Assert
            Assert.Equal("https://api.lawmatics.com", options.BaseUrl);
            Assert.Equal("v1", options.ApiVersion);
            Assert.Equal(60, options.TimeoutSeconds);
            Assert.Equal(3, options.MaxRetryAttempts);
            Assert.Equal(1000, options.RetryDelayMilliseconds);
            Assert.True(options.EnableRetry);
            Assert.True(options.IncludeErrorDetails);
            Assert.True(options.EnableLogging);
        }

        [Fact]
        public void Production_ReturnsProductionOptions()
        {
            // Act
            var options = LawMaticsClientOptions.Production;

            // Assert
            Assert.Equal("https://api.lawmatics.com", options.BaseUrl);
            Assert.Equal("v1", options.ApiVersion);
            Assert.Equal(30, options.TimeoutSeconds);
            Assert.Equal(2, options.MaxRetryAttempts);
            Assert.Equal(2000, options.RetryDelayMilliseconds);
            Assert.True(options.EnableRetry);
            Assert.False(options.IncludeErrorDetails);
            Assert.False(options.EnableLogging);
        }

        [Fact]
        public void Validate_WithDefaultOptions_DoesNotThrow()
        {
            // Arrange
            var options = LawMaticsClientOptions.Default;

            // Act & Assert
            options.Validate(); // Should not throw
        }

        [Fact]
        public void Validate_WithEmptyBaseUrl_ThrowsArgumentException()
        {
            // Arrange
            var options = new LawMaticsClientOptions
            {
                BaseUrl = ""
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Contains("Base URL is required", exception.Message);
        }

        [Fact]
        public void Validate_WithNullBaseUrl_ThrowsArgumentException()
        {
            // Arrange
            var options = new LawMaticsClientOptions
            {
                BaseUrl = null!
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Contains("Base URL is required", exception.Message);
        }

        [Fact]
        public void Validate_WithInvalidBaseUrl_ThrowsArgumentException()
        {
            // Arrange
            var options = new LawMaticsClientOptions
            {
                BaseUrl = "invalid-url"
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Contains("must be a valid absolute URI", exception.Message);
        }

        [Fact]
        public void Validate_WithEmptyApiVersion_ThrowsArgumentException()
        {
            // Arrange
            var options = new LawMaticsClientOptions
            {
                ApiVersion = ""
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Contains("API version is required", exception.Message);
        }

        [Fact]
        public void Validate_WithNullApiVersion_ThrowsArgumentException()
        {
            // Arrange
            var options = new LawMaticsClientOptions
            {
                ApiVersion = null!
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Contains("API version is required", exception.Message);
        }

        [Fact]
        public void Validate_WithNegativeTimeout_ThrowsArgumentException()
        {
            // Arrange
            var options = new LawMaticsClientOptions
            {
                TimeoutSeconds = -1
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Contains("must be greater than zero", exception.Message);
        }

        [Fact]
        public void Validate_WithZeroTimeout_ThrowsArgumentException()
        {
            // Arrange
            var options = new LawMaticsClientOptions
            {
                TimeoutSeconds = 0
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Contains("must be greater than zero", exception.Message);
        }

        [Fact]
        public void Validate_WithNegativeMaxRetryAttempts_ThrowsArgumentException()
        {
            // Arrange
            var options = new LawMaticsClientOptions
            {
                MaxRetryAttempts = -1
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Contains("cannot be negative", exception.Message);
        }

        [Fact]
        public void Validate_WithNegativeRetryDelay_ThrowsArgumentException()
        {
            // Arrange
            var options = new LawMaticsClientOptions
            {
                RetryDelayMilliseconds = -1
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Contains("cannot be negative", exception.Message);
        }

        [Fact]
        public void Validate_WithInvalidAuthorizationUrl_ThrowsArgumentException()
        {
            // Arrange
            var options = new LawMaticsClientOptions
            {
                AuthorizationUrl = "invalid-url"
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Contains("must be a valid absolute URI", exception.Message);
        }

        [Fact]
        public void Validate_WithInvalidTokenUrl_ThrowsArgumentException()
        {
            // Arrange
            var options = new LawMaticsClientOptions
            {
                TokenUrl = "invalid-url"
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Contains("must be a valid absolute URI", exception.Message);
        }

        [Fact]
        public void GetApiUrl_WithValidEndpoint_ReturnsFullUrl()
        {
            // Arrange
            var options = new LawMaticsClientOptions
            {
                BaseUrl = "https://api.test.lawmatics.com/",
                ApiVersion = "v2"
            };

            // Act
            var url = options.GetApiUrl("contacts");

            // Assert
            Assert.Equal("https://api.test.lawmatics.com/v2/contacts", url);
        }

        [Fact]
        public void GetApiUrl_WithEndpointStartingWithSlash_ReturnsFullUrl()
        {
            // Arrange
            var options = new LawMaticsClientOptions
            {
                BaseUrl = "https://api.test.lawmatics.com",
                ApiVersion = "v1"
            };

            // Act
            var url = options.GetApiUrl("/users");

            // Assert
            Assert.Equal("https://api.test.lawmatics.com/v1/users", url);
        }

        [Fact]
        public void GetApiUrl_WithNullEndpoint_ThrowsArgumentException()
        {
            // Arrange
            var options = new LawMaticsClientOptions();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => options.GetApiUrl(null!));
        }

        [Fact]
        public void GetApiUrl_WithEmptyEndpoint_ThrowsArgumentException()
        {
            // Arrange
            var options = new LawMaticsClientOptions();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => options.GetApiUrl(""));
        }

        [Fact]
        public void AddCustomHeader_WithValidParameters_AddsHeader()
        {
            // Arrange
            var options = new LawMaticsClientOptions();

            // Act
            options.AddCustomHeader("X-Custom-Header", "custom-value");

            // Assert
            Assert.True(options.CustomHeaders.ContainsKey("X-Custom-Header"));
            Assert.Equal("custom-value", options.CustomHeaders["X-Custom-Header"]);
        }

        [Fact]
        public void AddCustomHeader_WithNullName_ThrowsArgumentException()
        {
            // Arrange
            var options = new LawMaticsClientOptions();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => options.AddCustomHeader(null!, "value"));
        }

        [Fact]
        public void AddCustomHeader_WithEmptyName_ThrowsArgumentException()
        {
            // Arrange
            var options = new LawMaticsClientOptions();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => options.AddCustomHeader("", "value"));
        }

        [Fact]
        public void AddCustomHeader_WithNullValue_SetsEmptyValue()
        {
            // Arrange
            var options = new LawMaticsClientOptions();

            // Act
            options.AddCustomHeader("X-Test", null!);

            // Assert
            Assert.True(options.CustomHeaders.ContainsKey("X-Test"));
            Assert.Equal(string.Empty, options.CustomHeaders["X-Test"]);
        }

        [Fact]
        public void AddCustomHeader_WithExistingName_OverwritesValue()
        {
            // Arrange
            var options = new LawMaticsClientOptions();
            options.AddCustomHeader("X-Test", "original-value");

            // Act
            options.AddCustomHeader("X-Test", "new-value");

            // Assert
            Assert.Equal("new-value", options.CustomHeaders["X-Test"]);
        }

        [Fact]
        public void RemoveCustomHeader_WithExistingHeader_RemovesHeader()
        {
            // Arrange
            var options = new LawMaticsClientOptions();
            options.AddCustomHeader("X-Test", "value");

            // Act
            var result = options.RemoveCustomHeader("X-Test");

            // Assert
            Assert.True(result);
            Assert.False(options.CustomHeaders.ContainsKey("X-Test"));
        }

        [Fact]
        public void RemoveCustomHeader_WithNonExistingHeader_ReturnsFalse()
        {
            // Arrange
            var options = new LawMaticsClientOptions();

            // Act
            var result = options.RemoveCustomHeader("X-NonExisting");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void RemoveCustomHeader_WithNullName_ReturnsFalse()
        {
            // Arrange
            var options = new LawMaticsClientOptions();

            // Act
            var result = options.RemoveCustomHeader(null!);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void RemoveCustomHeader_WithEmptyName_ReturnsFalse()
        {
            // Arrange
            var options = new LawMaticsClientOptions();

            // Act
            var result = options.RemoveCustomHeader("");

            // Assert
            Assert.False(result);
        }
    }
}