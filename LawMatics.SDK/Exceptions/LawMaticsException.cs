using System;

namespace LawMatics.SDK.Exceptions
{
    /// <summary>
    /// Represents a base exception for all LawMatics API errors.
    /// </summary>
    public class LawMaticsException : Exception
    {
        /// <summary>
        /// Gets the HTTP status code returned by the API.
        /// </summary>
        public int? StatusCode { get; }

        /// <summary>
        /// Gets the error code returned by the API, if available.
        /// </summary>
        public string? ErrorCode { get; }

        /// <summary>
        /// Gets the correlation ID for tracking the request, if available.
        /// </summary>
        public string? CorrelationId { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsException"/> class.
        /// </summary>
        public LawMaticsException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public LawMaticsException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public LawMaticsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsException"/> class with detailed error information.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="statusCode">The HTTP status code returned by the API.</param>
        /// <param name="errorCode">The error code returned by the API.</param>
        /// <param name="correlationId">The correlation ID for tracking the request.</param>
        public LawMaticsException(string message, int statusCode, string? errorCode = null, string? correlationId = null) 
            : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            CorrelationId = correlationId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsException"/> class with detailed error information and inner exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="statusCode">The HTTP status code returned by the API.</param>
        /// <param name="errorCode">The error code returned by the API.</param>
        /// <param name="correlationId">The correlation ID for tracking the request.</param>
        public LawMaticsException(string message, Exception innerException, int statusCode, string? errorCode = null, string? correlationId = null) 
            : base(message, innerException)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            CorrelationId = correlationId;
        }
    }
}