using LawMatics.SDK.Configuration;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing email addresses in the LawMatics API.
    /// </summary>
    public class EmailAddressesClient : BaseClient
    {
        public EmailAddressesClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Gets all email addresses with optional pagination and filtering.
        /// </summary>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="contactId">Filter by contact ID.</param>
        /// <param name="email">Filter by email address.</param>
        /// <param name="type">Filter by email type.</param>
        /// <param name="isPrimary">Filter by primary status.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of email addresses.</returns>
        public async Task<PagedResponse<EmailAddress>> GetEmailAddressesAsync(
            int page = 1,
            int pageSize = 20,
            int? contactId = null,
            string? email = null,
            string? type = null,
            bool? isPrimary = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            if (contactId.HasValue)
                parameters["contact_id"] = contactId.Value.ToString();

            if (!string.IsNullOrEmpty(email))
                parameters["email"] = email;

            if (!string.IsNullOrEmpty(type))
                parameters["type"] = type;

            if (isPrimary.HasValue)
                parameters["is_primary"] = isPrimary.Value.ToString();

            return await GetAsync<PagedResponse<EmailAddress>>("email-addresses", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets a specific email address by ID.
        /// </summary>
        /// <param name="id">The email address ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The email address details.</returns>
        public async Task<EmailAddress?> GetEmailAddressAsync(int id, CancellationToken cancellationToken = default)
        {
            return await GetAsync<EmailAddress>($"email-addresses/{id}", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Creates a new email address.
        /// </summary>
        /// <param name="request">The email address creation request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created email address.</returns>
        public async Task<EmailAddress?> CreateEmailAddressAsync(CreateEmailAddressRequest request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<EmailAddress>("email-addresses", request, cancellationToken);
        }

        /// <summary>
        /// Updates an existing email address.
        /// </summary>
        /// <param name="id">The email address ID.</param>
        /// <param name="request">The email address update request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated email address.</returns>
        public async Task<EmailAddress?> UpdateEmailAddressAsync(int id, UpdateEmailAddressRequest request, CancellationToken cancellationToken = default)
        {
            return await PutAsync<EmailAddress>($"email-addresses/{id}", request, cancellationToken);
        }

        /// <summary>
        /// Deletes an email address.
        /// </summary>
        /// <param name="id">The email address ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if deletion was successful.</returns>
        public async Task<bool> DeleteEmailAddressAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync($"email-addresses/{id}", cancellationToken);
        }

        /// <summary>
        /// Gets email addresses for a specific contact.
        /// </summary>
        /// <param name="contactId">The contact ID.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of email addresses for the contact.</returns>
        public async Task<PagedResponse<EmailAddress>> GetEmailAddressesByContactAsync(
            int contactId,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetEmailAddressesAsync(page, pageSize, contactId: contactId, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the primary email address for a contact.
        /// </summary>
        /// <param name="contactId">The contact ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The primary email address.</returns>
        public async Task<EmailAddress?> GetPrimaryEmailAddressAsync(int contactId, CancellationToken cancellationToken = default)
        {
            return await GetAsync<EmailAddress>($"contacts/{contactId}/primary-email", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Sets an email address as the primary email for a contact.
        /// </summary>
        /// <param name="id">The email address ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated email address.</returns>
        public async Task<EmailAddress?> SetPrimaryEmailAddressAsync(int id, CancellationToken cancellationToken = default)
        {
            return await PostAsync<EmailAddress>($"email-addresses/{id}/set-primary", null, cancellationToken);
        }

        /// <summary>
        /// Validates an email address format.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Validation result.</returns>
        public async Task<EmailValidationResult?> ValidateEmailAddressAsync(string email, CancellationToken cancellationToken = default)
        {
            var request = new { email };
            return await PostAsync<EmailValidationResult>("email-addresses/validate", request, cancellationToken);
        }

        /// <summary>
        /// Checks if an email address is already in use.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <param name="excludeContactId">Exclude this contact ID from the check (optional).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Email availability result.</returns>
        public async Task<EmailAvailabilityResult?> CheckEmailAvailabilityAsync(
            string email,
            int? excludeContactId = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["email"] = email
            };

            if (excludeContactId.HasValue)
                parameters["exclude_contact_id"] = excludeContactId.Value.ToString();

            return await GetAsync<EmailAvailabilityResult>("email-addresses/check-availability", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets email address types.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of email address types.</returns>
        public async Task<List<string>> GetEmailAddressTypesAsync(CancellationToken cancellationToken = default)
        {
            var response = await GetAsync<EmailAddressTypesResponse>("email-addresses/types", cancellationToken: cancellationToken);
            return response?.Types ?? new List<string>();
        }

        /// <summary>
        /// Searches email addresses by email address.
        /// </summary>
        /// <param name="email">Email address to search for.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of email addresses matching the search.</returns>
        public async Task<PagedResponse<EmailAddress>> SearchEmailAddressesAsync(
            string email,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetEmailAddressesAsync(page, pageSize, email: email, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Bulk creates email addresses for a contact.
        /// </summary>
        /// <param name="contactId">The contact ID.</param>
        /// <param name="requests">List of email address creation requests.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of created email addresses.</returns>
        public async Task<List<EmailAddress>> BulkCreateEmailAddressesAsync(
            int contactId,
            List<CreateEmailAddressRequest> requests,
            CancellationToken cancellationToken = default)
        {
            var bulkRequest = new
            {
                contact_id = contactId,
                email_addresses = requests
            };

            return await PostAsync<List<EmailAddress>>("email-addresses/bulk", bulkRequest, cancellationToken) ?? new List<EmailAddress>();
        }

        /// <summary>
        /// Bulk deletes email addresses for a contact.
        /// </summary>
        /// <param name="contactId">The contact ID.</param>
        /// <param name="emailIds">List of email address IDs to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if bulk deletion was successful.</returns>
        public async Task<bool> BulkDeleteEmailAddressesAsync(
            int contactId,
            List<int> emailIds,
            CancellationToken cancellationToken = default)
        {
            var bulkRequest = new
            {
                contact_id = contactId,
                email_ids = emailIds
            };

            var response = await PostAsync<object>("email-addresses/bulk-delete", bulkRequest, cancellationToken);
            return response != null;
        }

        /// <summary>
        /// Exports email addresses to CSV.
        /// </summary>
        /// <param name="contactId">Filter by contact ID (optional).</param>
        /// <param name="type">Filter by email type (optional).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The CSV file content.</returns>
        public async Task<byte[]> ExportEmailAddressesToCsvAsync(
            int? contactId = null,
            string? type = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();

            if (contactId.HasValue)
                parameters["contact_id"] = contactId.Value.ToString();

            if (!string.IsNullOrEmpty(type))
                parameters["type"] = type;

            parameters["format"] = "csv";

            return await GetFileAsync("email-addresses/export", parameters, cancellationToken);
        }
    }

    /// <summary>
    /// Response model for email address types.
    /// </summary>
    public class EmailAddressTypesResponse
    {
        /// <summary>
        /// Gets or sets the list of email address types.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("types")]
        public List<string> Types { get; set; } = new();
    }

    

    /// <summary>
    /// Represents email availability result.
    /// </summary>
    public class EmailAvailabilityResult
    {
        /// <summary>
        /// Gets or sets whether the email is available.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("is_available")]
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Gets or sets the contact ID using the email (if not available).
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("contact_id")]
        public int? ContactId { get; set; }

        /// <summary>
        /// Gets or sets the contact name using the email (if not available).
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("contact_name")]
        public string? ContactName { get; set; }

        /// <summary>
        /// Gets or sets message about availability.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}