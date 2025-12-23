using LawMatics.SDK.Configuration;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing time entries in the LawMatics API.
    /// </summary>
    public class TimeEntriesClient : BaseClient
    {
        public TimeEntriesClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Gets all time entries with optional pagination and filtering.
        /// </summary>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="matterId">Filter by matter ID.</param>
        /// <param name="userId">Filter by user ID.</param>
        /// <param name="dateFrom">Filter by time entry date from.</param>
        /// <param name="dateTo">Filter by time entry date to.</param>
        /// <param name="hoursFrom">Filter by minimum hours.</param>
        /// <param name="hoursTo">Filter by maximum hours.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of time entries.</returns>
        public async Task<PagedResponse<TimeEntry>> GetTimeEntriesAsync(
            int page = 1,
            int pageSize = 20,
            int? matterId = null,
            int? userId = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            decimal? hoursFrom = null,
            decimal? hoursTo = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            if (matterId.HasValue)
                parameters["matter_id"] = matterId.Value.ToString();

            if (userId.HasValue)
                parameters["user_id"] = userId.Value.ToString();

            if (dateFrom.HasValue)
                parameters["date_from"] = dateFrom.Value.ToString("yyyy-MM-dd");

            if (dateTo.HasValue)
                parameters["date_to"] = dateTo.Value.ToString("yyyy-MM-dd");

            if (hoursFrom.HasValue)
                parameters["hours_from"] = hoursFrom.Value.ToString();

            if (hoursTo.HasValue)
                parameters["hours_to"] = hoursTo.Value.ToString();

            return await GetAsync<PagedResponse<TimeEntry>>("time-entries", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets a specific time entry by ID.
        /// </summary>
        /// <param name="id">The time entry ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The time entry details.</returns>
        public async Task<TimeEntry?> GetTimeEntryAsync(int id, CancellationToken cancellationToken = default)
        {
            return await GetAsync<TimeEntry>($"time-entries/{id}", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Creates a new time entry.
        /// </summary>
        /// <param name="request">The time entry creation request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created time entry.</returns>
        public async Task<TimeEntry?> CreateTimeEntryAsync(CreateTimeEntryRequest request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<TimeEntry>("time-entries", request, cancellationToken);
        }

        /// <summary>
        /// Updates an existing time entry.
        /// </summary>
        /// <param name="id">The time entry ID.</param>
        /// <param name="request">The time entry update request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated time entry.</returns>
        public async Task<TimeEntry?> UpdateTimeEntryAsync(int id, UpdateTimeEntryRequest request, CancellationToken cancellationToken = default)
        {
            return await PutAsync<TimeEntry>($"time-entries/{id}", request, cancellationToken);
        }

        /// <summary>
        /// Deletes a time entry.
        /// </summary>
        /// <param name="id">The time entry ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if deletion was successful.</returns>
        public async Task<bool> DeleteTimeEntryAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync($"time-entries/{id}", cancellationToken);
        }

        /// <summary>
        /// Gets time entries for a specific matter.
        /// </summary>
        /// <param name="matterId">The matter ID.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of time entries for the matter.</returns>
        public async Task<PagedResponse<TimeEntry>> GetTimeEntriesByMatterAsync(
            int matterId,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetTimeEntriesAsync(page, pageSize, matterId: matterId, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets time entries for a specific user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of time entries for the user.</returns>
        public async Task<PagedResponse<TimeEntry>> GetTimeEntriesByUserAsync(
            int userId,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetTimeEntriesAsync(page, pageSize, userId: userId, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets time entries within a specified date range.
        /// </summary>
        /// <param name="fromDate">Start date.</param>
        /// <param name="toDate">End date.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of time entries in the specified date range.</returns>
        public async Task<PagedResponse<TimeEntry>> GetTimeEntriesByDateRangeAsync(
            DateTime fromDate,
            DateTime toDate,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetTimeEntriesAsync(page, pageSize, dateFrom: fromDate, dateTo: toDate, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets time entries for a specific matter and user.
        /// </summary>
        /// <param name="matterId">The matter ID.</param>
        /// <param name="userId">The user ID.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of time entries for the matter and user.</returns>
        public async Task<PagedResponse<TimeEntry>> GetTimeEntriesByMatterAndUserAsync(
            int matterId,
            int userId,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetTimeEntriesAsync(page, pageSize, matterId: matterId, userId: userId, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets time entry summary for a matter.
        /// </summary>
        /// <param name="matterId">The matter ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Time entry summary for the matter.</returns>
        public async Task<TimeEntrySummary?> GetTimeEntrySummaryByMatterAsync(int matterId, CancellationToken cancellationToken = default)
        {
            return await GetAsync<TimeEntrySummary>($"time-entries/matters/{matterId}/summary", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets time entry summary for a user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="dateFrom">Start date (optional).</param>
        /// <param name="dateTo">End date (optional).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Time entry summary for the user.</returns>
        public async Task<TimeEntrySummary?> GetTimeEntrySummaryByUserAsync(
            int userId,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();

            if (dateFrom.HasValue)
                parameters["date_from"] = dateFrom.Value.ToString("yyyy-MM-dd");

            if (dateTo.HasValue)
                parameters["date_to"] = dateTo.Value.ToString("yyyy-MM-dd");

            return await GetAsync<TimeEntrySummary>($"time-entries/users/{userId}/summary", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets time entry summary for a date range.
        /// </summary>
        /// <param name="fromDate">Start date.</param>
        /// <param name="toDate">End date.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Time entry summary for the date range.</returns>
        public async Task<TimeEntrySummary?> GetTimeEntrySummaryByDateRangeAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["from_date"] = fromDate.ToString("yyyy-MM-dd"),
                ["to_date"] = toDate.ToString("yyyy-MM-dd")
            };

            return await GetAsync<TimeEntrySummary>("time-entries/summary", parameters, cancellationToken);
        }

        /// <summary>
        /// Searches time entries by keyword.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of time entries matching the search query.</returns>
        public async Task<PagedResponse<TimeEntry>> SearchTimeEntriesAsync(
            string query,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString(),
                ["search"] = query
            };

            return await GetAsync<PagedResponse<TimeEntry>>("time-entries", parameters, cancellationToken);
        }

        /// <summary>
        /// Starts a timer for time tracking.
        /// </summary>
        /// <param name="matterId">The matter ID (optional).</param>
        /// <param name="description">Initial description (optional).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The started timer session.</returns>
        public async Task<TimerSession?> StartTimerAsync(
            int? matterId = null,
            string? description = null,
            CancellationToken cancellationToken = default)
        {
            var request = new { matter_id = matterId, description };
            return await PostAsync<TimerSession>("time-entries/timer/start", request, cancellationToken);
        }

        /// <summary>
        /// Stops a running timer.
        /// </summary>
        /// <param name="timerId">The timer session ID.</param>
        /// <param name="description">Final description (optional).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created time entry from the timer.</returns>
        public async Task<TimeEntry?> StopTimerAsync(
            int timerId,
            string? description = null,
            CancellationToken cancellationToken = default)
        {
            var request = new { description };
            return await PostAsync<TimeEntry>($"time-entries/timer/{timerId}/stop", request, cancellationToken);
        }

        /// <summary>
        /// Gets active timer sessions.
        /// </summary>
        /// <param name="userId">Filter by user ID (optional).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of active timer sessions.</returns>
        public async Task<List<TimerSession>> GetActiveTimersAsync(int? userId = null, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();

            if (userId.HasValue)
                parameters["user_id"] = userId.Value.ToString();

            var response = await GetAsync<TimerSessionsResponse>("time-entries/timer/active", parameters, cancellationToken);
            return response?.Timers ?? new List<TimerSession>();
        }

        /// <summary>
        /// Exports time entries to CSV.
        /// </summary>
        /// <param name="matterId">Filter by matter ID (optional).</param>
        /// <param name="userId">Filter by user ID (optional).</param>
        /// <param name="dateFrom">Filter by time entry date from (optional).</param>
        /// <param name="dateTo">Filter by time entry date to (optional).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The CSV file content.</returns>
        public async Task<byte[]> ExportTimeEntriesToCsvAsync(
            int? matterId = null,
            int? userId = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();

            if (matterId.HasValue)
                parameters["matter_id"] = matterId.Value.ToString();

            if (userId.HasValue)
                parameters["user_id"] = userId.Value.ToString();

            if (dateFrom.HasValue)
                parameters["date_from"] = dateFrom.Value.ToString("yyyy-MM-dd");

            if (dateTo.HasValue)
                parameters["date_to"] = dateTo.Value.ToString("yyyy-MM-dd");

            parameters["format"] = "csv";

            return await GetFileAsync("time-entries/export", parameters, cancellationToken);
        }
    }

    /// <summary>
    /// Response model for timer sessions.
    /// </summary>
    public class TimerSessionsResponse
    {
        /// <summary>
        /// Gets or sets the list of timer sessions.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("timers")]
        public List<TimerSession> Timers { get; set; } = new();
    }

    /// <summary>
    /// Represents a timer session.
    /// </summary>
    public class TimerSession
    {
        /// <summary>
        /// Gets or sets the timer session ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the matter ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("matter_id")]
        public int? MatterId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("started_at")]
        public DateTime StartedAt { get; set; }

        /// <summary>
        /// Gets or sets the current elapsed hours.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("elapsed_hours")]
        public decimal ElapsedHours { get; set; }

        /// <summary>
        /// Gets or sets whether the timer is currently running.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("is_running")]
        public bool IsRunning { get; set; }
    }

    /// <summary>
    /// Represents time entry summary information.
    /// </summary>
    public class TimeEntrySummary
    {
        /// <summary>
        /// Gets or sets the total hours.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_hours")]
        public decimal TotalHours { get; set; }

        /// <summary>
        /// Gets or sets the total amount.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_amount")]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_count")]
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the average hours per entry.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("average_hours")]
        public decimal AverageHours { get; set; }

        /// <summary>
        /// Gets or sets the average rate.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("average_rate")]
        public decimal AverageRate { get; set; }

        /// <summary>
        /// Gets or sets the breakdown by matter.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("matter_breakdown")]
        public List<MatterTimeBreakdown> MatterBreakdown { get; set; } = new();

        /// <summary>
        /// Gets or sets the breakdown by user.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("user_breakdown")]
        public List<UserTimeBreakdown> UserBreakdown { get; set; } = new();

        /// <summary>
        /// Gets or sets the breakdown by day.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("daily_breakdown")]
        public List<DailyTimeBreakdown> DailyBreakdown { get; set; } = new();
    }

    /// <summary>
    /// Represents time breakdown by matter.
    /// </summary>
    public class MatterTimeBreakdown
    {
        /// <summary>
        /// Gets or sets the matter ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("matter_id")]
        public int MatterId { get; set; }

        /// <summary>
        /// Gets or sets the matter name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("matter_name")]
        public string MatterName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total hours.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("hours")]
        public decimal Hours { get; set; }

        /// <summary>
        /// Gets or sets the total amount.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("count")]
        public int Count { get; set; }
    }

    /// <summary>
    /// Represents time breakdown by user.
    /// </summary>
    public class UserTimeBreakdown
    {
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("user_name")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total hours.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("hours")]
        public decimal Hours { get; set; }

        /// <summary>
        /// Gets or sets the total amount.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("count")]
        public int Count { get; set; }
    }

    /// <summary>
    /// Represents time breakdown by day.
    /// </summary>
    public class DailyTimeBreakdown
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("date")]
        public string Date { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total hours.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("hours")]
        public decimal Hours { get; set; }

        /// <summary>
        /// Gets or sets the total amount.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("count")]
        public int Count { get; set; }
    }
}