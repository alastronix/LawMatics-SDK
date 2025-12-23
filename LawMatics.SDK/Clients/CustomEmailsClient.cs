using LawMatics.SDK.Configuration;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing custom emails in the LawMatics API.
    /// </summary>
    public class CustomEmailsClient : BaseClient
    {
        public CustomEmailsClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Gets all custom emails with optional pagination and filtering.
        /// </summary>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="category">Filter by email category.</param>
        /// <param name="isActive">Filter by active status.</param>
        /// <param name="search">Search in name, subject, or body.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of custom emails.</returns>
        public async Task<PagedResponse<CustomEmail>> GetCustomEmailsAsync(
            int page = 1,
            int pageSize = 20,
            string? category = null,
            bool? isActive = null,
            string? search = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            if (!string.IsNullOrEmpty(category))
                parameters["category"] = category;

            if (isActive.HasValue)
                parameters["is_active"] = isActive.Value.ToString();

            if (!string.IsNullOrEmpty(search))
                parameters["search"] = search;

            return await GetAsync<PagedResponse<CustomEmail>>("custom-emails", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets a specific custom email by ID.
        /// </summary>
        /// <param name="id">The custom email ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The custom email details.</returns>
        public async Task<CustomEmail?> GetCustomEmailAsync(int id, CancellationToken cancellationToken = default)
        {
            return await GetAsync<CustomEmail>($"custom-emails/{id}", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Creates a new custom email.
        /// </summary>
        /// <param name="request">The custom email creation request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created custom email.</returns>
        public async Task<CustomEmail?> CreateCustomEmailAsync(CreateCustomEmailRequest request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<CustomEmail>("custom-emails", request, cancellationToken);
        }

        /// <summary>
        /// Updates an existing custom email.
        /// </summary>
        /// <param name="id">The custom email ID.</param>
        /// <param name="request">The custom email update request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated custom email.</returns>
        public async Task<CustomEmail?> UpdateCustomEmailAsync(int id, UpdateCustomEmailRequest request, CancellationToken cancellationToken = default)
        {
            return await PutAsync<CustomEmail>($"custom-emails/{id}", request, cancellationToken);
        }

        /// <summary>
        /// Deletes a custom email.
        /// </summary>
        /// <param name="id">The custom email ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if deletion was successful.</returns>
        public async Task<bool> DeleteCustomEmailAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync($"custom-emails/{id}", cancellationToken);
        }

        /// <summary>
        /// Sends a test email using a custom email template.
        /// </summary>
        /// <param name="id">The custom email ID.</param>
        /// <param name="testEmail">The email address to send the test to.</param>
        /// <param name="variables">Optional variables for template substitution.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The result of the test send operation.</returns>
        public async Task<EmailTestResult?> SendTestEmailAsync(
            int id,
            string testEmail,
            Dictionary<string, object>? variables = null,
            CancellationToken cancellationToken = default)
        {
            var request = new
            {
                test_email = testEmail,
                variables = variables ?? new Dictionary<string, object>()
            };

            return await PostAsync<EmailTestResult>($"custom-emails/{id}/test", request, cancellationToken);
        }

        /// <summary>
        /// Previews a custom email with optional variable substitution.
        /// </summary>
        /// <param name="id">The custom email ID.</param>
        /// <param name="variables">Optional variables for template substitution.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The email preview.</returns>
        public async Task<CustomEmailPreview?> PreviewEmailAsync(
            int id,
            Dictionary<string, object>? variables = null,
            CancellationToken cancellationToken = default)
        {
            var request = new
            {
                variables = variables ?? new Dictionary<string, object>()
            };

            return await PostAsync<CustomEmailPreview>($"custom-emails/{id}/preview", request, cancellationToken);
        }

        /// <summary>
        /// Duplicates an existing custom email.
        /// </summary>
        /// <param name="id">The custom email ID to duplicate.</param>
        /// <param name="newName">The name for the duplicated email.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The duplicated custom email.</returns>
        public async Task<CustomEmail?> DuplicateCustomEmailAsync(
            int id,
            string newName,
            CancellationToken cancellationToken = default)
        {
            var request = new { name = newName };
            return await PostAsync<CustomEmail>($"custom-emails/{id}/duplicate", request, cancellationToken);
        }

        /// <summary>
        /// Gets custom emails by category.
        /// </summary>
        /// <param name="category">The email category.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of custom emails in the specified category.</returns>
        public async Task<PagedResponse<CustomEmail>> GetCustomEmailsByCategoryAsync(
            string category,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetCustomEmailsAsync(page, pageSize, category: category, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets active custom emails.
        /// </summary>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of active custom emails.</returns>
        public async Task<PagedResponse<CustomEmail>> GetActiveCustomEmailsAsync(
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetCustomEmailsAsync(page, pageSize, isActive: true, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Activates a custom email.
        /// </summary>
        /// <param name="id">The custom email ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated custom email.</returns>
        public async Task<CustomEmail?> ActivateCustomEmailAsync(int id, CancellationToken cancellationToken = default)
        {
            return await PostAsync<CustomEmail>($"custom-emails/{id}/activate", null, cancellationToken);
        }

        /// <summary>
        /// Deactivates a custom email.
        /// </summary>
        /// <param name="id">The custom email ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated custom email.</returns>
        public async Task<CustomEmail?> DeactivateCustomEmailAsync(int id, CancellationToken cancellationToken = default)
        {
            return await PostAsync<CustomEmail>($"custom-emails/{id}/deactivate", null, cancellationToken);
        }

        /// <summary>
        /// Gets available email categories.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of email categories.</returns>
        public async Task<List<string>> GetEmailCategoriesAsync(CancellationToken cancellationToken = default)
        {
            var response = await GetAsync<EmailCategoriesResponse>("custom-emails/categories", cancellationToken: cancellationToken);
            return response?.Categories ?? new List<string>();
        }

        /// <summary>
        /// Searches custom emails by keyword.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of custom emails matching the search query.</returns>
        public async Task<PagedResponse<CustomEmail>> SearchCustomEmailsAsync(
            string query,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetCustomEmailsAsync(page, pageSize, search: query, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Validates a custom email template syntax.
        /// </summary>
        /// <param name="subject">Email subject.</param>
        /// <param name="body">Email body (HTML).</param>
        /// <param name="bodyText">Email body (plain text).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Validation result.</returns>
        public async Task<EmailValidationResult?> ValidateEmailTemplateAsync(
            string subject,
            string body,
            string? bodyText = null,
            CancellationToken cancellationToken = default)
        {
            var request = new
            {
                subject,
                body,
                body_text = bodyText
            };

            return await PostAsync<EmailValidationResult>("custom-emails/validate", request, cancellationToken);
        }

        /// <summary>
        /// Gets the merge variables available for email templates.
        /// </summary>
        /// <param name="context">The context (e.g., 'matter', 'contact', 'company').</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of available merge variables.</returns>
        public async Task<List<MergeVariable>> GetMergeVariablesAsync(
            string context,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["context"] = context
            };

            var response = await GetAsync<MergeVariablesResponse>("custom-emails/merge-variables", parameters, cancellationToken);
            return response?.Variables ?? new List<MergeVariable>();
        }
    }

    /// <summary>
    /// Response model for email categories.
    /// </summary>
    public class EmailCategoriesResponse
    {
        /// <summary>
        /// Gets or sets the list of email categories.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("categories")]
        public List<string> Categories { get; set; } = new();
    }

    /// <summary>
    /// Represents the result of an email test.
    /// </summary>
    public class EmailTestResult
    {
        /// <summary>
        /// Gets or sets whether the test was successful.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email ID that was sent.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("email_id")]
        public int? EmailId { get; set; }

        /// <summary>
        /// Gets or sets any errors that occurred.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("errors")]
        public List<string>? Errors { get; set; }
    }

    /// <summary>
    /// Represents email validation result.
    /// </summary>
    public class EmailValidationResult
    {
        /// <summary>
        /// Gets or sets whether the template is valid.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("is_valid")]
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets validation errors.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("errors")]
        public List<string> Errors { get; set; } = new();

        /// <summary>
        /// Gets or sets validation warnings.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("warnings")]
        public List<string> Warnings { get; set; } = new();

        /// <summary>
        /// Gets or sets found merge variables.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("found_variables")]
        public List<string> FoundVariables { get; set; } = new();

        /// <summary>
        /// Gets or sets undefined variables.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("undefined_variables")]
        public List<string> UndefinedVariables { get; set; } = new();
    }

    /// <summary>
    /// Response model for merge variables.
    /// </summary>
    public class MergeVariablesResponse
    {
        /// <summary>
        /// Gets or sets the list of merge variables.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("variables")]
        public List<MergeVariable> Variables { get; set; } = new();
    }

    /// <summary>
    /// Represents a merge variable for email templates.
    /// </summary>
    public class MergeVariable
    {
        /// <summary>
        /// Gets or sets the variable name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the variable description.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the variable type.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets example value.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("example")]
        public string Example { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether the variable is required.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("required")]
        public bool Required { get; set; }
    }
}