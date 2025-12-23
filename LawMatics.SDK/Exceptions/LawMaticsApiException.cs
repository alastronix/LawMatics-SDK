using System;

namespace LawMatics.SDK.Exceptions
{
    /// <summary>
    /// Represents an exception thrown when the LawMatics API returns an error response.
    /// </summary>
    public class LawMaticsApiException : LawMaticsException
    {
        /// <summary>
        /// Gets the API error response details, if available.
        /// </summary>
        public Models.ApiErrorResponse? ErrorResponse { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsApiException"/> class.
        /// </summary>
        public LawMaticsApiException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsApiException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public LawMaticsApiException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsApiException"/> class with a specified error message and a reference to the inner exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public LawMaticsApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsApiException"/> class with detailed error information.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="statusCode">The HTTP status code returned by the API.</param>
        /// <param name="errorCode">The error code returned by the API.</param>
        /// <param name="correlationId">The correlation ID for tracking the request.</param>
        /// <param name="errorResponse">The detailed error response from the API.</param>
        public LawMaticsApiException(string message, int statusCode, string? errorCode = null, string? correlationId = null, Models.ApiErrorResponse? errorResponse = null) 
            : base(message, statusCode, errorCode, correlationId)
        {
            ErrorResponse = errorResponse;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsApiException"/> class with detailed error information and inner exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="statusCode">The HTTP status code returned by the API.</param>
        /// <param name="errorCode">The error code returned by the API.</param>
        /// <param name="correlationId">The correlation ID for tracking the request.</param>
        /// <param name="errorResponse">The detailed error response from the API.</param>
        public LawMaticsApiException(string message, Exception innerException, int statusCode, string? errorCode = null, string? correlationId = null, Models.ApiErrorResponse? errorResponse = null) 
            : base(message, innerException, statusCode, errorCode, correlationId)
        {
            ErrorResponse = errorResponse;
        }
    }

    /// <summary>
    /// Represents an exception thrown when authentication fails.
    /// </summary>
    public class LawMaticsAuthenticationException : LawMaticsException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsAuthenticationException"/> class.
        /// </summary>
        public LawMaticsAuthenticationException() : base("Authentication failed. Please check your credentials.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsAuthenticationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public LawMaticsAuthenticationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsAuthenticationException"/> class with a specified error message and a reference to the inner exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public LawMaticsAuthenticationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsAuthenticationException"/> class with detailed error information.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="statusCode">The HTTP status code returned by the API.</param>
        /// <param name="errorCode">The error code returned by the API.</param>
        /// <param name="correlationId">The correlation ID for tracking the request.</param>
        public LawMaticsAuthenticationException(string message, int statusCode, string? errorCode = null, string? correlationId = null) 
            : base(message, statusCode, errorCode, correlationId)
        {
        }
    }

    /// <summary>
    /// Represents an exception thrown when rate limiting is exceeded.
    /// </summary>
    public class LawMaticsRateLimitException : LawMaticsException
    {
        /// <summary>
        /// Gets the number of seconds to wait before making another request.
        /// </summary>
        public int? RetryAfterSeconds { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsRateLimitException"/> class.
        /// </summary>
        public LawMaticsRateLimitException() : base("Rate limit exceeded. Please try again later.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsRateLimitException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public LawMaticsRateLimitException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsRateLimitException"/> class with retry information.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="retryAfterSeconds">The number of seconds to wait before retrying.</param>
        public LawMaticsRateLimitException(string message, int retryAfterSeconds) : base(message)
        {
            RetryAfterSeconds = retryAfterSeconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsRateLimitException"/> class with detailed error information.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="statusCode">The HTTP status code returned by the API.</param>
        /// <param name="errorCode">The error code returned by the API.</param>
        /// <param name="correlationId">The correlation ID for tracking the request.</param>
        /// <param name="retryAfterSeconds">The number of seconds to wait before retrying.</param>
        public LawMaticsRateLimitException(string message, int statusCode, string? errorCode = null, string? correlationId = null, int? retryAfterSeconds = null) 
            : base(message, statusCode, errorCode, correlationId)
        {
            RetryAfterSeconds = retryAfterSeconds;
        }
    }

    /// <summary>
    /// Represents an exception thrown when a requested resource is not found.
    /// </summary>
    public class LawMaticsNotFoundException : LawMaticsException
    {
        /// <summary>
        /// Gets the type of resource that was not found.
        /// </summary>
        public string? ResourceType { get; }

        /// <summary>
        /// Gets the identifier of the resource that was not found.
        /// </summary>
        public string? ResourceId { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsNotFoundException"/> class.
        /// </summary>
        public LawMaticsNotFoundException() : base("The requested resource was not found.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsNotFoundException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public LawMaticsNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsNotFoundException"/> class with resource information.
        /// </summary>
        /// <param name="resourceType">The type of resource that was not found.</param>
        /// <param name="resourceId">The identifier of the resource that was not found.</param>
        public LawMaticsNotFoundException(string resourceType, string resourceId) 
            : base($"The {resourceType} with ID '{resourceId}' was not found.")
        {
            ResourceType = resourceType;
            ResourceId = resourceId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsNotFoundException"/> class with detailed error information.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="statusCode">The HTTP status code returned by the API.</param>
        /// <param name="errorCode">The error code returned by the API.</param>
        /// <param name="correlationId">The correlation ID for tracking the request.</param>
        /// <param name="resourceType">The type of resource that was not found.</param>
        /// <param name="resourceId">The identifier of the resource that was not found.</param>
        public LawMaticsNotFoundException(string message, int statusCode, string? errorCode = null, string? correlationId = null, string? resourceType = null, string? resourceId = null) 
            : base(message, statusCode, errorCode, correlationId)
        {
            ResourceType = resourceType;
            ResourceId = resourceId;
        }
    }
}