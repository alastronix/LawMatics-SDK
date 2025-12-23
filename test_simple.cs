using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using LawMatics.SDK.Clients;
using LawMatics.SDK.Configuration;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;

class Program
{
    static async Task Main()
    {
        // Setup exactly like the test
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var mockLogger = new Mock<ILogger<EventLocationsClient>>();
        var options = new LawMaticsClientOptions
        {
            BaseUrl = "https://api.test.lawmatics.com",
            ApiVersion = "v1",
            TimeoutSeconds = 30
        };
        var httpClient = new HttpClient(mockHttpMessageHandler.Object)
        {
            BaseAddress = new Uri(options.BaseUrl)
        };

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };

        var expectedResponse = new EventLocation
        {
            Id = 1,
            Name = "Main Conference Room",
            Address = "123 Main St, Suite 100",
            Capacity = 50,
            Description = "Large conference room with AV equipment",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var jsonResponse = JsonSerializer.Serialize(expectedResponse, jsonOptions);
        Console.WriteLine($"JSON Response: {jsonResponse}");

        var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            Content = new System.Net.Http.StringContent(jsonResponse, System.Text.Encoding.UTF8, "application/json")
        };

        // Setup mock to match any GET request to event-locations
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => 
                    req.Method == HttpMethod.Get),
                ItExpr.IsAny<System.Threading.CancellationToken>())
            .ReturnsAsync(response);

        var client = new EventLocationsClient(httpClient, options, mockLogger.Object);
        
        try
        {
            var result = await client.GetEventLocationAsync(1);
            Console.WriteLine($"Success! Result: {result?.Name}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Inner: {ex.InnerException?.Message}");
        }
    }
}