using LawMatics.SDK.Configuration;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing custom contact types in the LawMatics API.
    /// </summary>
    public class CustomContactTypesClient : BaseClient
    {
        public CustomContactTypesClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Gets all custom contact types with optional pagination and filtering.
        /// </summary>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="name">Filter by contact type name.</param>
        /// <param name="isActive">Filter by active status.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of custom contact types.</returns>
        public async Task<PagedResponse<CustomContactType>> GetCustomContactTypesAsync(
            int page = 1,
            int pageSize = 20,
            string? name = null,
            bool? isActive = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            if (!string.IsNullOrEmpty(name))
                parameters["name"] = name;

            if (isActive.HasValue)
                parameters["is_active"] = isActive.Value.ToString();

            return await GetAsync<PagedResponse<CustomContactType>>("custom-contact-types", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets a specific custom contact type by ID.
        /// </summary>
        /// <param name="id">The custom contact type ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The custom contact type details.</returns>
        public async Task<CustomContactType?> GetCustomContactTypeAsync(int id, CancellationToken cancellationToken = default)
        {
            return await GetAsync<CustomContactType>($"custom-contact-types/{id}", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Creates a new custom contact type.
        /// </summary>
        /// <param name="request">The custom contact type creation request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created custom contact type.</returns>
        public async Task<CustomContactType?> CreateCustomContactTypeAsync(CreateCustomContactTypeRequest request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<CustomContactType>("custom-contact-types", request, cancellationToken);
        }

        /// <summary>
        /// Updates an existing custom contact type.
        /// </summary>
        /// <param name="id">The custom contact type ID.</param>
        /// <param name="request">The custom contact type update request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated custom contact type.</returns>
        public async Task<CustomContactType?> UpdateCustomContactTypeAsync(int id, UpdateCustomContactTypeRequest request, CancellationToken cancellationToken = default)
        {
            return await PutAsync<CustomContactType>($"custom-contact-types/{id}", request, cancellationToken);
        }

        /// <summary>
        /// Deletes a custom contact type.
        /// </summary>
        /// <param name="id">The custom contact type ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if deletion was successful.</returns>
        public async Task<bool> DeleteCustomContactTypeAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync($"custom-contact-types/{id}", cancellationToken);
        }

        /// <summary>
        /// Gets active custom contact types.
        /// </summary>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of active custom contact types.</returns>
        public async Task<PagedResponse<CustomContactType>> GetActiveCustomContactTypesAsync(
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetCustomContactTypesAsync(page, pageSize, isActive: true, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Searches custom contact types by name.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of custom contact types matching the search query.</returns>
        public async Task<PagedResponse<CustomContactType>> SearchCustomContactTypesAsync(
            string query,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetCustomContactTypesAsync(page, pageSize, name: query, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets contacts of a specific custom type.
        /// </summary>
        /// <param name="contactTypeId">The contact type ID.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of contacts of the specified type.</returns>
        public async Task<PagedResponse<Contact>> GetContactsByTypeAsync(
            int contactTypeId,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            return await GetAsync<PagedResponse<Contact>>($"custom-contact-types/{contactTypeId}/contacts", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets custom contact type statistics.
        /// </summary>
        /// <param name="contactTypeId">The contact type ID.</param>
        /// <param name="dateFrom">Start date (optional).</param>
        /// <param name="dateTo">End date (optional).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Contact type statistics.</returns>
        public async Task<CustomContactTypeStatistics?> GetCustomContactTypeStatisticsAsync(
            int contactTypeId,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();

            if (dateFrom.HasValue)
                parameters["date_from"] = dateFrom.Value.ToString("yyyy-MM-dd");

            if (dateTo.HasValue)
                parameters["date_to"] = dateTo.Value.ToString("yyyy-MM-dd");

            return await GetAsync<CustomContactTypeStatistics>($"custom-contact-types/{contactTypeId}/statistics", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets custom contact type growth trends.
        /// </summary>
        /// <param name="contactTypeId">The contact type ID.</param>
        /// <param name="dateFrom">Start date.</param>
        /// <param name="dateTo">End date.</param>
        /// <param name="groupBy">How to group the data (day, week, month).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Contact type growth trends.</returns>
        public async Task<CustomContactTypeTrends?> GetCustomContactTypeTrendsAsync(
            int contactTypeId,
            DateTime dateFrom,
            DateTime dateTo,
            string groupBy = "day",
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["date_from"] = dateFrom.ToString("yyyy-MM-dd"),
                ["date_to"] = dateTo.ToString("yyyy-MM-dd"),
                ["group_by"] = groupBy
            };

            return await GetAsync<CustomContactTypeTrends>($"custom-contact-types/{contactTypeId}/trends", parameters, cancellationToken);
        }

        /// <summary>
        /// Duplicates a custom contact type.
        /// </summary>
        /// <param name="id">The custom contact type ID to duplicate.</param>
        /// <param name="newName">The name for the duplicated contact type.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The duplicated custom contact type.</returns>
        public async Task<CustomContactType?> DuplicateCustomContactTypeAsync(
            int id,
            string newName,
            CancellationToken cancellationToken = default)
        {
            var request = new { name = newName };
            return await PostAsync<CustomContactType>($"custom-contact-types/{id}/duplicate", request, cancellationToken);
        }

        /// <summary>
        /// Validates custom contact type data.
        /// </summary>
        /// <param name="request">The custom contact type request to validate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Validation result.</returns>
        public async Task<CustomContactTypeValidationResult?> ValidateCustomContactTypeAsync(
            CreateCustomContactTypeRequest request,
            CancellationToken cancellationToken = default)
        {
            return await PostAsync<CustomContactTypeValidationResult>("custom-contact-types/validate", request, cancellationToken);
        }

        /// <summary>
        /// Gets the most used custom contact types.
        /// </summary>
        /// <param name="dateFrom">Start date (optional).</param>
        /// <param name="dateTo">End date (optional).</param>
        /// <param name="limit">Maximum number of results to return (default: 10).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of popular contact types with usage counts.</returns>
        public async Task<List<PopularCustomContactType>> GetPopularCustomContactTypesAsync(
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            int limit = 10,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["limit"] = limit.ToString()
            };

            if (dateFrom.HasValue)
                parameters["date_from"] = dateFrom.Value.ToString("yyyy-MM-dd");

            if (dateTo.HasValue)
                parameters["date_to"] = dateTo.Value.ToString("yyyy-MM-dd");

            var response = await GetAsync<PopularCustomContactTypesResponse>("custom-contact-types/popular", parameters, cancellationToken);
            return response?.PopularTypes ?? new List<PopularCustomContactType>();
        }

        /// <summary>
        /// Updates the order of custom contact types.
        /// </summary>
        /// <param name="typeOrders">List of contact type IDs and their new order.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the order update was successful.</returns>
        public async Task<bool> UpdateCustomContactTypeOrderAsync(
            List<CustomContactTypeOrder> typeOrders,
            CancellationToken cancellationToken = default)
        {
            var request = new { contact_types = typeOrders };
            var response = await PutAsync<object>("custom-contact-types/order", request, cancellationToken);
            return response != null;
        }

        /// <summary>
        /// Exports custom contact types to CSV.
        /// </summary>
        /// <param name="includeStatistics">Include usage statistics.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The CSV file content.</returns>
        public async Task<byte[]> ExportCustomContactTypesToCsvAsync(
            bool includeStatistics = false,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["format"] = "csv",
                ["include_statistics"] = includeStatistics.ToString()
            };

            return await GetFileAsync("custom-contact-types/export", parameters, cancellationToken);
        }

        /// <summary>
        /// Assigns a custom contact type to multiple contacts.
        /// </summary>
        /// <param name="contactTypeId">The contact type ID.</param>
        /// <param name="contactIds">List of contact IDs to assign.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the assignment was successful.</returns>
        public async Task<bool> AssignCustomContactTypeToContactsAsync(
            int contactTypeId,
            List<int> contactIds,
            CancellationToken cancellationToken = default)
        {
            var request = new
            {
                contact_type_id = contactTypeId,
                contact_ids = contactIds
            };

            var response = await PostAsync<object>("custom-contact-types/assign", request, cancellationToken);
            return response != null;
        }

        /// <summary>
        /// Removes a custom contact type from multiple contacts.
        /// </summary>
        /// <param name="contactTypeId">The contact type ID.</param>
        /// <param name="contactIds">List of contact IDs to remove from.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the removal was successful.</returns>
        public async Task<bool> RemoveCustomContactTypeFromContactsAsync(
            int contactTypeId,
            List<int> contactIds,
            CancellationToken cancellationToken = default)
        {
            var request = new
            {
                contact_type_id = contactTypeId,
                contact_ids = contactIds
            };

            var response = await PostAsync<object>("custom-contact-types/remove", request, cancellationToken);
            return response != null;
        }
    }

    /// <summary>
    /// Represents custom contact type statistics.
    /// </summary>
    public class CustomContactTypeStatistics
    {
        /// <summary>
        /// Gets or sets the contact type ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("contact_type_id")]
        public int ContactTypeId { get; set; }

        /// <summary>
        /// Gets or sets the contact type name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("contact_type_name")]
        public string ContactTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total contacts.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_contacts")]
        public int TotalContacts { get; set; }

        /// <summary>
        /// Gets or sets the percentage of total contacts.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("percentage")]
        public decimal Percentage { get; set; }

        /// <summary>
        /// Gets or sets the average age of contacts (days since creation).
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("average_age_days")]
        public decimal AverageAgeDays { get; set; }

        /// <summary>
        /// Gets or sets the breakdown by month.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("monthly_breakdown")]
        public List<MonthlyContactTypeStats> MonthlyBreakdown { get; set; } = new();
    }

    /// <summary>
    /// Represents monthly contact type statistics.
    /// </summary>
    public class MonthlyContactTypeStats
    {
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("month")]
        public string Month { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of new contacts.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("new_contacts")]
        public int NewContacts { get; set; }

        /// <summary>
        /// Gets or sets the total contacts at month end.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_contacts")]
        public int TotalContacts { get; set; }
    }

    /// <summary>
    /// Represents custom contact type trends.
    /// </summary>
    public class CustomContactTypeTrends
    {
        /// <summary>
        /// Gets or sets the contact type ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("contact_type_id")]
        public int ContactTypeId { get; set; }

        /// <summary>
        /// Gets or sets the contact type name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("contact_type_name")]
        public string ContactTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the trend data points.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("data_points")]
        public List<ContactTrendDataPoint> DataPoints { get; set; } = new();

        /// <summary>
        /// Gets or sets the growth trend direction (increasing, decreasing, stable).
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("growth_trend")]
        public string GrowthTrend { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the growth percentage.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("growth_percentage")]
        public decimal GrowthPercentage { get; set; }
    }

    /// <summary>
    /// Represents a contact trend data point.
    /// </summary>
    public class ContactTrendDataPoint
    {
        /// <summary>
        /// Gets or sets the date or period.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("date")]
        public string Date { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of new contacts.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("new_contacts")]
        public int NewContacts { get; set; }

        /// <summary>
        /// Gets or sets the total contacts.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_contacts")]
        public int TotalContacts { get; set; }
    }

    /// <summary>
    /// Represents custom contact type validation result.
    /// </summary>
    public class CustomContactTypeValidationResult
    {
        /// <summary>
        /// Gets or sets whether the contact type is valid.
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
    }

    /// <summary>
    /// Response model for popular custom contact types.
    /// </summary>
    public class PopularCustomContactTypesResponse
    {
        /// <summary>
        /// Gets or sets the list of popular contact types.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("popular_types")]
        public List<PopularCustomContactType> PopularTypes { get; set; } = new();
    }

    /// <summary>
    /// Represents a popular custom contact type.
    /// </summary>
    public class PopularCustomContactType
    {
        /// <summary>
        /// Gets or sets the contact type ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("contact_type_id")]
        public int ContactTypeId { get; set; }

        /// <summary>
        /// Gets or sets the contact type name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("contact_type_name")]
        public string ContactTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the usage count.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("usage_count")]
        public int UsageCount { get; set; }

        /// <summary>
        /// Gets or sets the percentage of total contacts.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("percentage")]
        public decimal Percentage { get; set; }
    }

    /// <summary>
    /// Represents custom contact type order.
    /// </summary>
    public class CustomContactTypeOrder
    {
        /// <summary>
        /// Gets or sets the contact type ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("contact_type_id")]
        public int ContactTypeId { get; set; }

        /// <summary>
        /// Gets or sets the order position.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("order")]
        public int Order { get; set; }
    }
}