using System;
using System.Threading.Tasks;
using LawMatics.SDK;
using LawMatics.SDK.Authentication;
using LawMatics.SDK.Configuration;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Examples
{
    /// <summary>
    /// Comprehensive example demonstrating all LawMatics SDK functionality.
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== LawMatics C# SDK Example ===");
            Console.WriteLine();

            // Initialize the SDK
            var credentials = new LawMaticsCredentials("your-access-token")
            {
                ClientId = "your-client-id",
                ClientSecret = "your-client-secret",
                RefreshToken = "your-refresh-token"
            };

            var options = new LawMaticsClientOptions
            {
                BaseUrl = "https://api.lawmatics.com",
                ApiVersion = "v1",
                TimeoutSeconds = 30
            };

            // Create the main client
            using var client = new LawMaticsClient(credentials, options);

            try
            {
                // Example 1: Working with Contacts
                Console.WriteLine("=== Contacts ===");
                var contacts = await client.Contacts.GetContactsAsync();
                Console.WriteLine($"Found {contacts.Data?.Count ?? 0} contacts");

                if (contacts.Data?.Count > 0)
                {
                    var firstContact = contacts.Data[0];
                    Console.WriteLine($"First contact: {firstContact.FirstName} {firstContact.LastName}");
                }

                // Create a new contact
                var newContact = await client.Contacts.CreateContactAsync(new CreateContactRequest
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Phone = "555-1234"
                });

                Console.WriteLine($"Created new contact with ID: {newContact.Data?.Id}");
                Console.WriteLine();

                // Example 2: Working with Companies
                Console.WriteLine("=== Companies ===");
                var companies = await client.Companies.GetCompaniesAsync();
                Console.WriteLine($"Found {companies.Data?.Count ?? 0} companies");

                // Create a new company
                var newCompany = await client.Companies.CreateCompanyAsync(new CreateCompanyRequest
                {
                    Name = "Test Corporation",
                    Description = "A test company",
                    Website = "https://testcorp.com"
                });

                Console.WriteLine($"Created new company with ID: {newCompany.Data?.Id}");
                Console.WriteLine();

                // Example 3: Working with Matters
                Console.WriteLine("=== Matters ===");
                var matters = await client.Matters.GetMattersAsync();
                Console.WriteLine($"Found {matters.Data?.Count ?? 0} matters");

                // Create a new matter
                var newMatter = await client.Matters.CreateMatterAsync(new CreateMatterRequest
                {
                    Name = "Test Case",
                    Description = "A test legal matter",
                    Status = "Active",
                    ContactId = newContact.Data?.Id ?? 0
                });

                Console.WriteLine($"Created new matter with ID: {newMatter.Data?.Id}");
                Console.WriteLine();

                // Example 4: Working with Events
                Console.WriteLine("=== Events ===");
                var events = await client.Events.GetEventsAsync();
                Console.WriteLine($"Found {events.Data?.Count ?? 0} events");

                // Create a new event
                var newEvent = await client.Events.CreateEventAsync(new CreateEventRequest
                {
                    Title = "Client Meeting",
                    Description = "Initial consultation with client",
                    StartDate = DateTime.Now.AddDays(1),
                    EndDate = DateTime.Now.AddDays(1).AddHours(1),
                    MatterId = newMatter.Data?.Id ?? 0
                });

                Console.WriteLine($"Created new event with ID: {newEvent.Data?.Id}");
                Console.WriteLine();

                // Example 5: Working with Files and Folders
                Console.WriteLine("=== Files and Folders ===");
                var folders = await client.Folders.GetFoldersAsync();
                Console.WriteLine($"Found {folders.Data?.Count ?? 0} folders");

                var files = await client.Files.GetFilesAsync();
                Console.WriteLine($"Found {files.Data?.Count ?? 0} files");
                Console.WriteLine();

                // Example 6: Working with Email Campaigns
                Console.WriteLine("=== Email Campaigns ===");
                var campaigns = await client.EmailCampaigns.GetEmailCampaignsAsync();
                Console.WriteLine($"Found {campaigns.Data?.Count ?? 0} email campaigns");

                // Example 7: Working with Payments
                Console.WriteLine("=== Payments ===");
                var payments = await client.Payments.GetPaymentsAsync();
                Console.WriteLine($"Found {payments.Data?.Count ?? 0} payments");

                // Example 8: Working with Custom Fields
                Console.WriteLine("=== Custom Fields ===");
                var customFields = await client.CustomFields.GetCustomFieldsAsync();
                Console.WriteLine($"Found {customFields.Data?.Count ?? 0} custom fields");

                // Example 9: Working with Custom Forms
                Console.WriteLine("=== Custom Forms ===");
                var customForms = await client.CustomForms.GetCustomFormsAsync();
                Console.WriteLine($"Found {customForms.Data?.Count ?? 0} custom forms");

                // Example 10: Working with Notes
                Console.WriteLine("=== Notes ===");
                var notes = await client.Notes.GetNotesAsync();
                Console.WriteLine($"Found {notes.Data?.Count ?? 0} notes");

                // Create a note for the contact
                var newNote = await client.Notes.CreateNoteAsync(new CreateNoteRequest
                {
                    Title = "Initial Consultation Note",
                    Content = "Client expressed interest in our services. Follow up scheduled for next week.",
                    EntityType = "Contact",
                    EntityId = newContact.Data?.Id ?? 0
                });

                Console.WriteLine($"Created new note with ID: {newNote.Data?.Id}");
                Console.WriteLine();

                // Example 11: Working with Users
                Console.WriteLine("=== Users ===");
                var users = await client.Users.GetUsersAsync();
                Console.WriteLine($"Found {users.Data?.Count ?? 0} users");

                Console.WriteLine();
                Console.WriteLine("=== All Examples Completed Successfully! ===");
            }
            catch (LawMaticsApiException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                Console.WriteLine($"Error Code: {ex.ErrorCode}");
                if (ex.Details != null)
                {
                    Console.WriteLine("Details:");
                    foreach (var detail in ex.Details)
                    {
                        Console.WriteLine($"  {detail.Key}: {detail.Value}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}