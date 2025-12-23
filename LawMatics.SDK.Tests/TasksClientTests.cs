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
    /// Unit tests for the <see cref="TasksClient"/> class.
    /// </summary>
    public class TasksClientTests : IDisposable
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly Mock<ILogger<TasksClient>> _mockLogger;
        private readonly LawMaticsClientOptions _options;
        private readonly LawMaticsCredentials _credentials;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;


        public TasksClientTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _mockLogger = new Mock<ILogger<TasksClient>>();
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
        public async Task GetTasksAsync_WithValidParameters_ReturnsTasks()
        {
            // Arrange
            var client = new TasksClient(_httpClient, _options, _mockLogger.Object);
            var expectedResponse = new PagedResponse<TaskItem>
            {
                Data = new List<TaskItem>
                {
                    new TaskItem
                    {
                        Id = 1,
                        Title = "Review Contract",
                        Description = "Review the new client contract",
                        DueDate = DateTime.UtcNow.AddDays(7),
                        Priority = "high",
                        StatusId = 1,
                        AssignedToId = 1,
                        MatterId = 1,
                        ContactId = 1,
                        CreatedAt = DateTime.UtcNow
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

            var apiResponse = new ApiResponse<PagedResponse<TaskItem>> { Data = expectedResponse };
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
                        req.RequestUri.ToString().Contains("tasks?")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await client.GetTasksAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
            Assert.Equal(1, result.Data[0].Id);
            Assert.Equal("Review Contract", result.Data[0].Title);
        }

        [Fact]
        public async Task GetTaskAsync_WithValidId_ReturnsTask()
        {
            // Arrange
            var client = new TasksClient(_httpClient, _options, _mockLogger.Object);
            var expectedResponse = new TaskItem
            {
                Id = 1,
                Title = "Review Contract",
                Description = "Review the new client contract",
                DueDate = DateTime.UtcNow.AddDays(7),
                Priority = "high",
                StatusId = 1,
                AssignedToId = 1,
                MatterId = 1,
                ContactId = 1,
                CreatedAt = DateTime.UtcNow
            };

            var apiResponse = new ApiResponse<TaskItem> { Data = expectedResponse };
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
                        req.RequestUri.ToString().Contains("tasks/1")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await client.GetTaskAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Review Contract", result.Title);
            Assert.Equal("high", result.Priority);
        }

        [Fact]
        public async Task CreateTaskAsync_WithValidRequest_ReturnsCreatedTask()
        {
            // Arrange
            var client = new TasksClient(_httpClient, _options, _mockLogger.Object);
            var request = new CreateTaskRequest
            {
                Title = "New Task",
                Description = "Task description",
                DueDate = DateTime.UtcNow.AddDays(3),
                Priority = "medium",
                StatusId = 1,
                AssignedToId = 1,
                MatterId = 1,
                ContactId = 1
            };

            var expectedResponse = new TaskItem
            {
                Id = 2,
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                Priority = request.Priority,
                StatusId = request.StatusId,
                AssignedToId = request.AssignedToId,
                MatterId = request.MatterId,
                ContactId = request.ContactId,
                CreatedAt = DateTime.UtcNow
            };

            var apiResponse = new ApiResponse<TaskItem> { Data = expectedResponse };
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
                        req.RequestUri.ToString().Contains("tasks")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await client.CreateTaskAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
            Assert.Equal(request.Title, result.Title);
            Assert.Equal(request.Priority, result.Priority);
        }

        [Fact]
        public async Task UpdateTaskAsync_WithValidIdAndRequest_ReturnsUpdatedTask()
        {
            // Arrange
            var client = new TasksClient(_httpClient, _options, _mockLogger.Object);
            var request = new UpdateTaskRequest
            {
                Title = "Updated Task Title",
                Description = "Updated description",
                Priority = "urgent",
                DueDate = DateTime.UtcNow.AddDays(1)
            };

            var expectedResponse = new TaskItem
            {
                Id = 1,
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority,
                DueDate = request.DueDate,
                StatusId = 1,
                AssignedToId = 1,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = DateTime.UtcNow
            };

            var apiResponse = new ApiResponse<TaskItem> { Data = expectedResponse };
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
                        req.RequestUri.ToString().Contains("tasks/1")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await client.UpdateTaskAsync(1, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(request.Title, result.Title);
            Assert.Equal(request.Priority, result.Priority);
        }

        [Fact]
        public async Task DeleteTaskAsync_WithValidId_ReturnsTrue()
        {
            // Arrange
            var client = new TasksClient(_httpClient, _options, _mockLogger.Object);
            var response = new HttpResponseMessage(HttpStatusCode.NoContent);

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => 
                        req.Method == HttpMethod.Delete && 
                        req.RequestUri != null &&
                        req.RequestUri.ToString().Contains("tasks/1")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await client.DeleteTaskAsync(1);

            // Assert
            Assert.True(result);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}