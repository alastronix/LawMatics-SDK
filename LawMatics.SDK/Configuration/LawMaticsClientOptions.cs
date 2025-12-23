using System;

namespace LawMatics.SDK.Configuration
{
    /// <summary>
    /// Represents configuration options for the LawMatics client.
    /// </summary>
    public class LawMaticsClientOptions
    {
        /// <summary>
        /// Gets or sets the base URL for the LawMatics API.
        /// </summary>
        public string BaseUrl { get; set; } = "https://api.lawmatics.com";

        /// <summary>
        /// Gets or sets the API version to use.
        /// </summary>
        public string ApiVersion { get; set; } = "v1";

        /// <summary>
        /// Gets or sets the timeout for HTTP requests in seconds.
        /// </summary>
        public int TimeoutSeconds { get; set; } = 30;

        /// <summary>
        /// Gets or sets the maximum number of retry attempts for failed requests.
        /// </summary>
        public int MaxRetryAttempts { get; set; } = 3;

        /// <summary>
        /// Gets or sets the delay between retry attempts in milliseconds.
        /// </summary>
        public int RetryDelayMilliseconds { get; set; } = 1000;

        /// <summary>
        /// Gets or sets a value indicating whether retry logic should be enabled for transient failures.
        /// </summary>
        public bool EnableRetry { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether detailed error information should be included in exceptions.
        /// </summary>
        public bool IncludeErrorDetails { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether request/response logging should be enabled.
        /// </summary>
        public bool EnableLogging { get; set; } = false;

        /// <summary>
        /// Gets or sets custom headers to be included with every request.
        /// </summary>
        public Dictionary<string, string> CustomHeaders { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets or sets the user agent string to be used for requests.
        /// </summary>
        public string UserAgent { get; set; } = "LawMatics-SDK/1.0.0";

        /// <summary>
        /// Gets or sets the OAuth authorization URL.
        /// </summary>
        public string AuthorizationUrl { get; set; } = "https://app.lawmatics.com/oauth/authorize";

        /// <summary>
        /// Gets or sets the OAuth token URL.
        /// </summary>
        public string TokenUrl { get; set; } = "https://api.lawmatics.com/oauth/token";

        /// <summary>
        /// Gets or sets the OAuth redirect URI for the authorization flow.
        /// </summary>
        public string? RedirectUri { get; set; }

        /// <summary>
        /// Gets or sets the OAuth scopes to request.
        /// </summary>
        public string[] Scopes { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Creates a default instance of <see cref="LawMaticsClientOptions"/>.
        /// </summary>
        /// <returns>A new instance with default values.</returns>
        public static LawMaticsClientOptions Default => new LawMaticsClientOptions();

        /// <summary>
        /// Creates an instance of <see cref="LawMaticsClientOptions"/> for development environment.
        /// </summary>
        /// <returns>A new instance configured for development.</returns>
        public static LawMaticsClientOptions Development => new LawMaticsClientOptions
        {
            EnableLogging = true,
            IncludeErrorDetails = true,
            TimeoutSeconds = 60
        };

        /// <summary>
        /// Creates an instance of <see cref="LawMaticsClientOptions"/> for production environment.
        /// </summary>
        /// <returns>A new instance configured for production.</returns>
        public static LawMaticsClientOptions Production => new LawMaticsClientOptions
        {
            EnableLogging = false,
            IncludeErrorDetails = false,
            TimeoutSeconds = 30,
            MaxRetryAttempts = 2,
            RetryDelayMilliseconds = 2000
        };

        /// <summary>
        /// Validates the options and throws an exception if invalid.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when any required option is invalid.</exception>
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(BaseUrl))
                throw new ArgumentException("Base URL is required.", nameof(BaseUrl));

            if (!Uri.TryCreate(BaseUrl, UriKind.Absolute, out _))
                throw new ArgumentException("Base URL must be a valid absolute URI.", nameof(BaseUrl));

            if (string.IsNullOrWhiteSpace(ApiVersion))
                throw new ArgumentException("API version is required.", nameof(ApiVersion));

            if (TimeoutSeconds <= 0)
                throw new ArgumentException("Timeout seconds must be greater than zero.", nameof(TimeoutSeconds));

            if (MaxRetryAttempts < 0)
                throw new ArgumentException("Max retry attempts cannot be negative.", nameof(MaxRetryAttempts));

            if (RetryDelayMilliseconds < 0)
                throw new ArgumentException("Retry delay milliseconds cannot be negative.", nameof(RetryDelayMilliseconds));

            if (!string.IsNullOrWhiteSpace(AuthorizationUrl) && !Uri.TryCreate(AuthorizationUrl, UriKind.Absolute, out _))
                throw new ArgumentException("Authorization URL must be a valid absolute URI.", nameof(AuthorizationUrl));

            if (!string.IsNullOrWhiteSpace(TokenUrl) && !Uri.TryCreate(TokenUrl, UriKind.Absolute, out _))
                throw new ArgumentException("Token URL must be a valid absolute URI.", nameof(TokenUrl));
        }

        /// <summary>
        /// Gets the full API URL for the specified endpoint.
        /// </summary>
        /// <param name="endpoint">The API endpoint.</param>
        /// <returns>The full API URL.</returns>
        public string GetApiUrl(string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
                throw new ArgumentException("Endpoint cannot be null or empty.", nameof(endpoint));

            var baseUrl = BaseUrl.TrimEnd('/');
            var apiPath = $"/{ApiVersion}";
            var endpointPath = endpoint.StartsWith('/') ? endpoint : $"/{endpoint}";

            return $"{baseUrl}{apiPath}{endpointPath}";
        }

        /// <summary>
        /// Adds a custom header to be included with every request.
        /// </summary>
        /// <param name="name">The header name.</param>
        /// <param name="value">The header value.</param>
        public void AddCustomHeader(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Header name cannot be null or empty.", nameof(name));

            CustomHeaders[name] = value ?? string.Empty;
        }

        /// <summary>
        /// Removes a custom header.
        /// </summary>
        /// <param name="name">The header name to remove.</param>
        /// <returns>True if the header was removed; false if it didn't exist.</returns>
        public bool RemoveCustomHeader(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            return CustomHeaders.Remove(name);
        }
    }
}