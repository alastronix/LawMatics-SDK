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
    /// Unit tests for the <see cref="CommentsClient"/> class.
    /// </summary>
    public class CommentsClientTests : IDisposable
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly Mock<ILogger<CommentsClient>> _mockLogger;
        private readonly LawMaticsClientOptions _options;
        private readonly LawMaticsCredentials _credentials;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;


        public CommentsClientTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _mockLogger = new Mock<ILogger<CommentsClient>>();
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
        public async Task GetCommentsAsync_WithValidParameters_ReturnsComments()
        {
            // Arrange
            var expectedComments = new List<Comment>
            {
                new Comment
                {
                    Id = 1,
                    EntityType = "contact",
                    EntityId = 123,
                    Content = "Test comment 1",
                    CreatedById = 456,
                    CreatedByName = "Test User",
                    CreatedAt = DateTime.UtcNow,
                    IsInternal = false,
                    IsDeleted = false
                }
            };

            var apiResponse = new ApiResponse<List<Comment>> { Data = expectedComments };
            var responseJson = JsonSerializer.Serialize(apiResponse, _jsonOptions);
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseJson, System.Text.Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString().Contains("comments?")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var client = new CommentsClient(_httpClient, _options, _mockLogger.Object);

            // Act
            var result = await client.GetCommentsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Test comment 1", result[0].Content);
        }

        [Fact]
        public async Task GetCommentAsync_WithValidId_ReturnsComment()
        {
            // Arrange
            var expectedComment = new Comment
            {
                Id = 1,
                EntityType = "contact",
                EntityId = 123,
                Content = "Test comment",
                CreatedById = 456,
                CreatedByName = "Test User",
                CreatedAt = DateTime.UtcNow,
                IsInternal = false,
                IsDeleted = false
            };

            var apiResponse = new ApiResponse<Comment> { Data = expectedComment };
            var responseJson = JsonSerializer.Serialize(apiResponse, _jsonOptions);
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseJson, System.Text.Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString().Contains("comments/1")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var client = new CommentsClient(_httpClient, _options, _mockLogger.Object);

            // Act
            var result = await client.GetCommentAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test comment", result.Content);
            Assert.Equal("contact", result.EntityType);
            Assert.Equal(123, result.EntityId);
        }

        [Fact]
        public async Task CreateCommentAsync_WithValidRequest_ReturnsCreatedComment()
        {
            // Arrange
            var request = new CreateCommentRequest
            {
                EntityType = "matter",
                EntityId = 456,
                Comment = "New test comment",
                IsInternal = true
            };

            var expectedComment = new Comment
            {
                Id = 2,
                EntityType = request.EntityType,
                EntityId = request.EntityId,
                Content = request.Comment,
                IsInternal = request.IsInternal,
                CreatedAt = DateTime.UtcNow
            };

            var apiResponse = new ApiResponse<Comment> { Data = expectedComment };
            var responseJson = JsonSerializer.Serialize(apiResponse, _jsonOptions);
            var httpResponse = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StringContent(responseJson, System.Text.Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri.ToString().Contains("comments")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var client = new CommentsClient(_httpClient, _options, _mockLogger.Object);

            // Act
            var result = await client.CreateCommentAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New test comment", result.Content);
            Assert.Equal("matter", result.EntityType);
            Assert.Equal(456, result.EntityId);
            Assert.True(result.IsInternal);
        }

        [Fact]
        public async Task DeleteCommentAsync_WithValidId_ReturnsTrue()
        {
            // Arrange
            var apiResponse = new ApiResponse<object> { Data = null };
            var responseJson = JsonSerializer.Serialize(apiResponse, _jsonOptions);
            var httpResponse = new HttpResponseMessage(HttpStatusCode.NoContent)
            {
                Content = new StringContent(responseJson, System.Text.Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Delete && req.RequestUri.ToString().Contains("comments/1")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var client = new CommentsClient(_httpClient, _options, _mockLogger.Object);

            // Act
            var result = await client.DeleteCommentAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task BulkDeleteCommentsAsync_WithValidIds_ReturnsTrue()
        {
            // Arrange
            var commentIds = new List<int> { 1, 2, 3 };
            var apiResponse = new ApiResponse<object> { Data = null };
            var responseJson = JsonSerializer.Serialize(apiResponse, _jsonOptions);
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseJson, System.Text.Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri.ToString().Contains("comments/bulk/delete")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var client = new CommentsClient(_httpClient, _options, _mockLogger.Object);

            // Act
            var result = await client.BulkDeleteCommentsAsync(commentIds);

            // Assert
            Assert.NotNull(result);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}