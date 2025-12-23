using LawMatics.SDK.Configuration;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing event types in the LawMatics API.
    /// </summary>
    public class EventTypesClient : BaseClient
    {
        public EventTypesClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Gets all event types with optional pagination and filtering.
        /// </summary>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="name">Filter by event type name.</param>
        /// <param name="isActive">Filter by active status.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of event types.</returns>
        public async Task<PagedResponse<EventType>> GetEventTypesAsync(
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

            return await GetAsync<PagedResponse<EventType>>("event-types", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets a specific event type by ID.
        /// </summary>
        /// <param name="id">The event type ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The event type details.</returns>
        public async Task<EventType?> GetEventTypeAsync(int id, CancellationToken cancellationToken = default)
        {
            return await GetAsync<EventType>($"event-types/{id}", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Creates a new event type.
        /// </summary>
        /// <param name="request">The event type creation request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created event type.</returns>
        public async Task<EventType?> CreateEventTypeAsync(CreateEventTypeRequest request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<EventType>("event-types", request, cancellationToken);
        }

        /// <summary>
        /// Updates an existing event type.
        /// </summary>
        /// <param name="id">The event type ID.</param>
        /// <param name="request">The event type update request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated event type.</returns>
        public async Task<EventType?> UpdateEventTypeAsync(int id, UpdateEventTypeRequest request, CancellationToken cancellationToken = default)
        {
            return await PutAsync<EventType>($"event-types/{id}", request, cancellationToken);
        }

        /// <summary>
        /// Deletes an event type.
        /// </summary>
        /// <param name="id">The event type ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if deletion was successful.</returns>
        public async Task<bool> DeleteEventTypeAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync($"event-types/{id}", cancellationToken);
        }

        /// <summary>
        /// Gets active event types.
        /// </summary>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of active event types.</returns>
        public async Task<PagedResponse<EventType>> GetActiveEventTypesAsync(
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetEventTypesAsync(page, pageSize, isActive: true, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Searches event types by name.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of event types matching the search query.</returns>
        public async Task<PagedResponse<EventType>> SearchEventTypesAsync(
            string query,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetEventTypesAsync(page, pageSize, name: query, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets events of a specific type.
        /// </summary>
        /// <param name="eventTypeId">The event type ID.</param>
        /// <param name="dateFrom">Filter by start date (optional).</param>
        /// <param name="dateTo">Filter by end date (optional).</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of events of the specified type.</returns>
        public async Task<PagedResponse<Event>> GetEventsByTypeAsync(
            int eventTypeId,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            if (dateFrom.HasValue)
                parameters["date_from"] = dateFrom.Value.ToString("yyyy-MM-dd");

            if (dateTo.HasValue)
                parameters["date_to"] = dateTo.Value.ToString("yyyy-MM-dd");

            return await GetAsync<PagedResponse<Event>>($"event-types/{eventTypeId}/events", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets event type statistics.
        /// </summary>
        /// <param name="eventTypeId">The event type ID.</param>
        /// <param name="dateFrom">Start date (optional).</param>
        /// <param name="dateTo">End date (optional).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Event type statistics.</returns>
        public async Task<EventTypeStatistics?> GetEventTypeStatisticsAsync(
            int eventTypeId,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();

            if (dateFrom.HasValue)
                parameters["date_from"] = dateFrom.Value.ToString("yyyy-MM-dd");

            if (dateTo.HasValue)
                parameters["date_to"] = dateTo.Value.ToString("yyyy-MM-dd");

            return await GetAsync<EventTypeStatistics>($"event-types/{eventTypeId}/statistics", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets event type usage trends.
        /// </summary>
        /// <param name="eventTypeId">The event type ID.</param>
        /// <param name="dateFrom">Start date.</param>
        /// <param name="dateTo">End date.</param>
        /// <param name="groupBy">How to group the data (day, week, month).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Event type usage trends.</returns>
        public async Task<EventTypeTrends?> GetEventTypeTrendsAsync(
            int eventTypeId,
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

            return await GetAsync<EventTypeTrends>($"event-types/{eventTypeId}/trends", parameters, cancellationToken);
        }

        /// <summary>
        /// Duplicates an event type.
        /// </summary>
        /// <param name="id">The event type ID to duplicate.</param>
        /// <param name="newName">The name for the duplicated event type.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The duplicated event type.</returns>
        public async Task<EventType?> DuplicateEventTypeAsync(
            int id,
            string newName,
            CancellationToken cancellationToken = default)
        {
            var request = new { name = newName };
            return await PostAsync<EventType>($"event-types/{id}/duplicate", request, cancellationToken);
        }

        /// <summary>
        /// Validates event type data.
        /// </summary>
        /// <param name="request">The event type request to validate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Validation result.</returns>
        public async Task<EventTypeValidationResult?> ValidateEventTypeAsync(
            CreateEventTypeRequest request,
            CancellationToken cancellationToken = default)
        {
            return await PostAsync<EventTypeValidationResult>("event-types/validate", request, cancellationToken);
        }

        /// <summary>
        /// Gets the most popular event types.
        /// </summary>
        /// <param name="dateFrom">Start date (optional).</param>
        /// <param name="dateTo">End date (optional).</param>
        /// <param name="limit">Maximum number of results to return (default: 10).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of popular event types with usage counts.</returns>
        public async Task<List<PopularEventType>> GetPopularEventTypesAsync(
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

            var response = await GetAsync<PopularEventTypesResponse>("event-types/popular", parameters, cancellationToken);
            return response?.PopularTypes ?? new List<PopularEventType>();
        }

        /// <summary>
        /// Gets event type colors for calendar visualization.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of event types with their colors.</returns>
        public async Task<List<EventTypeColor>> GetEventTypeColorsAsync(CancellationToken cancellationToken = default)
        {
            var response = await GetAsync<EventTypeColorsResponse>("event-types/colors", cancellationToken: cancellationToken);
            return response?.Colors ?? new List<EventTypeColor>();
        }

        /// <summary>
        /// Updates the order of event types.
        /// </summary>
        /// <param name="typeOrders">List of event type IDs and their new order.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the order update was successful.</returns>
        public async Task<bool> UpdateEventTypeOrderAsync(
            List<EventTypeOrder> typeOrders,
            CancellationToken cancellationToken = default)
        {
            var request = new { event_types = typeOrders };
            var response = await PutAsync<object>("event-types/order", request, cancellationToken);
            return response != null;
        }

        /// <summary>
        /// Exports event types to CSV.
        /// </summary>
        /// <param name="includeStatistics">Include usage statistics.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The CSV file content.</returns>
        public async Task<byte[]> ExportEventTypesToCsvAsync(
            bool includeStatistics = false,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["format"] = "csv",
                ["include_statistics"] = includeStatistics.ToString()
            };

            return await GetFileAsync("event-types/export", parameters, cancellationToken);
        }
    }

    /// <summary>
    /// Represents event type statistics.
    /// </summary>
    public class EventTypeStatistics
    {
        /// <summary>
        /// Gets or sets the event type ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("event_type_id")]
        public int EventTypeId { get; set; }

        /// <summary>
        /// Gets or sets the event type name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("event_type_name")]
        public string EventTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total events.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_events")]
        public int TotalEvents { get; set; }

        /// <summary>
        /// Gets or sets the total duration in minutes.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_duration_minutes")]
        public int TotalDurationMinutes { get; set; }

        /// <summary>
        /// Gets or sets the average duration in minutes.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("average_duration_minutes")]
        public decimal AverageDurationMinutes { get; set; }

        /// <summary>
        /// Gets or sets the total attendees.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_attendees")]
        public int TotalAttendees { get; set; }

        /// <summary>
        /// Gets or sets the average attendance.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("average_attendance")]
        public decimal AverageAttendance { get; set; }

        /// <summary>
        /// Gets or sets the breakdown by month.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("monthly_breakdown")]
        public List<MonthlyEventTypeStats> MonthlyBreakdown { get; set; } = new();
    }

    /// <summary>
    /// Represents monthly event type statistics.
    /// </summary>
    public class MonthlyEventTypeStats
    {
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("month")]
        public string Month { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of events.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("events")]
        public int Events { get; set; }

        /// <summary>
        /// Gets or sets the total duration.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("duration_minutes")]
        public int DurationMinutes { get; set; }

        /// <summary>
        /// Gets or sets the total attendees.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("attendees")]
        public int Attendees { get; set; }
    }

    /// <summary>
    /// Represents event type trends.
    /// </summary>
    public class EventTypeTrends
    {
        /// <summary>
        /// Gets or sets the event type ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("event_type_id")]
        public int EventTypeId { get; set; }

        /// <summary>
        /// Gets or sets the event type name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("event_type_name")]
        public string EventTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the trend data points.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("data_points")]
        public List<TrendDataPoint> DataPoints { get; set; } = new();

        /// <summary>
        /// Gets or sets the trend direction (increasing, decreasing, stable).
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("trend_direction")]
        public string TrendDirection { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the trend percentage.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("trend_percentage")]
        public decimal TrendPercentage { get; set; }
    }

    /// <summary>
    /// Represents a trend data point.
    /// </summary>
    public class TrendDataPoint
    {
        /// <summary>
        /// Gets or sets the date or period.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("date")]
        public string Date { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("value")]
        public int Value { get; set; }
    }

    /// <summary>
    /// Represents event type validation result.
    /// </summary>
    public class EventTypeValidationResult
    {
        /// <summary>
        /// Gets or sets whether the event type is valid.
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
    /// Response model for popular event types.
    /// </summary>
    public class PopularEventTypesResponse
    {
        /// <summary>
        /// Gets or sets the list of popular event types.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("popular_types")]
        public List<PopularEventType> PopularTypes { get; set; } = new();
    }

    /// <summary>
    /// Represents a popular event type.
    /// </summary>
    public class PopularEventType
    {
        /// <summary>
        /// Gets or sets the event type ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("event_type_id")]
        public int EventTypeId { get; set; }

        /// <summary>
        /// Gets or sets the event type name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("event_type_name")]
        public string EventTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the usage count.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("usage_count")]
        public int UsageCount { get; set; }

        /// <summary>
        /// Gets or sets the percentage of total events.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("percentage")]
        public decimal Percentage { get; set; }
    }

    /// <summary>
    /// Response model for event type colors.
    /// </summary>
    public class EventTypeColorsResponse
    {
        /// <summary>
        /// Gets or sets the list of event type colors.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("colors")]
        public List<EventTypeColor> Colors { get; set; } = new();
    }

    /// <summary>
    /// Represents an event type color.
    /// </summary>
    public class EventTypeColor
    {
        /// <summary>
        /// Gets or sets the event type ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("event_type_id")]
        public int EventTypeId { get; set; }

        /// <summary>
        /// Gets or sets the event type name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("event_type_name")]
        public string EventTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("color")]
        public string Color { get; set; } = string.Empty;
    }

    /// <summary>
    /// Represents event type order.
    /// </summary>
    public class EventTypeOrder
    {
        /// <summary>
        /// Gets or sets the event type ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("event_type_id")]
        public int EventTypeId { get; set; }

        /// <summary>
        /// Gets or sets the order position.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("order")]
        public int Order { get; set; }
    }
}