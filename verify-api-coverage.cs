using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class ApiCoverageVerifier
{
    public static void Main()
    {
        Console.WriteLine("=== LawMatics SDK API Coverage Verification ===\n");
        
        // Official API sections from documentation
        var documentedSections = new List<string>
        {
            "Addresses", "Contacts", "Companies", "CustomEmails", "CustomFields", 
            "CustomForms", "EmailAddresses", "EmailCampaigns", "EmailCampaignStats",
            "Events", "EventLocations", "EventTypes", "Files", "Folders", 
            "Matters", "Notes", "Payments", "Users", "Tasks", "Expenses", 
            "TimeEntries", "TaskStatuses", "Subtasks", "CustomContactTypes",
            "MatterSubStatuses", "CompanyFinder"
        };
        
        // Get all client files
        var clientFiles = Directory.GetFiles("LawMatics.SDK/Clients", "*.cs")
            .Select(Path.GetFileNameWithoutExtension)
            .Where(name => name != "BaseClient")
            .ToList();
        
        Console.WriteLine($"Documented API Sections: {documentedSections.Count}");
        Console.WriteLine($"Implemented Client Files: {clientFiles.Count}\n");
        
        // Check coverage
        var coveredSections = new List<string>();
        var missingSections = new List<string>();
        
        foreach (var section in documentedSections)
        {
            var expectedClient = section + "Client";
            if (clientFiles.Contains(expectedClient))
            {
                coveredSections.Add(section);
                Console.WriteLine($"✅ {section} -> {expectedClient}");
            }
            else
            {
                missingSections.Add(section);
                Console.WriteLine($"❌ {section} -> MISSING: {expectedClient}");
            }
        }
        
        // Check for any extra implementations
        var extraImplementations = clientFiles
            .Where(client => !documentedSections.Any(section => section + "Client" == client))
            .ToList();
        
        Console.WriteLine($"\n=== Coverage Summary ===");
        Console.WriteLine($"Coverage: {coveredSections.Count}/{documentedSections.Count} ({(double)coveredSections.Count/documentedSections.Count*100:F1}%)");
        
        if (missingSections.Any())
        {
            Console.WriteLine($"\n❌ Missing Sections ({missingSections.Count}):");
            foreach (var missing in missingSections)
                Console.WriteLine($"   - {missing}");
        }
        
        if (extraImplementations.Any())
        {
            Console.WriteLine($"\n➕ Extra Implementations ({extraImplementations.Count}):");
            foreach (var extra in extraImplementations)
                Console.WriteLine($"   - {extra}");
        }
        
        // Verify each client has comprehensive CRUD operations
        Console.WriteLine($"\n=== CRUD Operations Verification ===");
        foreach (var clientFile in clientFiles)
        {
            var filePath = $"LawMatics.SDK/Clients/{clientFile}.cs";
            if (File.Exists(filePath))
            {
                var content = File.ReadAllText(filePath);
                var hasGetAsync = content.Contains("GetAsync");
                var hasPostAsync = content.Contains("PostAsync");
                var hasPutAsync = content.Contains("PutAsync");
                var hasDeleteAsync = content.Contains("DeleteAsync");
                
                var crudCount = new[] { hasGetAsync, hasPostAsync, hasPutAsync, hasDeleteAsync }.Count(b => b);
                Console.WriteLine($"{clientFile}: {crudCount}/4 CRUD operations");
            }
        }
        
        Console.WriteLine($"\n=== Verification Complete ===");
    }
}