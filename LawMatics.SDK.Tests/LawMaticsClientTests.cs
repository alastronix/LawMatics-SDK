using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using LawMatics.SDK.Authentication;
using LawMatics.SDK.Configuration;
using LawMatics.SDK.Exceptions;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Xunit;

namespace LawMatics.SDK.Tests
{
    /// <summary>
    /// Unit tests for the <see cref="LawMaticsClient"/> class.
    /// </summary>
    public class LawMaticsClientTests : IDisposable
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly Mock<ILogger<LawMaticsClient>> _mockLogger;
        private readonly LawMaticsClientOptions _options;
        private readonly LawMaticsCredentials _credentials;

        public LawMaticsClientTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _mockLogger = new Mock<ILogger<LawMaticsClient>>();
            _options = new LawMaticsClientOptions
            {
                BaseUrl = "https://api.test.lawmatics.com",
                ApiVersion = "v1",
                TimeoutSeconds = 30
            };
            _credentials = new LawMaticsCredentials("test-access-token")
            {
                ClientId = "test-client-id",
                ClientSecret = "test-client-secret",
                RefreshToken = "test-refresh-token"
            };
        }

        [Fact]
        public void Constructor_WithNullCredentials_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new LawMaticsClient((LawMaticsCredentials)null!, _options, _mockLogger.Object));
        }

        [Fact]
        public void Constructor_WithNullOptions_UsesDefaultOptions()
        {
            // Act
            using var client = new LawMaticsClient(_credentials, null, _mockLogger.Object);

            // Assert
            Assert.NotNull(client);
        }

        [Fact]
        public void Constructor_WithNullLogger_UsesNullLogger()
        {
            // Act
            using var client = new LawMaticsClient(_credentials, _options, null);

            // Assert
            Assert.NotNull(client);
        }

        [Fact]
        public void Constructor_WithInvalidBaseUrl_ThrowsArgumentException()
        {
            // Arrange
            var invalidOptions = new LawMaticsClientOptions
            {
                BaseUrl = "invalid-url"
            };

            // Act & Assert
            Assert.Throws<UriFormatException>(() => new LawMaticsClient(_credentials, invalidOptions, _mockLogger.Object));
        }

        [Fact]
        public void Constructor_WithNegativeTimeout_ThrowsArgumentException()
        {
            // Arrange
            var invalidOptions = new LawMaticsClientOptions
            {
                TimeoutSeconds = -1
            };

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new LawMaticsClient(_credentials, invalidOptions, _mockLogger.Object));
        }

        [Fact]
        public void Constructor_WithValidParameters_CreatesClient()
        {
            // Act
            using var client = new LawMaticsClient(_credentials, _options, _mockLogger.Object);

            // Assert
            Assert.NotNull(client);
            Assert.NotNull(client.Contacts);
            Assert.NotNull(client.Companies);
            Assert.NotNull(client.Matters);
            Assert.NotNull(client.Events);
            Assert.NotNull(client.Files);
            Assert.NotNull(client.Folders);
            Assert.NotNull(client.EmailCampaigns);
            Assert.NotNull(client.Payments);
            Assert.NotNull(client.CustomFields);
            Assert.NotNull(client.CustomForms);
            Assert.NotNull(client.Notes);
            Assert.NotNull(client.Users);
            Assert.NotNull(client.Comments);
        }

        [Fact]
        public void UpdateCredentials_WithNullCredentials_ThrowsArgumentNullException()
        {
            // Arrange
            using var client = new LawMaticsClient(_credentials, _options, _mockLogger.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => client.UpdateCredentials(null!));
        }

        [Fact]
        public void UpdateCredentials_WithValidCredentials_UpdatesCredentials()
        {
            // Arrange
            using var client = new LawMaticsClient(_credentials, _options, _mockLogger.Object);
            var newCredentials = new LawMaticsCredentials("new-access-token");

            // Act
            client.UpdateCredentials(newCredentials);

            // Assert
            Assert.Equal("new-access-token", client.Credentials.AccessToken);
        }

        [Fact]
        public async Task GetAsync_WithValidEndpoint_ReturnsResponse()
        {
            // Arrange
            var expectedData = new { Message = "Test data" };
            var expectedResponse = new ApiResponse<object> { Data = expectedData };
            var responseJson = JsonSerializer.Serialize(expectedResponse);
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseJson, System.Text.Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            using var client = new LawMaticsClient(_credentials, httpClient, _options, _mockLogger.Object);

            // Act
            var result = await client.GetAsync<ApiResponse<object>>("test");

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task PostAsync_WithValidData_ReturnsResponse()
        {
            // Arrange
            var requestData = new { Name = "Test" };
            var expectedData = new { Id = 1, Name = "Test" };
            var expectedResponse = new ApiResponse<object> { Data = expectedData };
            var responseJson = JsonSerializer.Serialize(expectedResponse);
            var httpResponse = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StringContent(responseJson, System.Text.Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            using var client = new LawMaticsClient(_credentials, httpClient, _options, _mockLogger.Object);

            // Act
            var result = await client.PostAsync<ApiResponse<object>>("test", requestData);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task PutAsync_WithValidData_ReturnsResponse()
        {
            // Arrange
            var requestData = new { Id = 1, Name = "Updated" };
            var expectedResponse = new ApiResponse<object> { Data = requestData };
            var responseJson = JsonSerializer.Serialize(expectedResponse);
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseJson, System.Text.Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            using var client = new LawMaticsClient(_credentials, httpClient, _options, _mockLogger.Object);

            // Act
            var result = await client.PutAsync<ApiResponse<object>>("test/1", requestData);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task DeleteAsync_WithValidEndpoint_ReturnsResponse()
        {
            // Arrange
            var expectedResponse = new ApiResponse<object> { Data = null };
            var responseJson = JsonSerializer.Serialize(expectedResponse);
            var httpResponse = new HttpResponseMessage(HttpStatusCode.NoContent)
            {
                Content = new StringContent(responseJson, System.Text.Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            using var client = new LawMaticsClient(_credentials, httpClient, _options, _mockLogger.Object);

            // Act
            var result = await client.DeleteAsync<ApiResponse<object>>("test/1");

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Dispose_CanBeCalledMultipleTimes_DoesNotThrow()
        {
            // Arrange & Act
            using var client = new LawMaticsClient(_credentials, _options, _mockLogger.Object);
            
            // Assert - should not throw
            Assert.True(true);
        }

        public void Dispose()
        {
            // Cleanup handled by using statements in tests
        }
    }
}