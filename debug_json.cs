using System;
using System.Text.Json;
using LawMatics.SDK.Models;

var jsonOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
    Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
};

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

var json = JsonSerializer.Serialize(expectedResponse, jsonOptions);
Console.WriteLine(json);