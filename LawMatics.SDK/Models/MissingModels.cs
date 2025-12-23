using System.Text.Json.Serialization;

namespace LawMatics.SDK.Models
{
    // Addresses Models
    public class CreateAddressRequest
    {
        [JsonPropertyName("street1")]
        public string? Street1 { get; set; }

        [JsonPropertyName("street2")]
        public string? Street2 { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("postal_code")]
        public string? PostalCode { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("county")]
        public string? County { get; set; }

        [JsonPropertyName("address_type")]
        public string? AddressType { get; set; }
    }

    public class UpdateAddressRequest
    {
        [JsonPropertyName("street1")]
        public string? Street1 { get; set; }

        [JsonPropertyName("street2")]
        public string? Street2 { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("postal_code")]
        public string? PostalCode { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("county")]
        public string? County { get; set; }

        [JsonPropertyName("address_type")]
        public string? AddressType { get; set; }
    }

    // Tasks Models
    public class CreateTaskRequest
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("status_id")]
        public int StatusId { get; set; }

        [JsonPropertyName("assigned_to_id")]
        public int? AssignedToId { get; set; }

        [JsonPropertyName("matter_id")]
        public int? MatterId { get; set; }

        [JsonPropertyName("contact_id")]
        public int? ContactId { get; set; }

        [JsonPropertyName("due_date")]
        public DateTime? DueDate { get; set; }

        [JsonPropertyName("priority")]
        public string? Priority { get; set; }
    }

    public class UpdateTaskRequest
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("status_id")]
        public int? StatusId { get; set; }

        [JsonPropertyName("assigned_to_id")]
        public int? AssignedToId { get; set; }

        [JsonPropertyName("matter_id")]
        public int? MatterId { get; set; }

        [JsonPropertyName("contact_id")]
        public int? ContactId { get; set; }

        [JsonPropertyName("due_date")]
        public DateTime? DueDate { get; set; }

        [JsonPropertyName("priority")]
        public string? Priority { get; set; }
    }

    // Task Status Models
    public class CreateTaskStatusRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("color")]
        public string? Color { get; set; }

        [JsonPropertyName("is_completed")]
        public bool IsCompleted { get; set; }

        [JsonPropertyName("sort_order")]
        public int SortOrder { get; set; }
    }

    public class UpdateTaskStatusRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("color")]
        public string? Color { get; set; }

        [JsonPropertyName("is_completed")]
        public bool? IsCompleted { get; set; }

        [JsonPropertyName("sort_order")]
        public int? SortOrder { get; set; }
    }

    // Subtasks Models
    public class CreateSubtaskRequest
    {
        [JsonPropertyName("task_id")]
        public int TaskId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("is_completed")]
        public bool IsCompleted { get; set; }
    }

    public class UpdateSubtaskRequest
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("is_completed")]
        public bool? IsCompleted { get; set; }
    }

    // Expenses Models
    public class CreateExpenseRequest
    {
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("matter_id")]
        public int? MatterId { get; set; }
    }

    public class UpdateExpenseRequest
    {
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("amount")]
        public decimal? Amount { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }

        [JsonPropertyName("matter_id")]
        public int? MatterId { get; set; }
    }

    // Time Entries Models
    public class CreateTimeEntryRequest
    {
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("hours")]
        public decimal Hours { get; set; }

        [JsonPropertyName("rate")]
        public decimal Rate { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("matter_id")]
        public int? MatterId { get; set; }

        [JsonPropertyName("user_id")]
        public int? UserId { get; set; }
    }

    public class UpdateTimeEntryRequest
    {
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("hours")]
        public decimal? Hours { get; set; }

        [JsonPropertyName("rate")]
        public decimal? Rate { get; set; }

        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }

        [JsonPropertyName("matter_id")]
        public int? MatterId { get; set; }

        [JsonPropertyName("user_id")]
        public int? UserId { get; set; }
    }

    // Custom Emails Models
    public class CreateCustomEmailRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("subject")]
        public string Subject { get; set; } = string.Empty;

        [JsonPropertyName("body")]
        public string Body { get; set; } = string.Empty;

        [JsonPropertyName("body_text")]
        public string? BodyText { get; set; }

        [JsonPropertyName("from_email")]
        public string? FromEmail { get; set; }

        [JsonPropertyName("from_name")]
        public string? FromName { get; set; }

        [JsonPropertyName("reply_to_email")]
        public string? ReplyToEmail { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; } = true;
    }

    public class UpdateCustomEmailRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("subject")]
        public string? Subject { get; set; }

        [JsonPropertyName("body")]
        public string? Body { get; set; }

        [JsonPropertyName("body_text")]
        public string? BodyText { get; set; }

        [JsonPropertyName("from_email")]
        public string? FromEmail { get; set; }

        [JsonPropertyName("from_name")]
        public string? FromName { get; set; }

        [JsonPropertyName("reply_to_email")]
        public string? ReplyToEmail { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("is_active")]
        public bool? IsActive { get; set; }
    }

    // Email Addresses Models
    public class CreateEmailAddressRequest
    {
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("is_primary")]
        public bool IsPrimary { get; set; }
    }

    public class UpdateEmailAddressRequest
    {
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("is_primary")]
        public bool? IsPrimary { get; set; }
    }

    // Event Locations Models
    public class CreateEventLocationRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("capacity")]
        public int? Capacity { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    public class UpdateEventLocationRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("capacity")]
        public int? Capacity { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    // Event Types Models
    public class CreateEventTypeRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("color")]
        public string? Color { get; set; }

        [JsonPropertyName("default_duration")]
        public int? DefaultDuration { get; set; }
    }

    public class UpdateEventTypeRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("color")]
        public string? Color { get; set; }

        [JsonPropertyName("default_duration")]
        public int? DefaultDuration { get; set; }
    }

    // Matter Sub Statuses Models
    public class CreateMatterSubStatusRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("parent_status_id")]
        public int? ParentStatusId { get; set; }
    }

    public class UpdateMatterSubStatusRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("parent_status_id")]
        public int? ParentStatusId { get; set; }
    }

    // Custom Contact Types Models
    public class CreateCustomContactTypeRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    public class UpdateCustomContactTypeRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    // Company Finder Models
    public class CompanyFinderRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("industry")]
        public string? Industry { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonPropertyName("size")]
        public string? Size { get; set; }

        [JsonPropertyName("revenue")]
        public string? Revenue { get; set; }

        [JsonPropertyName("website")]
        public string? Website { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("email_domain")]
        public string? EmailDomain { get; set; }

        [JsonPropertyName("limit")]
        public int? Limit { get; set; }

        [JsonPropertyName("offset")]
        public int? Offset { get; set; }
    }

    public class CompanyFinderResult
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("industry")]
        public string? Industry { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonPropertyName("size")]
        public string? Size { get; set; }

        [JsonPropertyName("revenue")]
        public string? Revenue { get; set; }

        [JsonPropertyName("website")]
        public string? Website { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("email_domain")]
        public string? EmailDomain { get; set; }

        [JsonPropertyName("confidence_score")]
        public decimal ConfidenceScore { get; set; }

        [JsonPropertyName("data_sources")]
        public List<string>? DataSources { get; set; }
    }

    /// <summary>
    /// Represents a comment on various entities
    /// </summary>
    public class Comment
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("entity_type")]
        public string EntityType { get; set; } = string.Empty;

        [JsonPropertyName("entity_id")]
        public int EntityId { get; set; }

        [JsonPropertyName("comment")]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("created_by_id")]
        public int? CreatedById { get; set; }

        [JsonPropertyName("created_by_name")]
        public string? CreatedByName { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("is_internal")]
        public bool IsInternal { get; set; }

        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; set; }
    }

    /// <summary>
    /// Request model for creating a comment
    /// </summary>
    public class CreateCommentRequest
    {
        [JsonPropertyName("entity_type")]
        public string EntityType { get; set; } = string.Empty;

        [JsonPropertyName("entity_id")]
        public int EntityId { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; } = string.Empty;

        [JsonPropertyName("is_internal")]
        public bool IsInternal { get; set; }
    }

    /// <summary>
    /// Request model for updating a comment
    /// </summary>
    public class UpdateCommentRequest
    {
        [JsonPropertyName("comment")]
        public string Comment { get; set; } = string.Empty;

        [JsonPropertyName("is_internal")]
        public bool? IsInternal { get; set; }
    }

    /// <summary>
    /// Represents the request parameters for filtering comments.
    /// </summary>
    public class GetCommentsParameters : FilterParameters
    {
        /// <summary>
        /// Gets or sets a filter for entity type.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? EntityType { get; set; }

        /// <summary>
        /// Gets or sets a filter for entity ID.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? EntityId { get; set; }

        /// <summary>
        /// Gets or sets a filter for created by user ID.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? CreatedById { get; set; }

        /// <summary>
        /// Gets or sets a filter for start date.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets a filter for end date.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets a filter for internal comments (true/false).
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? IsInternal { get; set; }

        /// <summary>
        /// Gets or sets a filter for search text.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Search { get; set; }
    }
}