using System;
using LawMatics.SDK.Authentication;
using LawMatics.SDK.Models;
using Xunit;

namespace LawMatics.SDK.Tests.Authentication
{
    /// <summary>
    /// Unit tests for the <see cref="LawMaticsCredentials"/> class.
    /// </summary>
    public class LawMaticsCredentialsTests
    {
        [Fact]
        public void Constructor_WithAccessToken_SetsAccessToken()
        {
            // Arrange
            var accessToken = "test-access-token";

            // Act
            var credentials = new LawMaticsCredentials(accessToken);

            // Assert
            Assert.Equal(accessToken, credentials.AccessToken);
            Assert.Equal("Bearer", credentials.TokenType);
        }

        [Fact]
        public void Constructor_WithNullAccessToken_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new LawMaticsCredentials(null!));
        }

        [Fact]
        public void Constructor_WithFullCredentials_SetsAllProperties()
        {
            // Arrange
            var clientId = "test-client-id";
            var clientSecret = "test-client-secret";
            var accessToken = "test-access-token";
            var refreshToken = "test-refresh-token";
            var expiresAt = DateTime.UtcNow.AddHours(1);

            // Act
            var credentials = new LawMaticsCredentials(clientId, clientSecret, accessToken, refreshToken, expiresAt);

            // Assert
            Assert.Equal(clientId, credentials.ClientId);
            Assert.Equal(clientSecret, credentials.ClientSecret);
            Assert.Equal(accessToken, credentials.AccessToken);
            Assert.Equal(refreshToken, credentials.RefreshToken);
            Assert.Equal(expiresAt, credentials.ExpiresAt);
            Assert.Equal("Bearer", credentials.TokenType);
        }

        [Fact]
        public void Constructor_WithNullClientId_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new LawMaticsCredentials(null!, "secret", "token"));
        }

        [Fact]
        public void Constructor_WithNullClientSecret_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new LawMaticsCredentials("client", null!, "token"));
        }

        [Fact]
        public void ConstructorWithFullCredentials_WithNullAccessToken_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new LawMaticsCredentials("client", "secret", null!));
        }

        [Fact]
        public void IsExpired_WithExpiredToken_ReturnsTrue()
        {
            // Arrange
            var credentials = new LawMaticsCredentials("client", "secret", "token", "refresh", DateTime.UtcNow.AddMinutes(-10));

            // Act & Assert
            Assert.True(credentials.IsExpired);
        }

        [Fact]
        public void IsExpired_WithTokenExpiringSoon_ReturnsTrue()
        {
            // Arrange
            var credentials = new LawMaticsCredentials("client", "secret", "token", "refresh", DateTime.UtcNow.AddMinutes(3));

            // Act & Assert
            Assert.True(credentials.IsExpired);
        }

        [Fact]
        public void IsExpired_WithValidToken_ReturnsFalse()
        {
            // Arrange
            var credentials = new LawMaticsCredentials("client", "secret", "token", "refresh", DateTime.UtcNow.AddHours(1));

            // Act & Assert
            Assert.False(credentials.IsExpired);
        }

        [Fact]
        public void IsExpired_WithNoExpiration_ReturnsFalse()
        {
            // Arrange
            var credentials = new LawMaticsCredentials("client", "secret", "token");

            // Act & Assert
            Assert.False(credentials.IsExpired);
        }

        [Fact]
        public void HasRefreshToken_WithRefreshToken_ReturnsTrue()
        {
            // Arrange
            var credentials = new LawMaticsCredentials("client", "secret", "token", "refresh-token");

            // Act & Assert
            Assert.True(credentials.HasRefreshToken);
        }

        [Fact]
        public void HasRefreshToken_WithNullRefreshToken_ReturnsFalse()
        {
            // Arrange
            var credentials = new LawMaticsCredentials("client", "secret", "token");

            // Act & Assert
            Assert.False(credentials.HasRefreshToken);
        }

        [Fact]
        public void HasRefreshToken_WithEmptyRefreshToken_ReturnsFalse()
        {
            // Arrange
            var credentials = new LawMaticsCredentials("client", "secret", "token", "");

            // Act & Assert
            Assert.False(credentials.HasRefreshToken);
        }

        [Fact]
        public void FromTokenResponse_WithFullResponse_CreatesCredentials()
        {
            // Arrange
            var tokenResponse = new OAuthTokenResponse
            {
                AccessToken = "new-access-token",
                RefreshToken = "new-refresh-token",
                TokenType = "Bearer",
                ExpiresIn = 3600,
                Scope = "read write"
            };
            var clientId = "test-client-id";
            var clientSecret = "test-client-secret";

            // Act
            var credentials = LawMaticsCredentials.FromTokenResponse(tokenResponse, clientId, clientSecret);

            // Assert
            Assert.Equal(clientId, credentials.ClientId);
            Assert.Equal(clientSecret, credentials.ClientSecret);
            Assert.Equal(tokenResponse.AccessToken, credentials.AccessToken);
            Assert.Equal(tokenResponse.RefreshToken, credentials.RefreshToken);
            Assert.Equal(tokenResponse.TokenType, credentials.TokenType);
            Assert.True(credentials.ExpiresAt.HasValue);
            Assert.True(credentials.ExpiresAt.Value > DateTime.UtcNow);
            Assert.True(credentials.ExpiresAt.Value <= DateTime.UtcNow.AddSeconds(3600).AddMinutes(1)); // Allow 1 minute tolerance
        }

        [Fact]
        public void FromTokenResponse_WithNoExpiration_CreatesCredentialsWithoutExpiration()
        {
            // Arrange
            var tokenResponse = new OAuthTokenResponse
            {
                AccessToken = "new-access-token",
                RefreshToken = "new-refresh-token",
                TokenType = "Bearer"
                // No ExpiresIn
            };
            var clientId = "test-client-id";
            var clientSecret = "test-client-secret";

            // Act
            var credentials = LawMaticsCredentials.FromTokenResponse(tokenResponse, clientId, clientSecret);

            // Assert
            Assert.Equal(clientId, credentials.ClientId);
            Assert.Equal(clientSecret, credentials.ClientSecret);
            Assert.Equal(tokenResponse.AccessToken, credentials.AccessToken);
            Assert.Equal(tokenResponse.RefreshToken, credentials.RefreshToken);
            Assert.Equal(tokenResponse.TokenType, credentials.TokenType);
            Assert.False(credentials.ExpiresAt.HasValue);
        }

        [Fact]
        public void FromTokenResponse_WithNullTokenResponse_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                LawMaticsCredentials.FromTokenResponse(null!, "client", "secret"));
        }

        [Fact]
        public void UpdateToken_WithAllParameters_UpdatesAllTokenProperties()
        {
            // Arrange
            var credentials = new LawMaticsCredentials("client", "secret", "old-token");
            var newAccessToken = "new-access-token";
            var newRefreshToken = "new-refresh-token";
            var expiresIn = 7200;

            // Act
            credentials.UpdateToken(newAccessToken, newRefreshToken, expiresIn);

            // Assert
            Assert.Equal(newAccessToken, credentials.AccessToken);
            Assert.Equal(newRefreshToken, credentials.RefreshToken);
            Assert.True(credentials.ExpiresAt.HasValue);
            Assert.True(credentials.ExpiresAt.Value > DateTime.UtcNow);
            Assert.True(credentials.ExpiresAt.Value <= DateTime.UtcNow.AddSeconds(expiresIn).AddMinutes(1)); // Allow 1 minute tolerance
        }

        [Fact]
        public void UpdateToken_WithOnlyAccessToken_UpdatesOnlyAccessToken()
        {
            // Arrange
            var credentials = new LawMaticsCredentials("client", "secret", "old-token", "old-refresh");
            var newAccessToken = "new-access-token";

            // Act
            credentials.UpdateToken(newAccessToken);

            // Assert
            Assert.Equal(newAccessToken, credentials.AccessToken);
            Assert.Equal("old-refresh", credentials.RefreshToken); // Should remain unchanged
        }

        [Fact]
        public void UpdateToken_WithEmptyRefreshToken_DoesNotUpdateRefreshToken()
        {
            // Arrange
            var credentials = new LawMaticsCredentials("client", "secret", "old-token", "old-refresh");
            var newAccessToken = "new-access-token";

            // Act
            credentials.UpdateToken(newAccessToken, "", null);

            // Assert
            Assert.Equal(newAccessToken, credentials.AccessToken);
            Assert.Equal("old-refresh", credentials.RefreshToken); // Should remain unchanged
        }

        [Fact]
        public void UpdateToken_WithNullAccessToken_ThrowsArgumentNullException()
        {
            // Arrange
            var credentials = new LawMaticsCredentials("client", "secret", "old-token");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => credentials.UpdateToken(null!));
        }
    }
}