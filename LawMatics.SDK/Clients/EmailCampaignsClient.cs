using LawMatics.SDK.Configuration;
using LawMatics.SDK.Exceptions;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing email campaigns in the Lawmatics API
    /// </summary>
    public class EmailCampaignsClient : BaseClient
    {
        public EmailCampaignsClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger = null)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Get all email campaigns with optional filtering and pagination
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>Paginated list of email campaigns</returns>
        public async Task<ApiResponse<List<EmailCampaign>>> GetCampaignsAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<EmailCampaign>>($"/email-campaigns?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get a specific email campaign by ID
        /// </summary>
        /// <param name="id">Campaign ID</param>
        /// <returns>Campaign details</returns>
        public async Task<EmailCampaign> GetCampaignAsync(int id)
        {
            var response = await GetAsync<EmailCampaign>($"/email-campaigns/{id}");
            return response.Data;
        }

        /// <summary>
        /// Create a new email campaign
        /// </summary>
        /// <param name="request">Campaign creation request</param>
        /// <returns>Created campaign</returns>
        public async Task<EmailCampaign> CreateCampaignAsync(CreateCampaignRequest request)
        {
            var response = await PostAsync<EmailCampaign>("/email-campaigns", request);
            return response.Data;
        }

        /// <summary>
        /// Update an existing email campaign
        /// </summary>
        /// <param name="id">Campaign ID</param>
        /// <param name="request">Campaign update request</param>
        /// <returns>Updated campaign</returns>
        public async Task<EmailCampaign> UpdateCampaignAsync(int id, CreateCampaignRequest request)
        {
            var response = await PutAsync<EmailCampaign>($"/email-campaigns/{id}", request);
            return response.Data;
        }

        /// <summary>
        /// Delete an email campaign
        /// </summary>
        /// <param name="id">Campaign ID</param>
        public async Task DeleteCampaignAsync(int id)
        {
            await DeleteAsync($"/email-campaigns/{id}");
        }

        /// <summary>
        /// Send an email campaign immediately
        /// </summary>
        /// <param name="id">Campaign ID</param>
        public async Task SendCampaignAsync(int id)
        {
            await PostAsync<object>($"/email-campaigns/{id}/send");
        }

        /// <summary>
        /// Schedule an email campaign to be sent later
        /// </summary>
        /// <param name="id">Campaign ID</param>
        /// <param name="scheduledAt">Date and time to send the campaign</param>
        public async Task ScheduleCampaignAsync(int id, DateTime scheduledAt)
        {
            await PostAsync<object>($"/email-campaigns/{id}/schedule", new { scheduled_at = scheduledAt });
        }

        /// <summary>
        /// Unschedule a previously scheduled campaign
        /// </summary>
        /// <param name="id">Campaign ID</param>
        public async Task UnscheduleCampaignAsync(int id)
        {
            await PostAsync<object>($"/email-campaigns/{id}/unschedule");
        }

        /// <summary>
        /// Pause an active campaign
        /// </summary>
        /// <param name="id">Campaign ID</param>
        public async Task PauseCampaignAsync(int id)
        {
            await PostAsync<object>($"/email-campaigns/{id}/pause");
        }

        /// <summary>
        /// Resume a paused campaign
        /// </summary>
        /// <param name="id">Campaign ID</param>
        public async Task ResumeCampaignAsync(int id)
        {
            await PostAsync<object>($"/email-campaigns/{id}/resume");
        }

        /// <summary>
        /// Cancel a campaign (stops sending and cannot be resumed)
        /// </summary>
        /// <param name="id">Campaign ID</param>
        public async Task CancelCampaignAsync(int id)
        {
            await PostAsync<object>($"/email-campaigns/{id}/cancel");
        }

        /// <summary>
        /// Get campaigns by status
        /// </summary>
        /// <param name="status">Status to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of campaigns with specified status</returns>
        public async Task<ApiResponse<List<EmailCampaign>>> GetCampaignsByStatusAsync(string status, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<EmailCampaign>>($"/email-campaigns?status={status}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get campaigns created by a specific user
        /// </summary>
        /// <param name="createdById">User ID to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of campaigns created by specified user</returns>
        public async Task<ApiResponse<List<EmailCampaign>>> GetCampaignsByCreatorAsync(int createdById, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<EmailCampaign>>($"/email-campaigns?created_by_id={createdById}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get campaigns created within a date range
        /// </summary>
        /// <param name="startDate">Start date filter</param>
        /// <param name="endDate">End date filter</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of campaigns created within date range</returns>
        public async Task<ApiResponse<List<EmailCampaign>>> GetCampaignsByDateRangeAsync(DateTime startDate, DateTime endDate, FilterParameters? parameters = null)
        {
            var filterParams = parameters ?? new FilterParameters();
            filterParams.StartDate = startDate;
            filterParams.EndDate = endDate;
            
            var queryParams = BuildQueryString(filterParams);
            var response = await GetAsync<List<EmailCampaign>>($"/email-campaigns?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get scheduled campaigns
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of scheduled campaigns</returns>
        public async Task<ApiResponse<List<EmailCampaign>>> GetScheduledCampaignsAsync(FilterParameters? parameters = null)
        {
            return await GetCampaignsByStatusAsync("scheduled", parameters);
        }

        /// <summary>
        /// Get active campaigns
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of active campaigns</returns>
        public async Task<ApiResponse<List<EmailCampaign>>> GetActiveCampaignsAsync(FilterParameters? parameters = null)
        {
            return await GetCampaignsByStatusAsync("active", parameters);
        }

        /// <summary>
        /// Get completed campaigns
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of completed campaigns</returns>
        public async Task<ApiResponse<List<EmailCampaign>>> GetCompletedCampaignsAsync(FilterParameters? parameters = null)
        {
            return await GetCampaignsByStatusAsync("completed", parameters);
        }

        /// <summary>
        /// Get paused campaigns
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of paused campaigns</returns>
        public async Task<ApiResponse<List<EmailCampaign>>> GetPausedCampaignsAsync(FilterParameters? parameters = null)
        {
            return await GetCampaignsByStatusAsync("paused", parameters);
        }

        /// <summary>
        /// Get cancelled campaigns
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of cancelled campaigns</returns>
        public async Task<ApiResponse<List<EmailCampaign>>> GetCancelledCampaignsAsync(FilterParameters? parameters = null)
        {
            return await GetCampaignsByStatusAsync("cancelled", parameters);
        }

        /// <summary>
        /// Duplicate an existing campaign
        /// </summary>
        /// <param name="id">Campaign ID to duplicate</param>
        /// <param name="newName">Optional new name for duplicated campaign</param>
        /// <returns>Duplicated campaign</returns>
        public async Task<EmailCampaign> DuplicateCampaignAsync(int id, string? newName = null)
        {
            var request = new { name = newName };
            var response = await PostAsync<EmailCampaign>($"/email-campaigns/{id}/duplicate", request);
            return response.Data;
        }

        /// <summary>
        /// Preview campaign content
        /// </summary>
        /// <param name="id">Campaign ID</param>
        /// <param name="testEmailAddress">Optional test email address to send preview to</param>
        /// <returns>Campaign preview content</returns>
        public async Task<string> PreviewCampaignAsync(int id, string? testEmailAddress = null)
        {
            var endpoint = string.IsNullOrEmpty(testEmailAddress) 
                ? $"/email-campaigns/{id}/preview" 
                : $"/email-campaigns/{id}/preview?test_email={Uri.EscapeDataString(testEmailAddress)}";
            
            var response = await GetAsync<string>(endpoint);
            return response.Data;
        }

        /// <summary>
        /// Send test campaign
        /// </summary>
        /// <param name="id">Campaign ID</param>
        /// <param name="testEmailAddresses">List of test email addresses</param>
        public async Task SendTestCampaignAsync(int id, List<string> testEmailAddresses)
        {
            await PostAsync<object>($"/email-campaigns/{id}/test", new { test_emails = testEmailAddresses });
        }

        /// <summary>
        /// Get campaign recipients
        /// </summary>
        /// <param name="id">Campaign ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of campaign recipients</returns>
        public async Task<ApiResponse<List<Contact>>> GetCampaignRecipientsAsync(int id, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Contact>>($"/email-campaigns/{id}/recipients?{queryParams}");
            return response;
        }

        /// <summary>
        /// Add recipients to campaign
        /// </summary>
        /// <param name="id">Campaign ID</param>
        /// <param name="contactIds">List of contact IDs to add</param>
        public async Task AddCampaignRecipientsAsync(int id, List<int> contactIds)
        {
            await PostAsync<object>($"/email-campaigns/{id}/recipients", new { contact_ids = contactIds });
        }

        /// <summary>
        /// Remove recipients from campaign
        /// </summary>
        /// <param name="id">Campaign ID</param>
        /// <param name="contactIds">List of contact IDs to remove</param>
        public async Task RemoveCampaignRecipientsAsync(int id, List<int> contactIds)
        {
            await PostAsync<object>($"/email-campaigns/{id}/recipients/remove", new { contact_ids = contactIds });
        }

        /// <summary>
        /// Get campaign delivery schedule
        /// </summary>
        /// <param name="id">Campaign ID</param>
        /// <returns>Delivery schedule information</returns>
        public async Task<object> GetCampaignScheduleAsync(int id)
        {
            var response = await GetAsync<object>($"/email-campaigns/{id}/schedule");
            return response.Data;
        }

        /// <summary>
        /// Bulk create multiple campaigns
        /// </summary>
        /// <param name="requests">List of campaign creation requests</param>
        /// <returns>List of created campaigns</returns>
        public async Task<List<EmailCampaign>> BulkCreateCampaignsAsync(List<CreateCampaignRequest> requests)
        {
            var response = await PostAsync<List<EmailCampaign>>("/email-campaigns/bulk", new { campaigns = requests });
            return response.Data;
        }

        /// <summary>
        /// Bulk send multiple campaigns
        /// </summary>
        /// <param name="campaignIds">List of campaign IDs to send</param>
        public async Task BulkSendCampaignsAsync(List<int> campaignIds)
        {
            await PostAsync<object>("/email-campaigns/bulk-send", new { campaign_ids = campaignIds });
        }

        /// <summary>
        /// Bulk delete multiple campaigns
        /// </summary>
        /// <param name="campaignIds">List of campaign IDs to delete</param>
        public async Task BulkDeleteCampaignsAsync(List<int> campaignIds)
        {
            await PostAsync<object>("/email-campaigns/bulk-delete", new { campaign_ids = campaignIds });
        }

        #region Email Campaign Statistics

        /// <summary>
        /// Get all campaign statistics
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of campaign statistics</returns>
        public async Task<ApiResponse<List<EmailCampaignStats>>> GetCampaignStatsAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<EmailCampaignStats>>($"/email-campaign-stats?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get statistics for a specific campaign
        /// </summary>
        /// <param name="id">Campaign ID</param>
        /// <returns>Campaign statistics</returns>
        public async Task<EmailCampaignStats> GetCampaignStatsAsync(int id)
        {
            var response = await GetAsync<EmailCampaignStats>($"/email-campaign-stats/{id}");
            return response.Data;
        }

        /// <summary>
        /// Get campaign engagement over time
        /// </summary>
        /// <param name="id">Campaign ID</param>
        /// <param name="startDate">Start date for analytics</param>
        /// <param name="endDate">End date for analytics</param>
        /// <returns>Engagement data over time</returns>
        public async Task<object> GetCampaignEngagementOverTimeAsync(int id, DateTime? startDate = null, DateTime? endDate = null)
        {
            var queryString = "";
            if (startDate.HasValue)
                queryString += $"&start_date={startDate.Value:yyyy-MM-dd}";
            if (endDate.HasValue)
                queryString += $"&end_date={endDate.Value:yyyy-MM-dd}";
            
            var endpoint = $"/email-campaign-stats/{id}/engagement{(!string.IsNullOrEmpty(queryString) ? $"?{queryString.Substring(1)}" : "")}";
            var response = await GetAsync<object>(endpoint);
            return response.Data;
        }

        /// <summary>
        /// Get campaign click tracking data
        /// </summary>
        /// <param name="id">Campaign ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>Click tracking data</returns>
        public async Task<object> GetCampaignClicksAsync(int id, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<object>($"/email-campaign-stats/{id}/clicks?{queryParams}");
            return response.Data;
        }

        /// <summary>
        /// Get campaign open tracking data
        /// </summary>
        /// <param name="id">Campaign ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>Open tracking data</returns>
        public async Task<object> GetCampaignOpensAsync(int id, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<object>($"/email-campaign-stats/{id}/opens?{queryParams}");
            return response.Data;
        }

        /// <summary>
        /// Get campaign bounce data
        /// </summary>
        /// <param name="id">Campaign ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>Bounce data</returns>
        public async Task<object> GetCampaignBouncesAsync(int id, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<object>($"/email-campaign-stats/{id}/bounces?{queryParams}");
            return response.Data;
        }

        /// <summary>
        /// Get campaign unsubscribe data
        /// </summary>
        /// <param name="id">Campaign ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>Unsubscribe data</returns>
        public async Task<object> GetCampaignUnsubscribesAsync(int id, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<object>($"/email-campaign-stats/{id}/unsubscribes?{queryParams}");
            return response.Data;
        }

        #endregion
    }
}