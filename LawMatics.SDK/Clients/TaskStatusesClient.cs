using LawMatics.SDK.Configuration;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;
using TaskStatus = LawMatics.SDK.Models.TaskStatus;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing task statuses in the LawMatics API.
    /// </summary>
    public class TaskStatusesClient : BaseClient
    {
        public TaskStatusesClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Gets all task statuses with optional pagination and filtering.
        /// </summary>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="name">Filter by status name.</param>
        /// <param name="isCompleted">Filter by completion status.</param>
        /// <param name="isActive">Filter by active status.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of task statuses.</returns>
        public async Task<PagedResponse<TaskStatus>> GetTaskStatusesAsync(
            int page = 1,
            int pageSize = 20,
            string? name = null,
            bool? isCompleted = null,
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

            if (isCompleted.HasValue)
                parameters["is_completed"] = isCompleted.Value.ToString();

            if (isActive.HasValue)
                parameters["is_active"] = isActive.Value.ToString();

            return await GetAsync<PagedResponse<TaskStatus>>("task-statuses", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets a specific task status by ID.
        /// </summary>
        /// <param name="id">The task status ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The task status details.</returns>
        public async Task<TaskStatus?> GetTaskStatusAsync(int id, CancellationToken cancellationToken = default)
        {
            return await GetAsync<TaskStatus>($"task-statuses/{id}", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Creates a new task status.
        /// </summary>
        /// <param name="request">The task status creation request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created task status.</returns>
        public async Task<TaskStatus?> CreateTaskStatusAsync(CreateTaskStatusRequest request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<TaskStatus>("task-statuses", request, cancellationToken);
        }

        /// <summary>
        /// Updates an existing task status.
        /// </summary>
        /// <param name="id">The task status ID.</param>
        /// <param name="request">The task status update request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated task status.</returns>
        public async Task<TaskStatus?> UpdateTaskStatusAsync(int id, UpdateTaskStatusRequest request, CancellationToken cancellationToken = default)
        {
            return await PutAsync<TaskStatus>($"task-statuses/{id}", request, cancellationToken);
        }

        /// <summary>
        /// Deletes a task status.
        /// </summary>
        /// <param name="id">The task status ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if deletion was successful.</returns>
        public async Task<bool> DeleteTaskStatusAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync($"task-statuses/{id}", cancellationToken);
        }

        /// <summary>
        /// Gets active task statuses.
        /// </summary>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of active task statuses.</returns>
        public async Task<PagedResponse<TaskStatus>> GetActiveTaskStatusesAsync(
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetTaskStatusesAsync(page, pageSize, isActive: true, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets completed task statuses.
        /// </summary>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of completed task statuses.</returns>
        public async Task<PagedResponse<TaskStatus>> GetCompletedTaskStatusesAsync(
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetTaskStatusesAsync(page, pageSize, isCompleted: true, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets incomplete task statuses.
        /// </summary>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of incomplete task statuses.</returns>
        public async Task<PagedResponse<TaskStatus>> GetIncompleteTaskStatusesAsync(
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetTaskStatusesAsync(page, pageSize, isCompleted: false, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Searches task statuses by name.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of task statuses matching the search query.</returns>
        public async Task<PagedResponse<TaskStatus>> SearchTaskStatusesAsync(
            string query,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetTaskStatusesAsync(page, pageSize, name: query, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets tasks with a specific status.
        /// </summary>
        /// <param name="statusId">The status ID.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of tasks with the specified status.</returns>
        public async Task<PagedResponse<TaskItem>> GetTasksByStatusAsync(
            int statusId,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            return await GetAsync<PagedResponse<TaskItem>>($"task-statuses/{statusId}/tasks", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets task status statistics.
        /// </summary>
        /// <param name="statusId">The status ID.</param>
        /// <param name="dateFrom">Start date (optional).</param>
        /// <param name="dateTo">End date (optional).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task status statistics.</returns>
        public async Task<TaskStatusStatistics?> GetTaskStatusStatisticsAsync(
            int statusId,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();

            if (dateFrom.HasValue)
                parameters["date_from"] = dateFrom.Value.ToString("yyyy-MM-dd");

            if (dateTo.HasValue)
                parameters["date_to"] = dateTo.Value.ToString("yyyy-MM-dd");

            return await GetAsync<TaskStatusStatistics>($"task-statuses/{statusId}/statistics", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets task status workflow transitions.
        /// </summary>
        /// <param name="statusId">The status ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Available workflow transitions from this status.</returns>
        public async Task<List<TaskStatusTransition>> GetTaskStatusTransitionsAsync(
            int statusId,
            CancellationToken cancellationToken = default)
        {
            var response = await GetAsync<TaskStatusTransitionsResponse>($"task-statuses/{statusId}/transitions", cancellationToken: cancellationToken);
            return response?.Transitions ?? new List<TaskStatusTransition>();
        }

        /// <summary>
        /// Duplicates a task status.
        /// </summary>
        /// <param name="id">The task status ID to duplicate.</param>
        /// <param name="newName">The name for the duplicated status.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The duplicated task status.</returns>
        public async Task<TaskStatus?> DuplicateTaskStatusAsync(
            int id,
            string newName,
            CancellationToken cancellationToken = default)
        {
            var request = new { name = newName };
            return await PostAsync<TaskStatus>($"task-statuses/{id}/duplicate", request, cancellationToken);
        }

        /// <summary>
        /// Validates task status data.
        /// </summary>
        /// <param name="request">The task status request to validate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Validation result.</returns>
        public async Task<TaskStatusValidationResult?> ValidateTaskStatusAsync(
            CreateTaskStatusRequest request,
            CancellationToken cancellationToken = default)
        {
            return await PostAsync<TaskStatusValidationResult>("task-statuses/validate", request, cancellationToken);
        }

        /// <summary>
        /// Gets the most used task statuses.
        /// </summary>
        /// <param name="dateFrom">Start date (optional).</param>
        /// <param name="dateTo">End date (optional).</param>
        /// <param name="limit">Maximum number of results to return (default: 10).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of popular task statuses with usage counts.</returns>
        public async Task<List<PopularTaskStatus>> GetPopularTaskStatusesAsync(
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

            var response = await GetAsync<PopularTaskStatusesResponse>("task-statuses/popular", parameters, cancellationToken);
            return response?.PopularStatuses ?? new List<PopularTaskStatus>();
        }

        /// <summary>
        /// Updates the order of task statuses.
        /// </summary>
        /// <param name="statusOrders">List of status IDs and their new order.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the order update was successful.</returns>
        public async Task<bool> UpdateTaskStatusOrderAsync(
            List<TaskStatusOrder> statusOrders,
            CancellationToken cancellationToken = default)
        {
            var request = new { task_statuses = statusOrders };
            var response = await PutAsync<object>("task-statuses/order", request, cancellationToken);
            return response != null;
        }

        /// <summary>
        /// Activates a task status.
        /// </summary>
        /// <param name="id">The task status ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated task status.</returns>
        public async Task<TaskStatus?> ActivateTaskStatusAsync(int id, CancellationToken cancellationToken = default)
        {
            return await PostAsync<TaskStatus>($"task-statuses/{id}/activate", null, cancellationToken);
        }

        /// <summary>
        /// Deactivates a task status.
        /// </summary>
        /// <param name="id">The task status ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated task status.</returns>
        public async Task<TaskStatus?> DeactivateTaskStatusAsync(int id, CancellationToken cancellationToken = default)
        {
            return await PostAsync<TaskStatus>($"task-statuses/{id}/deactivate", null, cancellationToken);
        }

        /// <summary>
        /// Exports task statuses to CSV.
        /// </summary>
        /// <param name="includeStatistics">Include usage statistics.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The CSV file content.</returns>
        public async Task<byte[]> ExportTaskStatusesToCsvAsync(
            bool includeStatistics = false,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["format"] = "csv",
                ["include_statistics"] = includeStatistics.ToString()
            };

            return await GetFileAsync("task-statuses/export", parameters, cancellationToken);
        }
    }

    /// <summary>
    /// Represents task status statistics.
    /// </summary>
    public class TaskStatusStatistics
    {
        /// <summary>
        /// Gets or sets the status ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("status_id")]
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets the status name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("status_name")]
        public string StatusName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total tasks.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_tasks")]
        public int TotalTasks { get; set; }

        /// <summary>
        /// Gets or sets the percentage of total tasks.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("percentage")]
        public decimal Percentage { get; set; }

        /// <summary>
        /// Gets or sets the average time in this status (hours).
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("average_time_in_status_hours")]
        public decimal AverageTimeInStatusHours { get; set; }

        /// <summary>
        /// Gets or sets the breakdown by month.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("monthly_breakdown")]
        public List<MonthlyStatusStats> MonthlyBreakdown { get; set; } = new();
    }

    /// <summary>
    /// Represents monthly status statistics.
    /// </summary>
    public class MonthlyStatusStats
    {
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("month")]
        public string Month { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of tasks.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("tasks")]
        public int Tasks { get; set; }

        /// <summary>
        /// Gets or sets the average time in status.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("average_time_hours")]
        public decimal AverageTimeHours { get; set; }
    }

    /// <summary>
    /// Response model for task status transitions.
    /// </summary>
    public class TaskStatusTransitionsResponse
    {
        /// <summary>
        /// Gets or sets the list of available transitions.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("transitions")]
        public List<TaskStatusTransition> Transitions { get; set; } = new();
    }

    /// <summary>
    /// Represents a task status transition.
    /// </summary>
    public class TaskStatusTransition
    {
        /// <summary>
        /// Gets or sets the target status ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("to_status_id")]
        public int ToStatusId { get; set; }

        /// <summary>
        /// Gets or sets the target status name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("to_status_name")]
        public string ToStatusName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether this transition is allowed.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("is_allowed")]
        public bool IsAllowed { get; set; }

        /// <summary>
        /// Gets or sets the condition for this transition.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("condition")]
        public string? Condition { get; set; }
    }

    /// <summary>
    /// Represents task status validation result.
    /// </summary>
    public class TaskStatusValidationResult
    {
        /// <summary>
        /// Gets or sets whether the status is valid.
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
    /// Response model for popular task statuses.
    /// </summary>
    public class PopularTaskStatusesResponse
    {
        /// <summary>
        /// Gets or sets the list of popular task statuses.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("popular_statuses")]
        public List<PopularTaskStatus> PopularStatuses { get; set; } = new();
    }

    /// <summary>
    /// Represents a popular task status.
    /// </summary>
    public class PopularTaskStatus
    {
        /// <summary>
        /// Gets or sets the status ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("status_id")]
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets the status name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("status_name")]
        public string StatusName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the usage count.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("usage_count")]
        public int UsageCount { get; set; }

        /// <summary>
        /// Gets or sets the percentage of total tasks.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("percentage")]
        public decimal Percentage { get; set; }
    }

    /// <summary>
    /// Represents task status order.
    /// </summary>
    public class TaskStatusOrder
    {
        /// <summary>
        /// Gets or sets the status ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("status_id")]
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets the order position.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("order")]
        public int Order { get; set; }
    }
}