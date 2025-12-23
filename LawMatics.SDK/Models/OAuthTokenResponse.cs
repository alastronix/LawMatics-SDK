using System.Text.Json.Serialization;

namespace LawMatics.SDK.Models
{
    /// <summary>
    /// Represents the response from an OAuth token request.
    /// </summary>
    public class OAuthTokenResponse
    {
        /// <summary>
        /// Gets or sets the access token for API authentication.
        /// </summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the token type (typically "Bearer").
        /// </summary>
        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }

        /// <summary>
        /// Gets or sets the expires in time in seconds.
        /// </summary>
        [JsonPropertyName("expires_in")]
        public int? ExpiresIn { get; set; }

        /// <summary>
        /// Gets or sets the refresh token for obtaining new access tokens.
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the scope of the access token.
        /// </summary>
        [JsonPropertyName("scope")]
        public string? Scope { get; set; }

        /// <summary>
        /// Gets or sets the date when the token was issued.
        /// </summary>
        [JsonPropertyName("issued_at")]
        public long? IssuedAt { get; set; }

        /// <summary>
        /// Gets or sets the expiration date of the token.
        /// </summary>
        [JsonPropertyName("expires_at")]
        public DateTime? ExpiresAt { get; set; }
    }
}