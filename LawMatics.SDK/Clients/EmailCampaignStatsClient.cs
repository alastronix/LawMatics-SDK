using LawMatics.SDK.Configuration;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing email campaign statistics in the LawMatics API.
    /// </summary>
    public class EmailCampaignStatsClient : BaseClient
    {
        public EmailCampaignStatsClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Gets comprehensive statistics for a specific email campaign.
        /// </summary>
        /// <param name="campaignId">The email campaign ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Campaign statistics.</returns>
        public async Task<CampaignStatistics?> GetCampaignStatisticsAsync(int campaignId, CancellationToken cancellationToken = default)
        {
            return await GetAsync<CampaignStatistics>($"email-campaigns/{campaignId}/statistics", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets real-time statistics for a specific email campaign.
        /// </summary>
        /// <param name="campaignId">The email campaign ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Real-time campaign statistics.</returns>
        public async Task<RealTimeCampaignStats?> GetRealTimeCampaignStatsAsync(int campaignId, CancellationToken cancellationToken = default)
        {
            return await GetAsync<RealTimeCampaignStats>($"email-campaigns/{campaignId}/realtime-stats", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets campaign performance over time.
        /// </summary>
        /// <param name="campaignId">The email campaign ID.</param>
        /// <param name="dateFrom">Start date.</param>
        /// <param name="dateTo">End date.</param>
        /// <param name="groupBy">How to group the data (hour, day, week).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Campaign performance over time.</returns>
        public async Task<CampaignPerformanceOverTime?> GetCampaignPerformanceOverTimeAsync(
            int campaignId,
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

            return await GetAsync<CampaignPerformanceOverTime>($"email-campaigns/{campaignId}/performance", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets email engagement statistics for a campaign.
        /// </summary>
        /// <param name="campaignId">The email campaign ID.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Email engagement statistics.</returns>
        public async Task<PagedResponse<EmailEngagement>> GetEmailEngagementStatsAsync(
            int campaignId,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            return await GetAsync<PagedResponse<EmailEngagement>>($"email-campaigns/{campaignId}/engagement", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets click-through statistics for a campaign.
        /// </summary>
        /// <param name="campaignId">The email campaign ID.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Click-through statistics.</returns>
        public async Task<PagedResponse<ClickThroughStats>> GetClickThroughStatsAsync(
            int campaignId,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            return await GetAsync<PagedResponse<ClickThroughStats>>($"email-campaigns/{campaignId}/clicks", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets bounce and complaint statistics for a campaign.
        /// </summary>
        /// <param name="campaignId">The email campaign ID.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Bounce and complaint statistics.</returns>
        public async Task<PagedResponse<BounceComplaintStats>> GetBounceComplaintStatsAsync(
            int campaignId,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            return await GetAsync<PagedResponse<BounceComplaintStats>>($"email-campaigns/{campaignId}/bounces", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets geographic statistics for a campaign.
        /// </summary>
        /// <param name="campaignId">The email campaign ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Geographic statistics.</returns>
        public async Task<CampaignGeographicStats?> GetGeographicStatsAsync(int campaignId, CancellationToken cancellationToken = default)
        {
            return await GetAsync<CampaignGeographicStats>($"email-campaigns/{campaignId}/geographic", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets device and client statistics for a campaign.
        /// </summary>
        /// <param name="campaignId">The email campaign ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Device and client statistics.</returns>
        public async Task<CampaignDeviceStats?> GetDeviceStatsAsync(int campaignId, CancellationToken cancellationToken = default)
        {
            return await GetAsync<CampaignDeviceStats>($"email-campaigns/{campaignId}/devices", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets time-based engagement statistics for a campaign.
        /// </summary>
        /// <param name="campaignId">The email campaign ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Time-based engagement statistics.</returns>
        public async Task<CampaignTimeStats?> GetTimeStatsAsync(int campaignId, CancellationToken cancellationToken = default)
        {
            return await GetAsync<CampaignTimeStats>($"email-campaigns/{campaignId}/time-stats", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets comparison statistics between multiple campaigns.
        /// </summary>
        /// <param name="campaignIds">List of campaign IDs to compare.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Comparison statistics.</returns>
        public async Task<CampaignComparison?> CompareCampaignsAsync(List<int> campaignIds, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["campaign_ids"] = string.Join(",", campaignIds)
            };

            return await GetAsync<CampaignComparison>("email-campaigns/compare", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets aggregate statistics for all campaigns within a date range.
        /// </summary>
        /// <param name="dateFrom">Start date.</param>
        /// <param name="dateTo">End date.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Aggregate campaign statistics.</returns>
        public async Task<AggregateCampaignStats?> GetAggregateCampaignStatsAsync(
            DateTime dateFrom,
            DateTime dateTo,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["date_from"] = dateFrom.ToString("yyyy-MM-dd"),
                ["date_to"] = dateTo.ToString("yyyy-MM-dd")
            };

            return await GetAsync<AggregateCampaignStats>("email-campaigns/aggregate-stats", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets top performing campaigns by metric.
        /// </summary>
        /// <param name="metric">The metric to rank by (open_rate, click_rate, conversion_rate).</param>
        /// <param name="dateFrom">Start date (optional).</param>
        /// <param name="dateTo">End date (optional).</param>
        /// <param name="limit">Maximum number of results to return (default: 10).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Top performing campaigns.</returns>
        public async Task<List<TopCampaignMetric>> GetTopCampaignsByMetricAsync(
            string metric,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            int limit = 10,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["metric"] = metric,
                ["limit"] = limit.ToString()
            };

            if (dateFrom.HasValue)
                parameters["date_from"] = dateFrom.Value.ToString("yyyy-MM-dd");

            if (dateTo.HasValue)
                parameters["date_to"] = dateTo.Value.ToString("yyyy-MM-dd");

            var response = await GetAsync<TopCampaignsResponse>("email-campaigns/top-by-metric", parameters, cancellationToken);
            return response?.TopCampaigns ?? new List<TopCampaignMetric>();
        }

        /// <summary>
        /// Exports campaign statistics to CSV.
        /// </summary>
        /// <param name="campaignId">The campaign ID.</param>
        /// <param name="includeDetails">Include detailed engagement data.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The CSV file content.</returns>
        public async Task<byte[]> ExportCampaignStatsToCsvAsync(
            int campaignId,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["format"] = "csv",
                ["include_details"] = includeDetails.ToString()
            };

            return await GetFileAsync($"email-campaigns/{campaignId}/export", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets campaign ROI statistics.
        /// </summary>
        /// <param name="campaignId">The campaign ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>ROI statistics.</returns>
        public async Task<CampaignROI?> GetCampaignROIAsync(int campaignId, CancellationToken cancellationToken = default)
        {
            return await GetAsync<CampaignROI>($"email-campaigns/{campaignId}/roi", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets campaign A/B test results.
        /// </summary>
        /// <param name="campaignId">The campaign ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A/B test results.</returns>
        public async Task<CampaignABTestResults?> GetCampaignABTestResultsAsync(int campaignId, CancellationToken cancellationToken = default)
        {
            return await GetAsync<CampaignABTestResults>($"email-campaigns/{campaignId}/ab-test-results", cancellationToken: cancellationToken);
        }
    }

    /// <summary>
    /// Represents comprehensive campaign statistics.
    /// </summary>
    public class CampaignStatistics
    {
        /// <summary>
        /// Gets or sets the campaign ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("campaign_id")]
        public int CampaignId { get; set; }

        /// <summary>
        /// Gets or sets the campaign name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("campaign_name")]
        public string CampaignName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total sent.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("sent")]
        public int Sent { get; set; }

        /// <summary>
        /// Gets or sets the total delivered.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("delivered")]
        public int Delivered { get; set; }

        /// <summary>
        /// Gets or sets the total opens.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("opens")]
        public int Opens { get; set; }

        /// <summary>
        /// Gets or sets the total clicks.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("clicks")]
        public int Clicks { get; set; }

        /// <summary>
        /// Gets or sets the total bounces.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("bounces")]
        public int Bounces { get; set; }

        /// <summary>
        /// Gets or sets the total complaints.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("complaints")]
        public int Complaints { get; set; }

        /// <summary>
        /// Gets or sets the open rate.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("open_rate")]
        public decimal OpenRate { get; set; }

        /// <summary>
        /// Gets or sets the click rate.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("click_rate")]
        public decimal ClickRate { get; set; }

        /// <summary>
        /// Gets or sets the bounce rate.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("bounce_rate")]
        public decimal BounceRate { get; set; }

        /// <summary>
        /// Gets or sets the complaint rate.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("complaint_rate")]
        public decimal ComplaintRate { get; set; }

        /// <summary>
        /// Gets or sets the unique opens.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("unique_opens")]
        public int UniqueOpens { get; set; }

        /// <summary>
        /// Gets or sets the unique clicks.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("unique_clicks")]
        public int UniqueClicks { get; set; }

        /// <summary>
        /// Gets or sets the unsubscribe rate.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("unsubscribe_rate")]
        public decimal UnsubscribeRate { get; set; }

        /// <summary>
        /// Gets or sets the conversion rate.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("conversion_rate")]
        public decimal ConversionRate { get; set; }

        /// <summary>
        /// Gets or sets the revenue generated.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("revenue")]
        public decimal Revenue { get; set; }

        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("cost")]
        public decimal Cost { get; set; }

        /// <summary>
        /// Gets or sets the last updated timestamp.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("last_updated")]
        public DateTime LastUpdated { get; set; }
    }

    /// <summary>
    /// Represents real-time campaign statistics.
    /// </summary>
    public class RealTimeCampaignStats
    {
        /// <summary>
        /// Gets or sets the campaign ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("campaign_id")]
        public int CampaignId { get; set; }

        /// <summary>
        /// Gets or sets the current sending rate (emails per minute).
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("sending_rate_per_minute")]
        public decimal SendingRatePerMinute { get; set; }

        /// <summary>
        /// Gets or sets the current open rate.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("current_open_rate")]
        public decimal CurrentOpenRate { get; set; }

        /// <summary>
        /// Gets or sets the current click rate.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("current_click_rate")]
        public decimal CurrentClickRate { get; set; }

        /// <summary>
        /// Gets or sets the estimated completion time.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("estimated_completion_time")]
        public DateTime? EstimatedCompletionTime { get; set; }

        /// <summary>
        /// Gets or sets the percentage complete.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("percentage_complete")]
        public decimal PercentageComplete { get; set; }

        /// <summary>
        /// Gets or sets the last updated timestamp.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("last_updated")]
        public DateTime LastUpdated { get; set; }
    }

    /// <summary>
    /// Represents campaign performance over time.
    /// </summary>
    public class CampaignPerformanceOverTime
    {
        /// <summary>
        /// Gets or sets the campaign ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("campaign_id")]
        public int CampaignId { get; set; }

        /// <summary>
        /// Gets or sets the performance data points.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("data_points")]
        public List<PerformanceDataPoint> DataPoints { get; set; } = new();
    }

    /// <summary>
    /// Represents a performance data point.
    /// </summary>
    public class PerformanceDataPoint
    {
        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the number of sends.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("sends")]
        public int Sends { get; set; }

        /// <summary>
        /// Gets or sets the number of opens.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("opens")]
        public int Opens { get; set; }

        /// <summary>
        /// Gets or sets the number of clicks.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("clicks")]
        public int Clicks { get; set; }

        /// <summary>
        /// Gets or sets the open rate.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("open_rate")]
        public decimal OpenRate { get; set; }

        /// <summary>
        /// Gets or sets the click rate.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("click_rate")]
        public decimal ClickRate { get; set; }
    }

    /// <summary>
    /// Represents email engagement statistics.
    /// </summary>
    public class EmailEngagement
    {
        /// <summary>
        /// Gets or sets the contact ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("contact_id")]
        public int ContactId { get; set; }

        /// <summary>
        /// Gets or sets the contact email.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("contact_email")]
        public string ContactEmail { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether the email was opened.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("was_opened")]
        public bool WasOpened { get; set; }

        /// <summary>
        /// Gets or sets the open count.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("open_count")]
        public int OpenCount { get; set; }

        /// <summary>
        /// Gets or sets whether the email was clicked.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("was_clicked")]
        public bool WasClicked { get; set; }

        /// <summary>
        /// Gets or sets the click count.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("click_count")]
        public int ClickCount { get; set; }

        /// <summary>
        /// Gets or sets the first open time.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("first_open_time")]
        public DateTime? FirstOpenTime { get; set; }

        /// <summary>
        /// Gets or sets the first click time.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("first_click_time")]
        public DateTime? FirstClickTime { get; set; }

        /// <summary>
        /// Gets or sets the last open time.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("last_open_time")]
        public DateTime? LastOpenTime { get; set; }

        /// <summary>
        /// Gets or sets the last click time.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("last_click_time")]
        public DateTime? LastClickTime { get; set; }
    }

    /// <summary>
    /// Represents click-through statistics.
    /// </summary>
    public class ClickThroughStats
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total clicks.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_clicks")]
        public int TotalClicks { get; set; }

        /// <summary>
        /// Gets or sets the unique clicks.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("unique_clicks")]
        public int UniqueClicks { get; set; }

        /// <summary>
        /// Gets or sets the click rate.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("click_rate")]
        public decimal ClickRate { get; set; }
    }

    /// <summary>
    /// Represents bounce and complaint statistics.
    /// </summary>
    public class BounceComplaintStats
    {
        /// <summary>
        /// Gets or sets the contact email.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("contact_email")]
        public string ContactEmail { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the bounce type (hard, soft).
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("bounce_type")]
        public string? BounceType { get; set; }

        /// <summary>
        /// Gets or sets the bounce reason.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("bounce_reason")]
        public string? BounceReason { get; set; }

        /// <summary>
        /// Gets or sets whether it's a complaint.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("is_complaint")]
        public bool IsComplaint { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Represents campaign geographic statistics.
    /// </summary>
    public class CampaignGeographicStats
    {
        /// <summary>
        /// Gets or sets the campaign ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("campaign_id")]
        public int CampaignId { get; set; }

        /// <summary>
        /// Gets or sets the breakdown by country.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("by_country")]
        public List<GeographicBreakdown> ByCountry { get; set; } = new();

        /// <summary>
        /// Gets or sets the breakdown by city.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("by_city")]
        public List<GeographicBreakdown> ByCity { get; set; } = new();
    }

    /// <summary>
    /// Represents geographic breakdown.
    /// </summary>
    public class GeographicBreakdown
    {
        /// <summary>
        /// Gets or sets the location name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("location")]
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the open count.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("opens")]
        public int Opens { get; set; }

        /// <summary>
        /// Gets or sets the click count.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("clicks")]
        public int Clicks { get; set; }

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("percentage")]
        public decimal Percentage { get; set; }
    }

    /// <summary>
    /// Represents campaign device statistics.
    /// </summary>
    public class CampaignDeviceStats
    {
        /// <summary>
        /// Gets or sets the campaign ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("campaign_id")]
        public int CampaignId { get; set; }

        /// <summary>
        /// Gets or sets the breakdown by device type.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("by_device_type")]
        public List<DeviceBreakdown> ByDeviceType { get; set; } = new();

        /// <summary>
        /// Gets or sets the breakdown by email client.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("by_email_client")]
        public List<ClientBreakdown> ByEmailClient { get; set; } = new();
    }

    /// <summary>
    /// Represents device breakdown.
    /// </summary>
    public class DeviceBreakdown
    {
        /// <summary>
        /// Gets or sets the device type.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("device_type")]
        public string DeviceType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the open count.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("opens")]
        public int Opens { get; set; }

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("percentage")]
        public decimal Percentage { get; set; }
    }

    /// <summary>
    /// Represents email client breakdown.
    /// </summary>
    public class ClientBreakdown
    {
        /// <summary>
        /// Gets or sets the email client.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("email_client")]
        public string EmailClient { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the open count.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("opens")]
        public int Opens { get; set; }

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("percentage")]
        public decimal Percentage { get; set; }
    }

    /// <summary>
    /// Represents campaign time statistics.
    /// </summary>
    public class CampaignTimeStats
    {
        /// <summary>
        /// Gets or sets the campaign ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("campaign_id")]
        public int CampaignId { get; set; }

        /// <summary>
        /// Gets or sets the best sending time.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("best_sending_time")]
        public string BestSendingTime { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the best sending day.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("best_sending_day")]
        public string BestSendingDay { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the breakdown by hour.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("by_hour")]
        public List<HourBreakdown> ByHour { get; set; } = new();

        /// <summary>
        /// Gets or sets the breakdown by day of week.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("by_day_of_week")]
        public List<DayBreakdown> ByDayOfWeek { get; set; } = new();
    }

    /// <summary>
    /// Represents hour breakdown.
    /// </summary>
    public class HourBreakdown
    {
        /// <summary>
        /// Gets or sets the hour (0-23).
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("hour")]
        public int Hour { get; set; }

        /// <summary>
        /// Gets or sets the open count.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("opens")]
        public int Opens { get; set; }

        /// <summary>
        /// Gets or sets the click count.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("clicks")]
        public int Clicks { get; set; }
    }

    /// <summary>
    /// Represents day breakdown.
    /// </summary>
    public class DayBreakdown
    {
        /// <summary>
        /// Gets or sets the day of week.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("day_of_week")]
        public string DayOfWeek { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the open count.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("opens")]
        public int Opens { get; set; }

        /// <summary>
        /// Gets or sets the click count.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("clicks")]
        public int Clicks { get; set; }
    }

    /// <summary>
    /// Represents campaign comparison.
    /// </summary>
    public class CampaignComparison
    {
        /// <summary>
        /// Gets or sets the compared campaigns.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("campaigns")]
        public List<CampaignComparisonItem> Campaigns { get; set; } = new();

        /// <summary>
        /// Gets or sets the comparison summary.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("summary")]
        public ComparisonSummary Summary { get; set; } = new();
    }

    /// <summary>
    /// Represents a campaign comparison item.
    /// </summary>
    public class CampaignComparisonItem
    {
        /// <summary>
        /// Gets or sets the campaign ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("campaign_id")]
        public int CampaignId { get; set; }

        /// <summary>
        /// Gets or sets the campaign name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("campaign_name")]
        public string CampaignName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the open rate.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("open_rate")]
        public decimal OpenRate { get; set; }

        /// <summary>
        /// Gets or sets the click rate.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("click_rate")]
        public decimal ClickRate { get; set; }

        /// <summary>
        /// Gets or sets the conversion rate.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("conversion_rate")]
        public decimal ConversionRate { get; set; }

        /// <summary>
        /// Gets or sets the ROI.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("roi")]
        public decimal ROI { get; set; }
    }

    /// <summary>
    /// Represents comparison summary.
    /// </summary>
    public class ComparisonSummary
    {
        /// <summary>
        /// Gets or sets the best open rate campaign.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("best_open_rate_campaign")]
        public int BestOpenRateCampaign { get; set; }

        /// <summary>
        /// Gets or sets the best click rate campaign.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("best_click_rate_campaign")]
        public int BestClickRateCampaign { get; set; }

        /// <summary>
        /// Gets or sets the best conversion rate campaign.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("best_conversion_rate_campaign")]
        public int BestConversionRateCampaign { get; set; }

        /// <summary>
        /// Gets or sets the best ROI campaign.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("best_roi_campaign")]
        public int BestROICampaign { get; set; }
    }

    /// <summary>
    /// Represents aggregate campaign statistics.
    /// </summary>
    public class AggregateCampaignStats
    {
        /// <summary>
        /// Gets or sets the total campaigns.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_campaigns")]
        public int TotalCampaigns { get; set; }

        /// <summary>
        /// Gets or sets the total emails sent.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_emails_sent")]
        public int TotalEmailsSent { get; set; }

        /// <summary>
        /// Gets or sets the total opens.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_opens")]
        public int TotalOpens { get; set; }

        /// <summary>
        /// Gets or sets the total clicks.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_clicks")]
        public int TotalClicks { get; set; }

        /// <summary>
        /// Gets or sets the average open rate.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("average_open_rate")]
        public decimal AverageOpenRate { get; set; }

        /// <summary>
        /// Gets or sets the average click rate.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("average_click_rate")]
        public decimal AverageClickRate { get; set; }

        /// <summary>
        /// Gets or sets the total revenue.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_revenue")]
        public decimal TotalRevenue { get; set; }

        /// <summary>
        /// Gets or sets the total cost.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_cost")]
        public decimal TotalCost { get; set; }

        /// <summary>
        /// Gets or sets the average ROI.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("average_roi")]
        public decimal AverageROI { get; set; }
    }

    /// <summary>
    /// Response model for top campaigns.
    /// </summary>
    public class TopCampaignsResponse
    {
        /// <summary>
        /// Gets or sets the list of top campaigns.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("top_campaigns")]
        public List<TopCampaignMetric> TopCampaigns { get; set; } = new();
    }

    /// <summary>
    /// Represents a top campaign by metric.
    /// </summary>
    public class TopCampaignMetric
    {
        /// <summary>
        /// Gets or sets the campaign ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("campaign_id")]
        public int CampaignId { get; set; }

        /// <summary>
        /// Gets or sets the campaign name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("campaign_name")]
        public string CampaignName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the metric value.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("metric_value")]
        public decimal MetricValue { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("rank")]
        public int Rank { get; set; }
    }

    /// <summary>
    /// Represents campaign ROI.
    /// </summary>
    public class CampaignROI
    {
        /// <summary>
        /// Gets or sets the campaign ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("campaign_id")]
        public int CampaignId { get; set; }

        /// <summary>
        /// Gets or sets the ROI percentage.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("roi_percentage")]
        public decimal ROIPercentage { get; set; }

        /// <summary>
        /// Gets or sets the revenue.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("revenue")]
        public decimal Revenue { get; set; }

        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("cost")]
        public decimal Cost { get; set; }

        /// <summary>
        /// Gets or sets the profit.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("profit")]
        public decimal Profit { get; set; }

        /// <summary>
        /// Gets or sets the cost per acquisition.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("cost_per_acquisition")]
        public decimal CostPerAcquisition { get; set; }

        /// <summary>
        /// Gets or sets the customer lifetime value.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("customer_lifetime_value")]
        public decimal CustomerLifetimeValue { get; set; }
    }

    /// <summary>
    /// Represents campaign A/B test results.
    /// </summary>
    public class CampaignABTestResults
    {
        /// <summary>
        /// Gets or sets the campaign ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("campaign_id")]
        public int CampaignId { get; set; }

        /// <summary>
        /// Gets or sets the test variants.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("variants")]
        public List<ABTestVariant> Variants { get; set; } = new();

        /// <summary>
        /// Gets or sets the winning variant.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("winning_variant")]
        public int WinningVariant { get; set; }

        /// <summary>
        /// Gets or sets the statistical significance.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("statistical_significance")]
        public decimal StatisticalSignificance { get; set; }

        /// <summary>
        /// Gets or sets the test duration in hours.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("test_duration_hours")]
        public int TestDurationHours { get; set; }
    }

    /// <summary>
    /// Represents an A/B test variant.
    /// </summary>
    public class ABTestVariant
    {
        /// <summary>
        /// Gets or sets the variant ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("variant_id")]
        public int VariantId { get; set; }

        /// <summary>
        /// Gets or sets the variant name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("variant_name")]
        public string VariantName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the open rate.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("open_rate")]
        public decimal OpenRate { get; set; }

        /// <summary>
        /// Gets or sets the click rate.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("click_rate")]
        public decimal ClickRate { get; set; }

        /// <summary>
        /// Gets or sets the conversion rate.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("conversion_rate")]
        public decimal ConversionRate { get; set; }

        /// <summary>
        /// Gets or sets the confidence level.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("confidence_level")]
        public decimal ConfidenceLevel { get; set; }
    }
}