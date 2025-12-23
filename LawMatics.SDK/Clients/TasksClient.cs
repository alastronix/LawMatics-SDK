using LawMatics.SDK.Configuration;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing tasks in the LawMatics API.
    /// </summary>
    public class TasksClient : BaseClient
    {
        public TasksClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Gets all tasks with optional pagination and filtering.
        /// </summary>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="statusId">Filter by status ID.</param>
        /// <param name="assignedToId">Filter by assigned user ID.</param>
        /// <param name="matterId">Filter by matter ID.</param>
        /// <param name="contactId">Filter by contact ID.</param>
        /// <param name="isCompleted">Filter by completion status.</param>
        /// <param name="dueDateFrom">Filter by due date from.</param>
        /// <param name="dueDateTo">Filter by due date to.</param>
        /// <param name="priority">Filter by priority.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of tasks.</returns>
        public async Task<PagedResponse<TaskItem>> GetTasksAsync(
            int page = 1,
            int pageSize = 20,
            int? statusId = null,
            int? assignedToId = null,
            int? matterId = null,
            int? contactId = null,
            bool? isCompleted = null,
            DateTime? dueDateFrom = null,
            DateTime? dueDateTo = null,
            string? priority = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            if (statusId.HasValue)
                parameters["status_id"] = statusId.Value.ToString();

            if (assignedToId.HasValue)
                parameters["assigned_to_id"] = assignedToId.Value.ToString();

            if (matterId.HasValue)
                parameters["matter_id"] = matterId.Value.ToString();

            if (contactId.HasValue)
                parameters["contact_id"] = contactId.Value.ToString();

            if (isCompleted.HasValue)
                parameters["is_completed"] = isCompleted.Value.ToString();

            if (dueDateFrom.HasValue)
                parameters["due_date_from"] = dueDateFrom.Value.ToString("yyyy-MM-dd");

            if (dueDateTo.HasValue)
                parameters["due_date_to"] = dueDateTo.Value.ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(priority))
                parameters["priority"] = priority;

            return await GetAsync<PagedResponse<TaskItem>>("tasks", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets a specific task by ID.
        /// </summary>
        /// <param name="id">The task ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The task details.</returns>
        public async Task<TaskItem?> GetTaskAsync(int id, CancellationToken cancellationToken = default)
        {
            return await GetAsync<TaskItem>($"tasks/{id}", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="request">The task creation request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created task.</returns>
        public async Task<TaskItem?> CreateTaskAsync(CreateTaskRequest request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<TaskItem>("tasks", request, cancellationToken);
        }

        /// <summary>
        /// Updates an existing task.
        /// </summary>
        /// <param name="id">The task ID.</param>
        /// <param name="request">The task update request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated task.</returns>
        public async Task<TaskItem?> UpdateTaskAsync(int id, UpdateTaskRequest request, CancellationToken cancellationToken = default)
        {
            return await PutAsync<TaskItem>($"tasks/{id}", request, cancellationToken);
        }

        /// <summary>
        /// Deletes a task.
        /// </summary>
        /// <param name="id">The task ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if deletion was successful.</returns>
        public async Task<bool> DeleteTaskAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync($"tasks/{id}", cancellationToken);
        }

        /// <summary>
        /// Marks a task as completed.
        /// </summary>
        /// <param name="id">The task ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated task.</returns>
        public async Task<TaskItem?> CompleteTaskAsync(int id, CancellationToken cancellationToken = default)
        {
            return await PostAsync<TaskItem>($"tasks/{id}/complete", null, cancellationToken);
        }

        /// <summary>
        /// Reopens a completed task.
        /// </summary>
        /// <param name="id">The task ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated task.</returns>
        public async Task<TaskItem?> ReopenTaskAsync(int id, CancellationToken cancellationToken = default)
        {
            return await PostAsync<TaskItem>($"tasks/{id}/reopen", null, cancellationToken);
        }

        /// <summary>
        /// Assigns a task to a user.
        /// </summary>
        /// <param name="id">The task ID.</param>
        /// <param name="userId">The user ID to assign to.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated task.</returns>
        public async Task<TaskItem?> AssignTaskAsync(int id, int userId, CancellationToken cancellationToken = default)
        {
            var request = new { assigned_to_id = userId };
            return await PostAsync<TaskItem>($"tasks/{id}/assign", request, cancellationToken);
        }

        /// <summary>
        /// Gets tasks assigned to a specific user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="isCompleted">Filter by completion status.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of tasks assigned to the user.</returns>
        public async Task<PagedResponse<TaskItem>> GetTasksByUserAsync(
            int userId,
            int page = 1,
            int pageSize = 20,
            bool? isCompleted = null,
            CancellationToken cancellationToken = default)
        {
            return await GetTasksAsync(page, pageSize, assignedToId: userId, isCompleted: isCompleted, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets tasks for a specific matter.
        /// </summary>
        /// <param name="matterId">The matter ID.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of tasks for the matter.</returns>
        public async Task<PagedResponse<TaskItem>> GetTasksByMatterAsync(
            int matterId,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetTasksAsync(page, pageSize, matterId: matterId, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets tasks for a specific contact.
        /// </summary>
        /// <param name="contactId">The contact ID.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of tasks for the contact.</returns>
        public async Task<PagedResponse<TaskItem>> GetTasksByContactAsync(
            int contactId,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetTasksAsync(page, pageSize, contactId: contactId, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets overdue tasks.
        /// </summary>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="assignedToId">Filter by assigned user ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of overdue tasks.</returns>
        public async Task<PagedResponse<TaskItem>> GetOverdueTasksAsync(
            int page = 1,
            int pageSize = 20,
            int? assignedToId = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString(),
                ["overdue"] = "true"
            };

            if (assignedToId.HasValue)
                parameters["assigned_to_id"] = assignedToId.Value.ToString();

            return await GetAsync<PagedResponse<TaskItem>>("tasks", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets tasks due within a specified date range.
        /// </summary>
        /// <param name="fromDate">Start date.</param>
        /// <param name="toDate">End date.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of tasks due in the specified range.</returns>
        public async Task<PagedResponse<TaskItem>> GetTasksDueInRangeAsync(
            DateTime fromDate,
            DateTime toDate,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetTasksAsync(page, pageSize, dueDateFrom: fromDate, dueDateTo: toDate, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Searches tasks by keyword.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of tasks matching the search query.</returns>
        public async Task<PagedResponse<TaskItem>> SearchTasksAsync(
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

            return await GetAsync<PagedResponse<TaskItem>>("tasks", parameters, cancellationToken);
        }
    }
}