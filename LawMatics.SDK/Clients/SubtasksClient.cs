using LawMatics.SDK.Configuration;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing subtasks in the LawMatics API.
    /// </summary>
    public class SubtasksClient : BaseClient
    {
        public SubtasksClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Gets all subtasks with optional pagination and filtering.
        /// </summary>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="taskId">Filter by parent task ID.</param>
        /// <param name="isCompleted">Filter by completion status.</param>
        /// <param name="title">Filter by subtask title.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of subtasks.</returns>
        public async Task<PagedResponse<Subtask>> GetSubtasksAsync(
            int page = 1,
            int pageSize = 20,
            int? taskId = null,
            bool? isCompleted = null,
            string? title = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            if (taskId.HasValue)
                parameters["task_id"] = taskId.Value.ToString();

            if (isCompleted.HasValue)
                parameters["is_completed"] = isCompleted.Value.ToString();

            if (!string.IsNullOrEmpty(title))
                parameters["title"] = title;

            return await GetAsync<PagedResponse<Subtask>>("subtasks", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets a specific subtask by ID.
        /// </summary>
        /// <param name="id">The subtask ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The subtask details.</returns>
        public async Task<Subtask?> GetSubtaskAsync(int id, CancellationToken cancellationToken = default)
        {
            return await GetAsync<Subtask>($"subtasks/{id}", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Creates a new subtask.
        /// </summary>
        /// <param name="request">The subtask creation request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created subtask.</returns>
        public async Task<Subtask?> CreateSubtaskAsync(CreateSubtaskRequest request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<Subtask>("subtasks", request, cancellationToken);
        }

        /// <summary>
        /// Updates an existing subtask.
        /// </summary>
        /// <param name="id">The subtask ID.</param>
        /// <param name="request">The subtask update request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated subtask.</returns>
        public async Task<Subtask?> UpdateSubtaskAsync(int id, UpdateSubtaskRequest request, CancellationToken cancellationToken = default)
        {
            return await PutAsync<Subtask>($"subtasks/{id}", request, cancellationToken);
        }

        /// <summary>
        /// Deletes a subtask.
        /// </summary>
        /// <param name="id">The subtask ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if deletion was successful.</returns>
        public async Task<bool> DeleteSubtaskAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync($"subtasks/{id}", cancellationToken);
        }

        /// <summary>
        /// Gets subtasks for a specific task.
        /// </summary>
        /// <param name="taskId">The parent task ID.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of subtasks for the task.</returns>
        public async Task<PagedResponse<Subtask>> GetSubtasksByTaskAsync(
            int taskId,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetSubtasksAsync(page, pageSize, taskId: taskId, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets completed subtasks for a task.
        /// </summary>
        /// <param name="taskId">The parent task ID.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of completed subtasks for the task.</returns>
        public async Task<PagedResponse<Subtask>> GetCompletedSubtasksByTaskAsync(
            int taskId,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetSubtasksAsync(page, pageSize, taskId: taskId, isCompleted: true, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets incomplete subtasks for a task.
        /// </summary>
        /// <param name="taskId">The parent task ID.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of incomplete subtasks for the task.</returns>
        public async Task<PagedResponse<Subtask>> GetIncompleteSubtasksByTaskAsync(
            int taskId,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetSubtasksAsync(page, pageSize, taskId: taskId, isCompleted: false, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Marks a subtask as completed.
        /// </summary>
        /// <param name="id">The subtask ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated subtask.</returns>
        public async Task<Subtask?> CompleteSubtaskAsync(int id, CancellationToken cancellationToken = default)
        {
            return await PostAsync<Subtask>($"subtasks/{id}/complete", null, cancellationToken);
        }

        /// <summary>
        /// Marks a subtask as incomplete.
        /// </summary>
        /// <param name="id">The subtask ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated subtask.</returns>
        public async Task<Subtask?> ReopenSubtaskAsync(int id, CancellationToken cancellationToken = default)
        {
            return await PostAsync<Subtask>($"subtasks/{id}/reopen", null, cancellationToken);
        }

        /// <summary>
        /// Bulk creates subtasks for a task.
        /// </summary>
        /// <param name="taskId">The parent task ID.</param>
        /// <param name="requests">List of subtask creation requests.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of created subtasks.</returns>
        public async Task<List<Subtask>> BulkCreateSubtasksAsync(
            int taskId,
            List<CreateSubtaskRequest> requests,
            CancellationToken cancellationToken = default)
        {
            var bulkRequest = new
            {
                task_id = taskId,
                subtasks = requests
            };

            return await PostAsync<List<Subtask>>("subtasks/bulk", bulkRequest, cancellationToken) ?? new List<Subtask>();
        }

        /// <summary>
        /// Bulk completes subtasks for a task.
        /// </summary>
        /// <param name="taskId">The parent task ID.</param>
        /// <param name="subtaskIds">List of subtask IDs to complete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if bulk completion was successful.</returns>
        public async Task<bool> BulkCompleteSubtasksAsync(
            int taskId,
            List<int> subtaskIds,
            CancellationToken cancellationToken = default)
        {
            var bulkRequest = new
            {
                task_id = taskId,
                subtask_ids = subtaskIds
            };

            var response = await PostAsync<object>("subtasks/bulk-complete", bulkRequest, cancellationToken);
            return response != null;
        }

        /// <summary>
        /// Bulk deletes subtasks for a task.
        /// </summary>
        /// <param name="taskId">The parent task ID.</param>
        /// <param name="subtaskIds">List of subtask IDs to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if bulk deletion was successful.</returns>
        public async Task<bool> BulkDeleteSubtasksAsync(
            int taskId,
            List<int> subtaskIds,
            CancellationToken cancellationToken = default)
        {
            var bulkRequest = new
            {
                task_id = taskId,
                subtask_ids = subtaskIds
            };

            var response = await PostAsync<object>("subtasks/bulk-delete", bulkRequest, cancellationToken);
            return response != null;
        }

        /// <summary>
        /// Gets subtask statistics for a task.
        /// </summary>
        /// <param name="taskId">The parent task ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Subtask statistics for the task.</returns>
        public async Task<SubtaskStatistics?> GetSubtaskStatisticsByTaskAsync(int taskId, CancellationToken cancellationToken = default)
        {
            return await GetAsync<SubtaskStatistics>($"subtasks/tasks/{taskId}/statistics", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Searches subtasks by title or description.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="taskId">Filter by parent task ID (optional).</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of subtasks matching the search query.</returns>
        public async Task<PagedResponse<Subtask>> SearchSubtasksAsync(
            string query,
            int? taskId = null,
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

            if (taskId.HasValue)
                parameters["task_id"] = taskId.Value.ToString();

            return await GetAsync<PagedResponse<Subtask>>("subtasks", parameters, cancellationToken);
        }

        /// <summary>
        /// Reorders subtasks within a task.
        /// </summary>
        /// <param name="taskId">The parent task ID.</param>
        /// <param name="subtaskOrders">List of subtask IDs and their new order.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the reorder was successful.</returns>
        public async Task<bool> ReorderSubtasksAsync(
            int taskId,
            List<SubtaskOrder> subtaskOrders,
            CancellationToken cancellationToken = default)
        {
            var request = new
            {
                task_id = taskId,
                subtasks = subtaskOrders
            };

            var response = await PutAsync<object>("subtasks/reorder", request, cancellationToken);
            return response != null;
        }

        /// <summary>
        /// Duplicates a subtask.
        /// </summary>
        /// <param name="id">The subtask ID to duplicate.</param>
        /// <param name="newTitle">The title for the duplicated subtask.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The duplicated subtask.</returns>
        public async Task<Subtask?> DuplicateSubtaskAsync(
            int id,
            string newTitle,
            CancellationToken cancellationToken = default)
        {
            var request = new { title = newTitle };
            return await PostAsync<Subtask>($"subtasks/{id}/duplicate", request, cancellationToken);
        }

        /// <summary>
        /// Validates subtask data.
        /// </summary>
        /// <param name="request">The subtask request to validate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Validation result.</returns>
        public async Task<SubtaskValidationResult?> ValidateSubtaskAsync(
            CreateSubtaskRequest request,
            CancellationToken cancellationToken = default)
        {
            return await PostAsync<SubtaskValidationResult>("subtasks/validate", request, cancellationToken);
        }

        /// <summary>
        /// Exports subtasks to CSV.
        /// </summary>
        /// <param name="taskId">Filter by parent task ID (optional).</param>
        /// <param name="isCompleted">Filter by completion status (optional).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The CSV file content.</returns>
        public async Task<byte[]> ExportSubtasksToCsvAsync(
            int? taskId = null,
            bool? isCompleted = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["format"] = "csv"
            };

            if (taskId.HasValue)
                parameters["task_id"] = taskId.Value.ToString();

            if (isCompleted.HasValue)
                parameters["is_completed"] = isCompleted.Value.ToString();

            return await GetFileAsync("subtasks/export", parameters, cancellationToken);
        }
    }

    /// <summary>
    /// Represents subtask statistics.
    /// </summary>
    public class SubtaskStatistics
    {
        /// <summary>
        /// Gets or sets the task ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("task_id")]
        public int TaskId { get; set; }

        /// <summary>
        /// Gets or sets the total subtasks.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_subtasks")]
        public int TotalSubtasks { get; set; }

        /// <summary>
        /// Gets or sets the completed subtasks.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("completed_subtasks")]
        public int CompletedSubtasks { get; set; }

        /// <summary>
        /// Gets or sets the incomplete subtasks.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("incomplete_subtasks")]
        public int IncompleteSubtasks { get; set; }

        /// <summary>
        /// Gets or sets the completion percentage.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("completion_percentage")]
        public decimal CompletionPercentage { get; set; }

        /// <summary>
        /// Gets or sets the total completion time (hours).
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_completion_time_hours")]
        public decimal TotalCompletionTimeHours { get; set; }

        /// <summary>
        /// Gets or sets the average completion time (hours).
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("average_completion_time_hours")]
        public decimal AverageCompletionTimeHours { get; set; }
    }

    /// <summary>
    /// Represents subtask validation result.
    /// </summary>
    public class SubtaskValidationResult
    {
        /// <summary>
        /// Gets or sets whether the subtask is valid.
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
    /// Represents subtask order.
    /// </summary>
    public class SubtaskOrder
    {
        /// <summary>
        /// Gets or sets the subtask ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("subtask_id")]
        public int SubtaskId { get; set; }

        /// <summary>
        /// Gets or sets the order position.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("order")]
        public int Order { get; set; }
    }
}