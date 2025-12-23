using System.Text.Json.Serialization;

namespace LawMatics.SDK.Models
{
    /// <summary>
    /// Represents an email campaign in Lawmatics
    /// </summary>
    public class EmailCampaign
    {
        /// <summary>
        /// Unique identifier for the campaign
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Campaign name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Campaign subject line
        /// </summary>
        [JsonPropertyName("subject")]
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Campaign description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Campaign status (draft, scheduled, active, paused, completed)
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Campaign type
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// HTML content of the email
        /// </summary>
        [JsonPropertyName("html_content")]
        public string? HtmlContent { get; set; }

        /// <summary>
        /// Plain text content of the email
        /// </summary>
        [JsonPropertyName("text_content")]
        public string? TextContent { get; set; }

        /// <summary>
        /// From email address
        /// </summary>
        [JsonPropertyName("from_email")]
        public string? FromEmail { get; set; }

        /// <summary>
        /// From name
        /// </summary>
        [JsonPropertyName("from_name")]
        public string? FromName { get; set; }

        /// <summary>
        /// Reply-to email address
        /// </summary>
        [JsonPropertyName("reply_to")]
        public string? ReplyTo { get; set; }

        /// <summary>
        /// Campaign template
        /// </summary>
        [JsonPropertyName("template")]
        public EmailTemplate? Template { get; set; }

        /// <summary>
        /// Target audience/segment
        /// </summary>
        [JsonPropertyName("audience")]
        public CampaignAudience? Audience { get; set; }

        /// <summary>
        /// Campaign schedule
        /// </summary>
        [JsonPropertyName("schedule")]
        public CampaignSchedule? Schedule { get; set; }

        /// <summary>
        /// Campaign statistics
        /// </summary>
        [JsonPropertyName("statistics")]
        public CampaignStatistics? Statistics { get; set; }

        /// <summary>
        /// Campaign settings
        /// </summary>
        [JsonPropertyName("settings")]
        public CampaignSettings? Settings { get; set; }

        /// <summary>
        /// Date when campaign was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when campaign was last updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Date when campaign was sent
        /// </summary>
        [JsonPropertyName("sent_at")]
        public DateTime? SentAt { get; set; }

        /// <summary>
        /// Custom fields for the campaign
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }

    /// <summary>
    /// Email template information
    /// </summary>
    public class EmailTemplate
    {
        /// <summary>
        /// Template ID
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Template name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Template description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    /// <summary>
    /// Campaign audience information
    /// </summary>
    public class CampaignAudience
    {
        /// <summary>
        /// Total number of recipients
        /// </summary>
        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }

        /// <summary>
        /// Number of contacts
        /// </summary>
        [JsonPropertyName("contacts_count")]
        public int ContactsCount { get; set; }

        /// <summary>
        /// Number of companies
        /// </summary>
        [JsonPropertyName("companies_count")]
        public int CompaniesCount { get; set; }

        /// <summary>
        /// Audience filters
        /// </summary>
        [JsonPropertyName("filters")]
        public Dictionary<string, object>? Filters { get; set; }
    }

    /// <summary>
    /// Campaign schedule information
    /// </summary>
    public class CampaignSchedule
    {
        /// <summary>
        /// Scheduled send date
        /// </summary>
        [JsonPropertyName("send_date")]
        public DateTime? SendDate { get; set; }

        /// <summary>
        /// Timezone for scheduling
        /// </summary>
        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }

        /// <summary>
        /// Whether to send immediately
        /// </summary>
        [JsonPropertyName("send_immediately")]
        public bool SendImmediately { get; set; }
    }

    /// <summary>
    /// Campaign statistics
    /// </summary>
    public class CampaignStatistics
    {
        /// <summary>
        /// Total emails sent
        /// </summary>
        [JsonPropertyName("sent")]
        public int Sent { get; set; }

        /// <summary>
        /// Total emails delivered
        /// </summary>
        [JsonPropertyName("delivered")]
        public int Delivered { get; set; }

        /// <summary>
        /// Total opens
        /// </summary>
        [JsonPropertyName("opens")]
        public int Opens { get; set; }

        /// <summary>
        /// Total clicks
        /// </summary>
        [JsonPropertyName("clicks")]
        public int Clicks { get; set; }

        /// <summary>
        /// Total bounces
        /// </summary>
        [JsonPropertyName("bounces")]
        public int Bounces { get; set; }

        /// <summary>
        /// Total unsubscribes
        /// </summary>
        [JsonPropertyName("unsubscribes")]
        public int Unsubscribes { get; set; }

        /// <summary>
        /// Open rate percentage
        /// </summary>
        [JsonPropertyName("open_rate")]
        public decimal? OpenRate { get; set; }

        /// <summary>
        /// Click rate percentage
        /// </summary>
        [JsonPropertyName("click_rate")]
        public decimal? ClickRate { get; set; }
    }

    /// <summary>
    /// Campaign settings
    /// </summary>
    public class CampaignSettings
    {
        /// <summary>
        /// Track opens
        /// </summary>
        [JsonPropertyName("track_opens")]
        public bool TrackOpens { get; set; }

        /// <summary>
        /// Track clicks
        /// </summary>
        [JsonPropertyName("track_clicks")]
        public bool TrackClicks { get; set; }

        /// <summary>
        /// Include unsubscribe link
        /// </summary>
        [JsonPropertyName("include_unsubscribe")]
        public bool IncludeUnsubscribe { get; set; }

        /// <summary>
        /// Personalize emails
        /// </summary>
        [JsonPropertyName("personalize")]
        public bool Personalize { get; set; }
    }

    /// <summary>
    /// Request model for creating a new email campaign
    /// </summary>
    public class CreateEmailCampaignRequest
    {
        /// <summary>
        /// Campaign name (required)
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Campaign subject line (required)
        /// </summary>
        [JsonPropertyName("subject")]
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Campaign description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Campaign type
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// HTML content of the email
        /// </summary>
        [JsonPropertyName("html_content")]
        public string? HtmlContent { get; set; }

        /// <summary>
        /// Plain text content of the email
        /// </summary>
        [JsonPropertyName("text_content")]
        public string? TextContent { get; set; }

        /// <summary>
        /// From email address
        /// </summary>
        [JsonPropertyName("from_email")]
        public string? FromEmail { get; set; }

        /// <summary>
        /// From name
        /// </summary>
        [JsonPropertyName("from_name")]
        public string? FromName { get; set; }

        /// <summary>
        /// Reply-to email address
        /// </summary>
        [JsonPropertyName("reply_to")]
        public string? ReplyTo { get; set; }

        /// <summary>
        /// Template ID
        /// </summary>
        [JsonPropertyName("template_id")]
        public int? TemplateId { get; set; }

        /// <summary>
        /// Campaign schedule
        /// </summary>
        [JsonPropertyName("schedule")]
        public CampaignSchedule? Schedule { get; set; }

        /// <summary>
        /// Campaign settings
        /// </summary>
        [JsonPropertyName("settings")]
        public CampaignSettings? Settings { get; set; }

        /// <summary>
        /// Custom fields for the campaign
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }

    /// <summary>
    /// Request model for updating an existing email campaign
    /// </summary>
    public class UpdateEmailCampaignRequest
    {
        /// <summary>
        /// Campaign name
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Campaign subject line
        /// </summary>
        [JsonPropertyName("subject")]
        public string? Subject { get; set; }

        /// <summary>
        /// Campaign description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Campaign status
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// HTML content of the email
        /// </summary>
        [JsonPropertyName("html_content")]
        public string? HtmlContent { get; set; }

        /// <summary>
        /// Plain text content of the email
        /// </summary>
        [JsonPropertyName("text_content")]
        public string? TextContent { get; set; }

        /// <summary>
        /// From email address
        /// </summary>
        [JsonPropertyName("from_email")]
        public string? FromEmail { get; set; }

        /// <summary>
        /// From name
        /// </summary>
        [JsonPropertyName("from_name")]
        public string? FromName { get; set; }

        /// <summary>
        /// Reply-to email address
        /// </summary>
        [JsonPropertyName("reply_to")]
        public string? ReplyTo { get; set; }

        /// <summary>
        /// Template ID
        /// </summary>
        [JsonPropertyName("template_id")]
        public int? TemplateId { get; set; }

        /// <summary>
        /// Campaign schedule
        /// </summary>
        [JsonPropertyName("schedule")]
        public CampaignSchedule? Schedule { get; set; }

        /// <summary>
        /// Campaign settings
        /// </summary>
        [JsonPropertyName("settings")]
        public CampaignSettings? Settings { get; set; }

        /// <summary>
        /// Custom fields for the campaign
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }
}