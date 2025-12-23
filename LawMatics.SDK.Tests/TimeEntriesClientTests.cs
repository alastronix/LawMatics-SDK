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
    /// Unit tests for the <see cref="TimeEntriesClient"/> class.
    /// </summary>
    public class TimeEntriesClientTests : IDisposable
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly Mock<ILogger<TimeEntriesClient>> _mockLogger;
        private readonly LawMaticsClientOptions _options;
        private readonly LawMaticsCredentials _credentials;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;


        public TimeEntriesClientTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _mockLogger = new Mock<ILogger<TimeEntriesClient>>();
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
        public async Task GetTimeEntriesAsync_WithValidParameters_ReturnsTimeEntries()
        {
            // Arrange
            var client = new TimeEntriesClient(_httpClient, _options, _mockLogger.Object);
            var expectedResponse = new PagedResponse<TimeEntry>
            {
                Data = new List<TimeEntry>
                {
                    new TimeEntry
                    {
                        Id = 1,
                        Description = "Client consultation call",
                        Hours = 1.5m,
                        Rate = 250.00m,
                        Total = 375.00m,
                        Date = DateTime.UtcNow,
                        MatterId = 1,
                        UserId = 1,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
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

            var apiResponse = new ApiResponse<PagedResponse<TimeEntry>> { Data = expectedResponse };
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
                        req.RequestUri.ToString().Contains("time-entries?")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await client.GetTimeEntriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
            Assert.Equal(1, result.Data[0].Id);
            Assert.Equal("Client consultation call", result.Data[0].Description);
        }

        [Fact]
        public async Task GetTimeEntryAsync_WithValidId_ReturnsTimeEntry()
        {
            // Arrange
            var client = new TimeEntriesClient(_httpClient, _options, _mockLogger.Object);
            var expectedTimeEntry = new TimeEntry
            {
                Id = 1,
                Description = "Client consultation call",
                Hours = 1.5m,
                Rate = 250.00m,
                Total = 375.00m,
                Date = DateTime.UtcNow,
                MatterId = 1,
                UserId = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var apiResponse = new ApiResponse<TimeEntry> { Data = expectedTimeEntry };

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
                        req.RequestUri.ToString().Contains("time-entries/1")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await client.GetTimeEntryAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Client consultation call", result.Description);
            Assert.Equal(1.5m, result.Hours);
        }

        [Fact]
        public async Task CreateTimeEntryAsync_WithValidRequest_ReturnsCreatedTimeEntry()
        {
            // Arrange
            var client = new TimeEntriesClient(_httpClient, _options, _mockLogger.Object);
            var request = new CreateTimeEntryRequest
            {
                Description = "Document review",
                Hours = 2.0m,
                Rate = 300.00m,
                Date = DateTime.UtcNow,
                MatterId = 1,
                UserId = 1
            };

            var expectedResponse = new TimeEntry
            {
                Id = 2,
                Description = request.Description,
                Hours = request.Hours,
                Rate = request.Rate,
                Total = request.Hours * request.Rate,
                Date = request.Date,
                MatterId = request.MatterId,
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
            };

            var apiResponse = new ApiResponse<TimeEntry> { Data = expectedResponse };
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
                        req.RequestUri.ToString().Contains("time-entries")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await client.CreateTimeEntryAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
            Assert.Equal(request.Description, result.Description);
            Assert.Equal(request.Hours, result.Hours);
        }

        [Fact]
        public async Task UpdateTimeEntryAsync_WithValidIdAndRequest_ReturnsUpdatedTimeEntry()
        {
            // Arrange
            var client = new TimeEntriesClient(_httpClient, _options, _mockLogger.Object);
            var request = new UpdateTimeEntryRequest
            {
                Description = "Updated consultation call",
                Hours = 2.0m,
                Rate = 275.00m
            };

            var expectedResponse = new TimeEntry
            {
                Id = 1,
                Description = request.Description,
                Hours = request.Hours ?? 0m,
                Rate = request.Rate ?? 0m,
                Total = (request.Hours ?? 0m) * (request.Rate ?? 0m),
                Date = DateTime.UtcNow.AddDays(-1),
                MatterId = 1,
                UserId = 1,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = DateTime.UtcNow
            };

            var apiResponse = new ApiResponse<TimeEntry> { Data = expectedResponse };
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
                        req.RequestUri.ToString().Contains("time-entries/1")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await client.UpdateTimeEntryAsync(1, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(request.Description, result.Description);
            Assert.Equal(request.Hours, result.Hours);
        }

        [Fact]
        public async Task DeleteTimeEntryAsync_WithValidId_ReturnsTrue()
        {
            // Arrange
            var client = new TimeEntriesClient(_httpClient, _options, _mockLogger.Object);
            var response = new HttpResponseMessage(HttpStatusCode.NoContent);

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => 
                        req.Method == HttpMethod.Delete && 
                        req.RequestUri != null &&
                        req.RequestUri.ToString().Contains("time-entries/1")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await client.DeleteTimeEntryAsync(1);

            // Assert
            Assert.True(result);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}