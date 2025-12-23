using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LawMatics.SDK.Configuration;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing custom forms in the LawMatics API.
    /// </summary>
    public class CustomFormsClient : BaseClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFormsClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="options">The client options.</param>
        /// <param name="logger">The optional logger.</param>
        public CustomFormsClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger = null)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Gets a list of custom forms with optional filtering.
        /// </summary>
        /// <param name="parameters">The optional filter and pagination parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A paginated list of custom forms.</returns>
        public async Task<ApiResponse<List<CustomForm>>> GetCustomFormsAsync(FilterParameters? parameters = null, CancellationToken cancellationToken = default)
        {
            var endpoint = "custom-forms";
            return await GetAsync<List<CustomForm>>(endpoint);
        }

        /// <summary>
        /// Gets a custom form by ID.
        /// </summary>
        /// <param name="id">The custom form ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The custom form details.</returns>
        public async Task<CustomForm> GetCustomFormAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentException("Custom form ID must be greater than zero.", nameof(id));

            var endpoint = $"custom-forms/{id}";
            var response = await GetAsync<CustomForm>(endpoint);
            
            return response.Data ?? throw new Exceptions.LawMaticsNotFoundException("Custom form not found.", 404, null, null, "CustomForm", id.ToString());
        }

        /// <summary>
        /// Creates a new custom form.
        /// </summary>
        /// <param name="customForm">The custom form data to create.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The created custom form details.</returns>
        public async Task<CustomForm> CreateCustomFormAsync(CreateCustomFormRequest customForm, CancellationToken cancellationToken = default)
        {
            if (customForm == null)
                throw new ArgumentNullException(nameof(customForm));

            if (string.IsNullOrWhiteSpace(customForm.Name))
                throw new ArgumentException("Custom form name is required.", nameof(customForm));

            var endpoint = "custom-forms";
            var response = await PostAsync<CustomForm>(endpoint, customForm);
            
            return response.Data ?? throw new Exceptions.LawMaticsException("Failed to create custom form: No data returned from API.");
        }

        /// <summary>
        /// Updates an existing custom form.
        /// </summary>
        /// <param name="id">The custom form ID.</param>
        /// <param name="customForm">The updated custom form data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated custom form details.</returns>
        public async Task<CustomForm> UpdateCustomFormAsync(int id, UpdateCustomFormRequest customForm, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentException("Custom form ID must be greater than zero.", nameof(id));

            if (customForm == null)
                throw new ArgumentNullException(nameof(customForm));

            var endpoint = $"custom-forms/{id}";
            var response = await PutAsync<CustomForm>(endpoint, customForm);
            
            return response.Data ?? throw new Exceptions.LawMaticsException("Failed to update custom form: No data returned from API.");
        }

        /// <summary>
        /// Deletes a custom form.
        /// </summary>
        /// <param name="id">The custom form ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the custom form was deleted successfully.</returns>
        public async Task<bool> DeleteCustomFormAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentException("Custom form ID must be greater than zero.", nameof(id));

            var endpoint = $"custom-forms/{id}";
            await DeleteAsync(endpoint);
            return true;
        }
    }

    public class CustomForm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? FormType { get; set; }
        public List<FormField>? Fields { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class FormField
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FieldType { get; set; }
        public string? Label { get; set; }
        public bool Required { get; set; }
        public string? Options { get; set; }
        public int? Order { get; set; }
    }

    public class CreateCustomFormRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? FormType { get; set; }
        public List<FormField>? Fields { get; set; }
    }

    public class UpdateCustomFormRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? FormType { get; set; }
        public List<FormField>? Fields { get; set; }
        public bool? Active { get; set; }
    }
}