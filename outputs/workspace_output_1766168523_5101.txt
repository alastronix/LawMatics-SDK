using System.Text.Json.Serialization;

namespace LawMatics.SDK.Models
{
    /// <summary>
    /// Represents a file in Lawmatics
    /// </summary>
    public class LawMaticsFile
    {
        /// <summary>
        /// Unique identifier for the file
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// File name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// File description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// File type
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// File size in bytes
        /// </summary>
        [JsonPropertyName("size")]
        public long Size { get; set; }

        /// <summary>
        /// File URL
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        /// <summary>
        /// Download URL
        /// </summary>
        [JsonPropertyName("download_url")]
        public string? DownloadUrl { get; set; }

        /// <summary>
        /// Date when file was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when file was last updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Request model for updating a file
    /// </summary>
    public class UpdateFileRequest
    {
        /// <summary>
        /// File name
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// File description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// File type
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }

    /// <summary>
    /// Represents a custom contact type
    /// </summary>
    public class CustomContactType
    {
        /// <summary>
        /// Unique identifier for the custom contact type
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Custom contact type name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Custom contact type description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Date when custom contact type was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when custom contact type was last updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Represents an email campaign creation request
    /// </summary>
    public class CreateCampaignRequest
    {
        /// <summary>
        /// Campaign name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Campaign subject
        /// </summary>
        [JsonPropertyName("subject")]
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Campaign content
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Campaign type
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Campaign status
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }

    /// <summary>
    /// Represents email campaign statistics
    /// </summary>
    public class EmailCampaignStats
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
        /// Open rate percentage
        /// </summary>
        [JsonPropertyName("open_rate")]
        public decimal OpenRate { get; set; }

        /// <summary>
        /// Click rate percentage
        /// </summary>
        [JsonPropertyName("click_rate")]
        public decimal ClickRate { get; set; }
    }

    /// <summary>
    /// Represents an event location
    /// </summary>
    public class EventLocation
    {
        /// <summary>
        /// Unique identifier for the event location
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Location name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Location address
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        /// <summary>
        /// Location capacity
        /// </summary>
        [JsonPropertyName("capacity")]
        public int? Capacity { get; set; }

        /// <summary>
        /// Location description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Date when location was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when location was last updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Represents an event type
    /// </summary>
    public class EventType
    {
        /// <summary>
        /// Unique identifier for the event type
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Event type name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Event type description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Event type color
        /// </summary>
        [JsonPropertyName("color")]
        public string? Color { get; set; }

        /// <summary>
        /// Default duration in minutes
        /// </summary>
        [JsonPropertyName("default_duration")]
        public int? DefaultDuration { get; set; }

        /// <summary>
        /// Date when event type was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when event type was last updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Represents a matter sub-status
    /// </summary>
    public class MatterSubStatus
    {
        /// <summary>
        /// Unique identifier for the matter sub-status
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Sub-status name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Sub-status description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Parent status ID
        /// </summary>
        [JsonPropertyName("parent_status_id")]
        public int? ParentStatusId { get; set; }

        /// <summary>
        /// Date when sub-status was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when sub-status was last updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Represents a payment creation request
    /// </summary>
    public class CreatePaymentRequest
    {
        /// <summary>
        /// Payment amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Payment description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Payment method
        /// </summary>
        [JsonPropertyName("method")]
        public string? Method { get; set; }

        /// <summary>
        /// Contact ID
        /// </summary>
        [JsonPropertyName("contact_id")]
        public int? ContactId { get; set; }

        /// <summary>
        /// Matter ID
        /// </summary>
        [JsonPropertyName("matter_id")]
        public int? MatterId { get; set; }
    }

    /// <summary>
    /// Represents a payment update request
    /// </summary>
    public class UpdatePaymentRequest
    {
        /// <summary>
        /// Payment amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Payment description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Payment method
        /// </summary>
        [JsonPropertyName("method")]
        public string? Method { get; set; }

        /// <summary>
        /// Payment status
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }

    /// <summary>
    /// Represents an expense
    /// </summary>
    public class Expense
    {
        /// <summary>
        /// Unique identifier for the expense
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Expense description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Expense amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Expense category
        /// </summary>
        [JsonPropertyName("category")]
        public string? Category { get; set; }

        /// <summary>
        /// Expense date
        /// </summary>
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Matter ID
        /// </summary>
        [JsonPropertyName("matter_id")]
        public int? MatterId { get; set; }

        /// <summary>
        /// Date when expense was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when expense was last updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Represents a time entry
    /// </summary>
    public class TimeEntry
    {
        /// <summary>
        /// Unique identifier for the time entry
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Time entry description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Hours worked
        /// </summary>
        [JsonPropertyName("hours")]
        public decimal Hours { get; set; }

        /// <summary>
        /// Hourly rate
        /// </summary>
        [JsonPropertyName("rate")]
        public decimal Rate { get; set; }

        /// <summary>
        /// Total amount
        /// </summary>
        [JsonPropertyName("total")]
        public decimal Total { get; set; }

        /// <summary>
        /// Time entry date
        /// </summary>
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Matter ID
        /// </summary>
        [JsonPropertyName("matter_id")]
        public int? MatterId { get; set; }

        /// <summary>
        /// User ID
        /// </summary>
        [JsonPropertyName("user_id")]
        public int? UserId { get; set; }

        /// <summary>
        /// Date when time entry was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when time entry was last updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Represents a custom email template.
    /// </summary>
    public class CustomEmail
    {
        /// <summary>
        /// Gets or sets the custom email ID.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the email name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email subject.
        /// </summary>
        [JsonPropertyName("subject")]
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email body content (HTML).
        /// </summary>
        [JsonPropertyName("body")]
        public string Body { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email body content (plain text).
        /// </summary>
        [JsonPropertyName("body_text")]
        public string? BodyText { get; set; }

        /// <summary>
        /// Gets or sets the from email address.
        /// </summary>
        [JsonPropertyName("from_email")]
        public string? FromEmail { get; set; }

        /// <summary>
        /// Gets or sets the from display name.
        /// </summary>
        [JsonPropertyName("from_name")]
        public string? FromName { get; set; }

        /// <summary>
        /// Gets or sets the reply-to email address.
        /// </summary>
        [JsonPropertyName("reply_to_email")]
        public string? ReplyToEmail { get; set; }

        /// <summary>
        /// Gets or sets the email category.
        /// </summary>
        [JsonPropertyName("category")]
        public string? Category { get; set; }

        /// <summary>
        /// Gets or sets whether the email is active.
        /// </summary>
        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last update date.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Represents a custom email preview.
    /// </summary>
    public class CustomEmailPreview
    {
        /// <summary>
        /// Gets or sets the preview subject.
        /// </summary>
        [JsonPropertyName("subject")]
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the preview body (HTML).
        /// </summary>
        [JsonPropertyName("body")]
        public string Body { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the preview body (plain text).
        /// </summary>
        [JsonPropertyName("body_text")]
        public string? BodyText { get; set; }
    }

    /// <summary>
    /// Represents a task.
    /// </summary>
    public class TaskItem
    {
        /// <summary>
        /// Gets or sets the task ID.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the task title.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the task description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the task status ID.
        /// </summary>
        [JsonPropertyName("status_id")]
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets the task status name.
        /// </summary>
        [JsonPropertyName("status_name")]
        public string StatusName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the assigned user ID.
        /// </summary>
        [JsonPropertyName("assigned_to_id")]
        public int? AssignedToId { get; set; }

        /// <summary>
        /// Gets or sets the assigned user name.
        /// </summary>
        [JsonPropertyName("assigned_to_name")]
        public string? AssignedToName { get; set; }

        /// <summary>
        /// Gets or sets the user who assigned the task ID.
        /// </summary>
        [JsonPropertyName("assigned_by_id")]
        public int? AssignedById { get; set; }

        /// <summary>
        /// Gets or sets the user who assigned the task name.
        /// </summary>
        [JsonPropertyName("assigned_by_name")]
        public string? AssignedByName { get; set; }

        /// <summary>
        /// Gets or sets the related matter ID.
        /// </summary>
        [JsonPropertyName("matter_id")]
        public int? MatterId { get; set; }

        /// <summary>
        /// Gets or sets the related matter name.
        /// </summary>
        [JsonPropertyName("matter_name")]
        public string? MatterName { get; set; }

        /// <summary>
        /// Gets or sets the related contact ID.
        /// </summary>
        [JsonPropertyName("contact_id")]
        public int? ContactId { get; set; }

        /// <summary>
        /// Gets or sets the related contact name.
        /// </summary>
        [JsonPropertyName("contact_name")]
        public string? ContactName { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        [JsonPropertyName("due_date")]
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Gets or sets the completion date.
        /// </summary>
        [JsonPropertyName("completed_at")]
        public DateTime? CompletedAt { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        [JsonPropertyName("priority")]
        public string Priority { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last update date.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Represents a task status.
    /// </summary>
    public class TaskStatus
    {
        /// <summary>
        /// Gets or sets the task status ID.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the task status name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the task status description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the color for the status.
        /// </summary>
        [JsonPropertyName("color")]
        public string? Color { get; set; }

        /// <summary>
        /// Gets or sets whether this is a completed status.
        /// </summary>
        [JsonPropertyName("is_completed")]
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        [JsonPropertyName("sort_order")]
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets whether the status is active.
        /// </summary>
        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last update date.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Represents an email address.
    /// </summary>
    public class EmailAddress
    {
        /// <summary>
        /// Gets or sets the email address ID.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email type.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether this is the primary email.
        /// </summary>
        [JsonPropertyName("is_primary")]
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last update date.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Represents a subtask.
    /// </summary>
    public class Subtask
    {
        /// <summary>
        /// Gets or sets the subtask ID.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the parent task ID.
        /// </summary>
        [JsonPropertyName("task_id")]
        public int TaskId { get; set; }

        /// <summary>
        /// Gets or sets the subtask title.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the subtask description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets whether the subtask is completed.
        /// </summary>
        [JsonPropertyName("is_completed")]
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Gets or sets the completion date.
        /// </summary>
        [JsonPropertyName("completed_at")]
        public DateTime? CompletedAt { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last update date.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    }