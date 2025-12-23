using System.Text.Json.Serialization;

namespace LawMatics.SDK.Models
{
    /// <summary>
    /// Represents the standard error response structure returned by the LawMatics API.
    /// </summary>
    public class ApiErrorResponse
    {
        /// <summary>
        /// Gets or sets the error code returned by the API.
        /// </summary>
        [JsonPropertyName("error")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Error { get; set; }

        /// <summary>
        /// Gets or sets the error message describing what went wrong.
        /// </summary>
        [JsonPropertyName("error_description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ErrorDescription { get; set; }

        /// <summary>
        /// Gets or sets the correlation ID for tracking the request.
        /// </summary>
        [JsonPropertyName("correlation_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CorrelationId { get; set; }

        /// <summary>
        /// Gets or sets additional error details, if available.
        /// </summary>
        [JsonPropertyName("details")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, object>? Details { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the error occurred.
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the path of the request that caused the error.
        /// </summary>
        [JsonPropertyName("path")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Path { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        [JsonPropertyName("message")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        [JsonPropertyName("error_code")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ErrorCode { get; set; }
    }

    /// <summary>
    /// Represents the base response structure for all LawMatics API responses.
    /// </summary>
    /// <typeparam name="T">The type of data contained in the response.</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Gets or sets the response data.
        /// </summary>
        [JsonPropertyName("data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        /// <summary>
        /// Gets or sets pagination information, if applicable.
        /// </summary>
        [JsonPropertyName("pagination")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PaginationInfo? Pagination { get; set; }

        /// <summary>
        /// Gets or sets the correlation ID for tracking the request.
        /// </summary>
        [JsonPropertyName("correlation_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CorrelationId { get; set; }

        /// <summary>
        /// Gets or sets metadata about the response.
        /// </summary>
        [JsonPropertyName("meta")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, object>? Meta { get; set; }
    }

    /// <summary>
    /// Represents pagination information for list responses.
    /// </summary>
    public class PaginationInfo
    {
        /// <summary>
        /// Gets or sets the current page number (1-based).
        /// </summary>
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }

        /// <summary>
        /// Gets or sets the total number of items available.
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the total number of pages available.
        /// </summary>
        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether there is a next page.
        /// </summary>
        [JsonPropertyName("has_next")]
        public bool HasNext { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether there is a previous page.
        /// </summary>
        [JsonPropertyName("has_prev")]
        public bool HasPrev { get; set; }

        /// <summary>
        /// Gets or sets the URL for the next page, if available.
        /// </summary>
        [JsonPropertyName("next_page_url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? NextPageUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL for the previous page, if available.
        /// </summary>
        [JsonPropertyName("prev_page_url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PrevPageUrl { get; set; }
    }

    /// <summary>
    /// Represents the base request parameters that can be applied to most API endpoints.
    /// </summary>
    public class BaseRequestParameters
    {
        /// <summary>
        /// Gets or sets the page number for pagination (1-based).
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Page { get; set; }

        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? PerPage { get; set; }

        /// <summary>
        /// Gets or sets the field to sort by.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? SortBy { get; set; }

        /// <summary>
        /// Gets or sets the sort direction (asc or desc).
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? SortDirection { get; set; }

        /// <summary>
        /// Gets or sets the fields to include in the response.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string[]? Include { get; set; }

        /// <summary>
        /// Gets or sets the fields to exclude from the response.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string[]? Exclude { get; set; }
    }

    /// <summary>
    /// Represents filter parameters for API queries.
    /// </summary>
    public class FilterParameters : BaseRequestParameters
    {
        /// <summary>
        /// Gets or sets the search term to filter results.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Search { get; set; }

        /// <summary>
        /// Gets or sets the start date for filtering results.
        /// </summary>
        [JsonPropertyName("start_date")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date for filtering results.
        /// </summary>
        [JsonPropertyName("end_date")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets additional filter criteria.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, object>? Filters { get; set; }
    }

    /// <summary>
    /// Represents a paginated response from the LawMatics API.
    /// </summary>
    /// <typeparam name="T">The type of items in the data collection.</typeparam>
    public class PagedResponse<T>
    {
        /// <summary>
        /// Gets or sets the collection of items returned by the API.
        /// </summary>
        [JsonPropertyName("data")]
        public List<T>? Data { get; set; }

        /// <summary>
        /// Gets or sets pagination information.
        /// </summary>
        [JsonPropertyName("pagination")]
        public PaginationInfo? Pagination { get; set; }

        /// <summary>
        /// Gets or sets the correlation ID for tracking the request.
        /// </summary>
        [JsonPropertyName("correlation_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CorrelationId { get; set; }

        /// <summary>
        /// Gets or sets metadata about the response.
        /// </summary>
        [JsonPropertyName("meta")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, object>? Meta { get; set; }
    }
}