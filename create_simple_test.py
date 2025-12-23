#!/usr/bin/env python3

import subprocess
import json

def create_debug_test():
    """Create a simple test to debug the JSON issue"""
    
    test_code = '''
using System;
using System.Net.Http;
using System.Text.Json;
using LawMatics.SDK.Models;

public class DebugTest
{
    public static void Main()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };

        var testResponse = new PagedResponse<CustomContactType>
        {
            Data = new List<CustomContactType>
            {
                new CustomContactType
                {
                    Id = 1,
                    Name = "Test",
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

        var json = JsonSerializer.Serialize(testResponse, options);
        Console.WriteLine("Generated JSON:");
        Console.WriteLine(json);
        Console.WriteLine();

        // Try to deserialize it back
        try
        {
            var deserialized = JsonSerializer.Deserialize<PagedResponse<CustomContactType>>(json, options);
            Console.WriteLine("Deserialization successful!");
            Console.WriteLine($"Data count: {deserialized?.Data?.Count ?? 0}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Deserialization failed: {ex.Message}");
        }
    }
}
'''
    
    with open('/workspace/debug_test.cs', 'w') as f:
        f.write(test_code)
    
    print("Debug test created at /workspace/debug_test.cs")

if __name__ == "__main__":
    create_debug_test()