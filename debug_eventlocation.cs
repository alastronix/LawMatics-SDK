using System;
using System.Text.Json;
using LawMatics.SDK.Models;

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

var json = JsonSerializer.Serialize(expectedResponse, jsonOptions);
Console.WriteLine("Generated JSON:");
Console.WriteLine(json);

try 
{
    var deserialized = JsonSerializer.Deserialize<EventLocation>(json, jsonOptions);
    Console.WriteLine("\nDeserialization successful!");
    Console.WriteLine($"Id: {deserialized?.Id}");
    Console.WriteLine($"Name: {deserialized?.Name}");
}
catch (Exception ex)
{
    Console.WriteLine($"\nDeserialization failed: {ex.Message}");
}