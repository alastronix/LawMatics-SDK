using System;

namespace LawMatics.SDK.Authentication
{
    /// <summary>
    /// Represents the authentication credentials for accessing the LawMatics API.
    /// </summary>
    public class LawMaticsCredentials
    {
        /// <summary>
        /// Gets or sets the OAuth access token for API authentication.
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the OAuth refresh token for obtaining new access tokens.
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the client ID for OAuth authentication.
        /// </summary>
        public string? ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret for OAuth authentication.
        /// </summary>
        public string? ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the expiration time of the access token.
        /// </summary>
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// Gets or sets the token type (typically "Bearer").
        /// </summary>
        public string TokenType { get; set; } = "Bearer";

        /// <summary>
        /// Gets a value indicating whether the access token is expired or will expire within the next 5 minutes.
        /// </summary>
        public bool IsExpired => ExpiresAt.HasValue && ExpiresAt.Value <= DateTime.UtcNow.AddMinutes(5);

        /// <summary>
        /// Gets a value indicating whether the credentials contain a refresh token.
        /// </summary>
        public bool HasRefreshToken => !string.IsNullOrEmpty(RefreshToken);

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsCredentials"/> class.
        /// </summary>
        public LawMaticsCredentials()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsCredentials"/> class with an access token.
        /// </summary>
        /// <param name="accessToken">The OAuth access token.</param>
        public LawMaticsCredentials(string accessToken)
        {
            AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LawMaticsCredentials"/> class with OAuth credentials.
        /// </summary>
        /// <param name="clientId">The OAuth client ID.</param>
        /// <param name="clientSecret">The OAuth client secret.</param>
        /// <param name="accessToken">The OAuth access token.</param>
        /// <param name="refreshToken">The OAuth refresh token (optional).</param>
        /// <param name="expiresAt">The expiration time of the access token (optional).</param>
        public LawMaticsCredentials(string clientId, string clientSecret, string accessToken, string? refreshToken = null, DateTime? expiresAt = null)
        {
            ClientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
            ClientSecret = clientSecret ?? throw new ArgumentNullException(nameof(clientSecret));
            AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            RefreshToken = refreshToken;
            ExpiresAt = expiresAt;
        }

        /// <summary>
        /// Creates a new instance of <see cref="LawMaticsCredentials"/> from an OAuth token response.
        /// </summary>
        /// <param name="tokenResponse">The OAuth token response.</param>
        /// <param name="clientId">The OAuth client ID.</param>
        /// <param name="clientSecret">The OAuth client secret.</param>
        /// <returns>A new instance of <see cref="LawMaticsCredentials"/>.</returns>
        public static LawMaticsCredentials FromTokenResponse(Models.OAuthTokenResponse tokenResponse, string clientId, string clientSecret)
        {
            if (tokenResponse == null)
                throw new ArgumentNullException(nameof(tokenResponse));

            if (tokenResponse.ExpiresIn.HasValue)
            {
                return new LawMaticsCredentials(
                    clientId,
                    clientSecret,
                    tokenResponse.AccessToken,
                    tokenResponse.RefreshToken,
                    DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn.Value))
                {
                    TokenType = tokenResponse.TokenType ?? "Bearer"
                };
            }

            return new LawMaticsCredentials(
                clientId,
                clientSecret,
                tokenResponse.AccessToken,
                tokenResponse.RefreshToken)
            {
                TokenType = tokenResponse.TokenType ?? "Bearer"
            };
        }

        /// <summary>
        /// Updates the credentials with a new access token.
        /// </summary>
        /// <param name="accessToken">The new access token.</param>
        /// <param name="refreshToken">The new refresh token (optional).</param>
        /// <param name="expiresIn">The expiration time in seconds (optional).</param>
        public void UpdateToken(string accessToken, string? refreshToken = null, int? expiresIn = null)
        {
            AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            
            if (!string.IsNullOrEmpty(refreshToken))
                RefreshToken = refreshToken;

            if (expiresIn.HasValue)
                ExpiresAt = DateTime.UtcNow.AddSeconds(expiresIn.Value);
        }

        /// <summary>
        /// Updates the credentials from an OAuth token response.
        /// </summary>
        /// <param name="tokenResponse">The OAuth token response.</param>
        public void UpdateTokens(Models.OAuthTokenResponse tokenResponse)
        {
            if (tokenResponse == null)
                throw new ArgumentNullException(nameof(tokenResponse));

            AccessToken = tokenResponse.AccessToken;
            
            if (!string.IsNullOrEmpty(tokenResponse.RefreshToken))
                RefreshToken = tokenResponse.RefreshToken;

            if (tokenResponse.ExpiresAt.HasValue)
                ExpiresAt = tokenResponse.ExpiresAt.Value;
            else if (tokenResponse.ExpiresIn.HasValue)
                ExpiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn.Value);

            if (!string.IsNullOrEmpty(tokenResponse.TokenType))
                TokenType = tokenResponse.TokenType;
        }
    }
}