using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using LawMatics.SDK.Authentication;
using LawMatics.SDK.Configuration;
using LawMatics.SDK.Clients;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Xunit;

namespace LawMatics.SDK.Tests
{
    /// <summary>
    /// Unit tests for the <see cref="CustomContactTypesClient"/> class.
    /// </summary>
    public class CustomContactTypesClientTests : IDisposable
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly Mock<ILogger<CustomContactTypesClient>> _mockLogger;
        private readonly LawMaticsClientOptions _options;
        private readonly LawMaticsCredentials _credentials;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public CustomContactTypesClientTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _mockLogger = new Mock<ILogger<CustomContactTypesClient>>();
            _options = new LawMaticsClientOptions
            {
                BaseUrl = "https://api.test.lawmatics.com",
                ApiVersion = "v1",
                TimeoutSeconds = 30
            };
            _credentials = new LawMaticsCredentials("test-access-token");
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri(_options.BaseUrl)
            };
            
            // Initialize JSON options to match BaseClient
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };
        }

        [Fact]
        public async Task GetCustomContactTypesAsync_WithValidParameters_ReturnsCustomContactTypes()
        {
            // Arrange
            var client = new CustomContactTypesClient(_httpClient, _options, _mockLogger.Object);
            var expectedResponse = new PagedResponse<CustomContactType>
            {
                Data = new List<CustomContactType>
                {
                    new CustomContactType
                    {
                        Id = 1,
                        Name = "Client",
                        Description = "Regular clients of the firm",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                },
                Pagination = new PaginationInfo
                {
                    CurrentPage = 1,
                    PerPage = 20,
                    Total = 1,
                    TotalPages = 1,
                    HasNext = false
                }
            };

            var apiResponse = new ApiResponse<PagedResponse<CustomContactType>> { Data = expectedResponse };
               var jsonResponse = JsonSerializer.Serialize(apiResponse, _jsonOptions);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonResponse, System.Text.Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => 
                        req.Method == HttpMethod.Get && 
                        req.RequestUri != null &&
                        req.RequestUri.ToString().Contains("custom-contact-types?")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await client.GetCustomContactTypesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
            Assert.Equal(1, result.Data[0].Id);
            Assert.Equal("Client", result.Data[0].Name);
        }

        [Fact]
        public async Task GetCustomContactTypeAsync_WithValidId_ReturnsCustomContactType()
        {
            // Arrange
            var client = new CustomContactTypesClient(_httpClient, _options, _mockLogger.Object);
            var expectedCustomContactType = new CustomContactType
            {
                Id = 1,
                Name = "Client",
                Description = "Regular clients of the firm",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var apiResponse = new ApiResponse<CustomContactType> { Data = expectedCustomContactType };
            var jsonResponse = JsonSerializer.Serialize(apiResponse, _jsonOptions);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonResponse, System.Text.Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => 
                        req.Method == HttpMethod.Get && 
                        req.RequestUri != null &&
                        req.RequestUri.ToString().Contains("custom-contact-types/1")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await client.GetCustomContactTypeAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Client", result.Name);
        }

        [Fact]
        public async Task CreateCustomContactTypeAsync_WithValidRequest_ReturnsCreatedCustomContactType()
        {
            // Arrange
            var client = new CustomContactTypesClient(_httpClient, _options, _mockLogger.Object);
            var request = new CreateCustomContactTypeRequest
            {
                Name = "Prospect",
                Description = "Potential clients"
            };

            var expectedResponse = new CustomContactType
            {
                Id = 2,
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var apiResponse = new ApiResponse<CustomContactType> { Data = expectedResponse };
               var jsonResponse = JsonSerializer.Serialize(apiResponse, _jsonOptions);
            var response = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StringContent(jsonResponse, System.Text.Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => 
                        req.Method == HttpMethod.Post && 
                        req.RequestUri != null &&
                        req.RequestUri.ToString().Contains("custom-contact-types")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await client.CreateCustomContactTypeAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
            Assert.Equal(request.Name, result.Name);
            Assert.Equal(request.Description, result.Description);
        }

        [Fact]
        public async Task UpdateCustomContactTypeAsync_WithValidIdAndRequest_ReturnsUpdatedCustomContactType()
        {
            // Arrange
            var client = new CustomContactTypesClient(_httpClient, _options, _mockLogger.Object);
            var request = new UpdateCustomContactTypeRequest
            {
                Name = "Updated Client Type",
                Description = "Updated description"
            };

            var expectedResponse = new CustomContactType
            {
                Id = 1,
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = DateTime.UtcNow
            };

            var apiResponse = new ApiResponse<CustomContactType> { Data = expectedResponse };
               var jsonResponse = JsonSerializer.Serialize(apiResponse, _jsonOptions);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonResponse, System.Text.Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => 
                        req.Method == HttpMethod.Put && 
                        req.RequestUri != null &&
                        req.RequestUri.ToString().Contains("custom-contact-types/1")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await client.UpdateCustomContactTypeAsync(1, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(request.Name, result.Name);
            Assert.Equal(request.Description, result.Description);
        }

        [Fact]
        public async Task DeleteCustomContactTypeAsync_WithValidId_ReturnsTrue()
        {
            // Arrange
            var client = new CustomContactTypesClient(_httpClient, _options, _mockLogger.Object);
            var response = new HttpResponseMessage(HttpStatusCode.NoContent);

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => 
                        req.Method == HttpMethod.Delete && 
                        req.RequestUri != null &&
                        req.RequestUri.ToString().Contains("custom-contact-types/1")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await client.DeleteCustomContactTypeAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetActiveCustomContactTypesAsync_ReturnsOnlyActiveContactTypes()
        {
            // Arrange
            var client = new CustomContactTypesClient(_httpClient, _options, _mockLogger.Object);
            var expectedResponse = new PagedResponse<CustomContactType>
            {
                Data = new List<CustomContactType>
                {
                    new CustomContactType
                    {
                        Id = 1,
                        Name = "Active Type",
                        Description = "Active custom contact type",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                },
                Pagination = new PaginationInfo
                {
                    CurrentPage = 1,
                    PerPage = 20,
                    Total = 1,
                    TotalPages = 1,
                    HasNext = false
                }
            };

            var apiResponse = new ApiResponse<PagedResponse<CustomContactType>> { Data = expectedResponse };
               var jsonResponse = JsonSerializer.Serialize(apiResponse, _jsonOptions);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonResponse, System.Text.Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => 
                        req.Method == HttpMethod.Get && 
                        req.RequestUri != null &&
                        req.RequestUri.ToString().Contains("custom-contact-types?page=1&page_size=20&is_active=True")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await client.GetActiveCustomContactTypesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
            Assert.Equal("Active Type", result.Data[0].Name);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}