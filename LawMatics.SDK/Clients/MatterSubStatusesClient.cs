using LawMatics.SDK.Configuration;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing matter sub-statuses in the LawMatics API.
    /// </summary>
    public class MatterSubStatusesClient : BaseClient
    {
        public MatterSubStatusesClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Gets all matter sub-statuses with optional pagination and filtering.
        /// </summary>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="name">Filter by sub-status name.</param>
        /// <param name="parentStatusId">Filter by parent status ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of matter sub-statuses.</returns>
        public async Task<PagedResponse<MatterSubStatus>> GetMatterSubStatusesAsync(
            int page = 1,
            int pageSize = 20,
            string? name = null,
            int? parentStatusId = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            if (!string.IsNullOrEmpty(name))
                parameters["name"] = name;

            if (parentStatusId.HasValue)
                parameters["parent_status_id"] = parentStatusId.Value.ToString();

            return await GetAsync<PagedResponse<MatterSubStatus>>("matter-sub-statuses", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets a specific matter sub-status by ID.
        /// </summary>
        /// <param name="id">The matter sub-status ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The matter sub-status details.</returns>
        public async Task<MatterSubStatus?> GetMatterSubStatusAsync(int id, CancellationToken cancellationToken = default)
        {
            return await GetAsync<MatterSubStatus>($"matter-sub-statuses/{id}", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Creates a new matter sub-status.
        /// </summary>
        /// <param name="request">The matter sub-status creation request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created matter sub-status.</returns>
        public async Task<MatterSubStatus?> CreateMatterSubStatusAsync(CreateMatterSubStatusRequest request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<MatterSubStatus>("matter-sub-statuses", request, cancellationToken);
        }

        /// <summary>
        /// Updates an existing matter sub-status.
        /// </summary>
        /// <param name="id">The matter sub-status ID.</param>
        /// <param name="request">The matter sub-status update request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated matter sub-status.</returns>
        public async Task<MatterSubStatus?> UpdateMatterSubStatusAsync(int id, UpdateMatterSubStatusRequest request, CancellationToken cancellationToken = default)
        {
            return await PutAsync<MatterSubStatus>($"matter-sub-statuses/{id}", request, cancellationToken);
        }

        /// <summary>
        /// Deletes a matter sub-status.
        /// </summary>
        /// <param name="id">The matter sub-status ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if deletion was successful.</returns>
        public async Task<bool> DeleteMatterSubStatusAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync($"matter-sub-statuses/{id}", cancellationToken);
        }

        /// <summary>
        /// Gets matter sub-statuses by parent status.
        /// </summary>
        /// <param name="parentStatusId">The parent status ID.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of matter sub-statuses for the parent status.</returns>
        public async Task<PagedResponse<MatterSubStatus>> GetMatterSubStatusesByParentStatusAsync(
            int parentStatusId,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetMatterSubStatusesAsync(page, pageSize, parentStatusId: parentStatusId, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Searches matter sub-statuses by name.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of matter sub-statuses matching the search query.</returns>
        public async Task<PagedResponse<MatterSubStatus>> SearchMatterSubStatusesAsync(
            string query,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetMatterSubStatusesAsync(page, pageSize, name: query, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets matters with a specific sub-status.
        /// </summary>
        /// <param name="subStatusId">The sub-status ID.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of matters with the specified sub-status.</returns>
        public async Task<PagedResponse<Matter>> GetMattersBySubStatusAsync(
            int subStatusId,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            return await GetAsync<PagedResponse<Matter>>($"matter-sub-statuses/{subStatusId}/matters", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets matter sub-status statistics.
        /// </summary>
        /// <param name="subStatusId">The sub-status ID.</param>
        /// <param name="dateFrom">Start date (optional).</param>
        /// <param name="dateTo">End date (optional).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Sub-status statistics.</returns>
        public async Task<MatterSubStatusStatistics?> GetMatterSubStatusStatisticsAsync(
            int subStatusId,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();

            if (dateFrom.HasValue)
                parameters["date_from"] = dateFrom.Value.ToString("yyyy-MM-dd");

            if (dateTo.HasValue)
                parameters["date_to"] = dateTo.Value.ToString("yyyy-MM-dd");

            return await GetAsync<MatterSubStatusStatistics>($"matter-sub-statuses/{subStatusId}/statistics", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets matter sub-status transitions over time.
        /// </summary>
        /// <param name="subStatusId">The sub-status ID.</param>
        /// <param name="dateFrom">Start date.</param>
        /// <param name="dateTo">End date.</param>
        /// <param name="groupBy">How to group the data (day, week, month).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Sub-status transition data.</returns>
        public async Task<MatterSubStatusTransitions?> GetMatterSubStatusTransitionsAsync(
            int subStatusId,
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

            return await GetAsync<MatterSubStatusTransitions>($"matter-sub-statuses/{subStatusId}/transitions", parameters, cancellationToken);
        }

        /// <summary>
        /// Duplicates a matter sub-status.
        /// </summary>
        /// <param name="id">The matter sub-status ID to duplicate.</param>
        /// <param name="newName">The name for the duplicated sub-status.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The duplicated matter sub-status.</returns>
        public async Task<MatterSubStatus?> DuplicateMatterSubStatusAsync(
            int id,
            string newName,
            CancellationToken cancellationToken = default)
        {
            var request = new { name = newName };
            return await PostAsync<MatterSubStatus>($"matter-sub-statuses/{id}/duplicate", request, cancellationToken);
        }

        /// <summary>
        /// Validates matter sub-status data.
        /// </summary>
        /// <param name="request">The matter sub-status request to validate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Validation result.</returns>
        public async Task<MatterSubStatusValidationResult?> ValidateMatterSubStatusAsync(
            CreateMatterSubStatusRequest request,
            CancellationToken cancellationToken = default)
        {
            return await PostAsync<MatterSubStatusValidationResult>("matter-sub-statuses/validate", request, cancellationToken);
        }

        /// <summary>
        /// Gets the most used matter sub-statuses.
        /// </summary>
        /// <param name="dateFrom">Start date (optional).</param>
        /// <param name="dateTo">End date (optional).</param>
        /// <param name="limit">Maximum number of results to return (default: 10).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of popular sub-statuses with usage counts.</returns>
        public async Task<List<PopularMatterSubStatus>> GetPopularMatterSubStatusesAsync(
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

            var response = await GetAsync<PopularMatterSubStatusesResponse>("matter-sub-statuses/popular", parameters, cancellationToken);
            return response?.PopularSubStatuses ?? new List<PopularMatterSubStatus>();
        }

        /// <summary>
        /// Updates the order of matter sub-statuses.
        /// </summary>
        /// <param name="subStatusOrders">List of sub-status IDs and their new order.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the order update was successful.</returns>
        public async Task<bool> UpdateMatterSubStatusOrderAsync(
            List<MatterSubStatusOrder> subStatusOrders,
            CancellationToken cancellationToken = default)
        {
            var request = new { sub_statuses = subStatusOrders };
            var response = await PutAsync<object>("matter-sub-statuses/order", request, cancellationToken);
            return response != null;
        }

        /// <summary>
        /// Exports matter sub-statuses to CSV.
        /// </summary>
        /// <param name="parentStatusId">Filter by parent status ID (optional).</param>
        /// <param name="includeStatistics">Include usage statistics.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The CSV file content.</returns>
        public async Task<byte[]> ExportMatterSubStatusesToCsvAsync(
            int? parentStatusId = null,
            bool includeStatistics = false,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["format"] = "csv",
                ["include_statistics"] = includeStatistics.ToString()
            };

            if (parentStatusId.HasValue)
                parameters["parent_status_id"] = parentStatusId.Value.ToString();

            return await GetFileAsync("matter-sub-statuses/export", parameters, cancellationToken);
        }
    }

    /// <summary>
    /// Represents matter sub-status statistics.
    /// </summary>
    public class MatterSubStatusStatistics
    {
        /// <summary>
        /// Gets or sets the sub-status ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("sub_status_id")]
        public int SubStatusId { get; set; }

        /// <summary>
        /// Gets or sets the sub-status name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("sub_status_name")]
        public string SubStatusName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total matters.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_matters")]
        public int TotalMatters { get; set; }

        /// <summary>
        /// Gets or sets the average time in this status (days).
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("average_time_in_status_days")]
        public decimal AverageTimeInStatusDays { get; set; }

        /// <summary>
        /// Gets or sets the total value of matters in this status.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_matter_value")]
        public decimal TotalMatterValue { get; set; }

        /// <summary>
        /// Gets or sets the breakdown by month.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("monthly_breakdown")]
        public List<MonthlySubStatusStats> MonthlyBreakdown { get; set; } = new();
    }

    /// <summary>
    /// Represents monthly sub-status statistics.
    /// </summary>
    public class MonthlySubStatusStats
    {
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("month")]
        public string Month { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of matters.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("matters")]
        public int Matters { get; set; }

        /// <summary>
        /// Gets or sets the total value.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("value")]
        public decimal Value { get; set; }
    }

    /// <summary>
    /// Represents matter sub-status transitions.
    /// </summary>
    public class MatterSubStatusTransitions
    {
        /// <summary>
        /// Gets or sets the sub-status ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("sub_status_id")]
        public int SubStatusId { get; set; }

        /// <summary>
        /// Gets or sets the sub-status name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("sub_status_name")]
        public string SubStatusName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the transition data points.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("data_points")]
        public List<TransitionDataPoint> DataPoints { get; set; } = new();

        /// <summary>
        /// Gets or sets the total transitions in.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_transitions_in")]
        public int TotalTransitionsIn { get; set; }

        /// <summary>
        /// Gets or sets the total transitions out.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_transitions_out")]
        public int TotalTransitionsOut { get; set; }
    }

    /// <summary>
    /// Represents a transition data point.
    /// </summary>
    public class TransitionDataPoint
    {
        /// <summary>
        /// Gets or sets the date or period.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("date")]
        public string Date { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the transitions in.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("transitions_in")]
        public int TransitionsIn { get; set; }

        /// <summary>
        /// Gets or sets the transitions out.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("transitions_out")]
        public int TransitionsOut { get; set; }
    }

    /// <summary>
    /// Represents matter sub-status validation result.
    /// </summary>
    public class MatterSubStatusValidationResult
    {
        /// <summary>
        /// Gets or sets whether the sub-status is valid.
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
    /// Response model for popular matter sub-statuses.
    /// </summary>
    public class PopularMatterSubStatusesResponse
    {
        /// <summary>
        /// Gets or sets the list of popular sub-statuses.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("popular_sub_statuses")]
        public List<PopularMatterSubStatus> PopularSubStatuses { get; set; } = new();
    }

    /// <summary>
    /// Represents a popular matter sub-status.
    /// </summary>
    public class PopularMatterSubStatus
    {
        /// <summary>
        /// Gets or sets the sub-status ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("sub_status_id")]
        public int SubStatusId { get; set; }

        /// <summary>
        /// Gets or sets the sub-status name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("sub_status_name")]
        public string SubStatusName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the usage count.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("usage_count")]
        public int UsageCount { get; set; }

        /// <summary>
        /// Gets or sets the percentage of total matters.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("percentage")]
        public decimal Percentage { get; set; }
    }

    /// <summary>
    /// Represents matter sub-status order.
    /// </summary>
    public class MatterSubStatusOrder
    {
        /// <summary>
        /// Gets or sets the sub-status ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("sub_status_id")]
        public int SubStatusId { get; set; }

        /// <summary>
        /// Gets or sets the order position.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("order")]
        public int Order { get; set; }
    }
}