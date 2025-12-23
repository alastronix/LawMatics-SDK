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
    /// Unit tests for the <see cref="EventTypesClient"/> class.
    /// </summary>
    public class EventTypesClientTests : IDisposable
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly Mock<ILogger<EventTypesClient>> _mockLogger;
        private readonly LawMaticsClientOptions _options;
        private readonly LawMaticsCredentials _credentials;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;


        public EventTypesClientTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _mockLogger = new Mock<ILogger<EventTypesClient>>();
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
        public async Task GetEventTypesAsync_WithValidParameters_ReturnsEventTypes()
        {
            // Arrange
            var client = new EventTypesClient(_httpClient, _options, _mockLogger.Object);
            var expectedResponse = new PagedResponse<EventType>
            {
                Data = new List<EventType>
                {
                    new EventType
                    {
                        Id = 1,
                        Name = "Client Consultation",
                        Description = "Initial consultation with new client",
                        Color = "#FF5733",
                        DefaultDuration = 60,
                        CreatedAt = DateTime.UtcNow
                    ,
                        UpdatedAt = DateTime.UtcNow}
                },
                Pagination = new PaginationInfo
{ 
                    CurrentPage = 1,
                    PerPage = 20,
                    Total = 1
                ,
                        TotalPages = 1,
                        HasNext = false
                        }
            };

            var apiResponse = new ApiResponse<PagedResponse<EventType>> { Data = expectedResponse };
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
                        req.RequestUri.ToString().Contains("event-types?")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await client.GetEventTypesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
            Assert.Equal(1, result.Data[0].Id);
            Assert.Equal("Client Consultation", result.Data[0].Name);
        }

        [Fact]
        public async Task GetEventTypeAsync_WithValidId_ReturnsEventType()
        {
            // Arrange
            var client = new EventTypesClient(_httpClient, _options, _mockLogger.Object);
            var expectedResponse = new EventType
            {
                Id = 1,
                Name = "Client Consultation",
                Description = "Initial consultation with new client",
                Color = "#FF5733",
                DefaultDuration = 60,
                CreatedAt = DateTime.UtcNow
            ,
                        UpdatedAt = DateTime.UtcNow};

            var apiResponse = new ApiResponse<EventType> { Data = expectedResponse };
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
                        req.RequestUri.ToString().Contains("event-types/1")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await client.GetEventTypeAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Client Consultation", result.Name);
            Assert.Equal(60, result.DefaultDuration);
        }

        [Fact]
        public async Task DeleteEventTypeAsync_WithValidId_ReturnsTrue()
        {
            // Arrange
            var client = new EventTypesClient(_httpClient, _options, _mockLogger.Object);
            var response = new HttpResponseMessage(HttpStatusCode.NoContent);

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => 
                        req.Method == HttpMethod.Delete && 
                        req.RequestUri != null &&
                        req.RequestUri.ToString().Contains("event-types/1")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await client.DeleteEventTypeAsync(1);

            // Assert
            Assert.True(result);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}