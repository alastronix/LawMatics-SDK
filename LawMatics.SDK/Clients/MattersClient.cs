using LawMatics.SDK.Configuration;
using LawMatics.SDK.Exceptions;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing matters (prospects) in the Lawmatics API
    /// </summary>
    public class MattersClient : BaseClient
    {
        public MattersClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger = null)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Get all matters with optional filtering and pagination
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>Paginated list of matters</returns>
        public async Task<ApiResponse<List<Matter>>> GetMattersAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Matter>>($"/matters?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get a specific matter by ID
        /// </summary>
        /// <param name="id">Matter ID</param>
        /// <returns>Matter details</returns>
        public async Task<Matter> GetMatterAsync(int id)
        {
            var response = await GetAsync<Matter>($"/matters/{id}");
            return response.Data;
        }

        /// <summary>
        /// Create a new matter
        /// </summary>
        /// <param name="request">Matter creation request</param>
        /// <returns>Created matter</returns>
        public async Task<Matter> CreateMatterAsync(CreateMatterRequest request)
        {
            var response = await PostAsync<Matter>("/matters", request);
            return response.Data;
        }

        /// <summary>
        /// Update an existing matter
        /// </summary>
        /// <param name="id">Matter ID</param>
        /// <param name="request">Matter update request</param>
        /// <returns>Updated matter</returns>
        public async Task<Matter> UpdateMatterAsync(int id, CreateMatterRequest request)
        {
            var response = await PutAsync<Matter>($"/matters/{id}", request);
            return response.Data;
        }

        /// <summary>
        /// Delete a matter
        /// </summary>
        /// <param name="id">Matter ID</param>
        public async Task DeleteMatterAsync(int id)
        {
            await DeleteAsync($"/matters/{id}");
        }

        /// <summary>
        /// Get matters by status
        /// </summary>
        /// <param name="status">Status to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of matters with specified status</returns>
        public async Task<ApiResponse<List<Matter>>> GetMattersByStatusAsync(string status, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Matter>>($"/matters?status={status}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get matters by sub-status
        /// </summary>
        /// <param name="subStatus">Sub-status to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of matters with specified sub-status</returns>
        public async Task<ApiResponse<List<Matter>>> GetMattersBySubStatusAsync(string subStatus, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Matter>>($"/matters?sub_status={subStatus}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get matters by practice area
        /// </summary>
        /// <param name="practiceArea">Practice area to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of matters in specified practice area</returns>
        public async Task<ApiResponse<List<Matter>>> GetMattersByPracticeAreaAsync(string practiceArea, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Matter>>($"/matters?practice_area={practiceArea}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get matters by priority
        /// </summary>
        /// <param name="priority">Priority to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of matters with specified priority</returns>
        public async Task<ApiResponse<List<Matter>>> GetMattersByPriorityAsync(string priority, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Matter>>($"/matters?priority={priority}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get matters assigned to a specific user
        /// </summary>
        /// <param name="assignedToId">User ID to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of matters assigned to specified user</returns>
        public async Task<ApiResponse<List<Matter>>> GetMattersByAssignedUserAsync(int assignedToId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Matter>>($"/matters?assigned_to_id={assignedToId}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get matters for a specific contact
        /// </summary>
        /// <param name="contactId">Contact ID to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of matters for specified contact</returns>
        public async Task<ApiResponse<List<Matter>>> GetMattersByContactAsync(int contactId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Matter>>($"/matters?contact_id={contactId}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get matters for a specific company
        /// </summary>
        /// <param name="companyId">Company ID to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of matters for specified company</returns>
        public async Task<ApiResponse<List<Matter>>> GetMattersByCompanyAsync(int companyId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Matter>>($"/matters?company_id={companyId}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get matters within a date range
        /// </summary>
        /// <param name="startDate">Start date filter</param>
        /// <param name="endDate">End date filter</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of matters within date range</returns>
        public async Task<ApiResponse<List<Matter>>> GetMattersByDateRangeAsync(DateTime startDate, DateTime endDate, FilterParameters? parameters = null)
        {
            var filterParams = parameters ?? new FilterParameters();
            filterParams.StartDate = startDate;
            filterParams.EndDate = endDate;
            
            var queryParams = BuildQueryString(filterParams);
            var response = await GetAsync<List<Matter>>($"/matters?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get matter files
        /// </summary>
        /// <param name="matterId">Matter ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of matter files</returns>
        public async Task<ApiResponse<List<LawMaticsFile>>> GetMatterFilesAsync(int matterId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<LawMaticsFile>>($"/matters/{matterId}/files?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get matter notes
        /// </summary>
        /// <param name="matterId">Matter ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of matter notes</returns>
        public async Task<ApiResponse<List<Note>>> GetMatterNotesAsync(int matterId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Note>>($"/matters/{matterId}/notes?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get matter events
        /// </summary>
        /// <param name="matterId">Matter ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of matter events</returns>
        public async Task<ApiResponse<List<Event>>> GetMatterEventsAsync(int matterId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Event>>($"/matters/{matterId}/events?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get matter payments
        /// </summary>
        /// <param name="matterId">Matter ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of matter payments</returns>
        public async Task<ApiResponse<List<Payment>>> GetMatterPaymentsAsync(int matterId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Payment>>($"/matters/{matterId}/payments?{queryParams}");
            return response;
        }

        /// <summary>
        /// Add tag to matter
        /// </summary>
        /// <param name="matterId">Matter ID</param>
        /// <param name="tag">Tag to add</param>
        public async Task AddTagToMatterAsync(int matterId, string tag)
        {
            await PostAsync<object>($"/matters/{matterId}/tags", new { tag });
        }

        /// <summary>
        /// Remove tag from matter
        /// </summary>
        /// <param name="matterId">Matter ID</param>
        /// <param name="tag">Tag to remove</param>
        public async Task RemoveTagFromMatterAsync(int matterId, string tag)
        {
            await DeleteAsync($"/matters/{matterId}/tags/{tag}");
        }

        #region Matter Sub-Status Management

        /// <summary>
        /// Get all matter sub-statuses
        /// </summary>
        /// <returns>List of matter sub-statuses</returns>
        public async Task<ApiResponse<List<MatterSubStatus>>> GetMatterSubStatusesAsync()
        {
            var response = await GetAsync<List<MatterSubStatus>>("/matter-sub-statuses");
            return response;
        }

        /// <summary>
        /// Get a specific matter sub-status by ID
        /// </summary>
        /// <param name="id">Sub-status ID</param>
        /// <returns>Sub-status details</returns>
        public async Task<MatterSubStatus> GetMatterSubStatusAsync(int id)
        {
            var response = await GetAsync<MatterSubStatus>($"/matter-sub-statuses/{id}");
            return response.Data;
        }

        /// <summary>
        /// Create a new matter sub-status
        /// </summary>
        /// <param name="request">Sub-status creation request</param>
        /// <returns>Created sub-status</returns>
        public async Task<MatterSubStatus> CreateMatterSubStatusAsync(MatterSubStatus request)
        {
            var response = await PostAsync<MatterSubStatus>("/matter-sub-statuses", request);
            return response.Data;
        }

        /// <summary>
        /// Update an existing matter sub-status
        /// </summary>
        /// <param name="id">Sub-status ID</param>
        /// <param name="request">Sub-status update request</param>
        /// <returns>Updated sub-status</returns>
        public async Task<MatterSubStatus> UpdateMatterSubStatusAsync(int id, MatterSubStatus request)
        {
            var response = await PutAsync<MatterSubStatus>($"/matter-sub-statuses/{id}", request);
            return response.Data;
        }

        /// <summary>
        /// Delete a matter sub-status
        /// </summary>
        /// <param name="id">Sub-status ID</param>
        public async Task DeleteMatterSubStatusAsync(int id)
        {
            await DeleteAsync($"/matter-sub-statuses/{id}");
        }

        #endregion

        /// <summary>
        /// Bulk create multiple matters
        /// </summary>
        /// <param name="requests">List of matter creation requests</param>
        /// <returns>List of created matters</returns>
        public async Task<List<Matter>> BulkCreateMattersAsync(List<CreateMatterRequest> requests)
        {
            var response = await PostAsync<List<Matter>>("/matters/bulk", new { matters = requests });
            return response.Data;
        }

        /// <summary>
        /// Bulk update multiple matters
        /// </summary>
        /// <param name="updates">Dictionary of matter IDs to update requests</param>
        /// <returns>List of updated matters</returns>
        public async Task<List<Matter>> BulkUpdateMattersAsync(Dictionary<int, CreateMatterRequest> updates)
        {
            var response = await PutAsync<List<Matter>>("/matters/bulk", new { updates });
            return response.Data;
        }
    }
}