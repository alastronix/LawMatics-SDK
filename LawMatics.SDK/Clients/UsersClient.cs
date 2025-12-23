using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LawMatics.SDK.Configuration;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing users in the LawMatics API.
    /// </summary>
    public class UsersClient : BaseClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="options">The client options.</param>
        /// <param name="logger">The optional logger.</param>
        public UsersClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger = null)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Gets a list of users with optional filtering.
        /// </summary>
        /// <param name="parameters">The optional filter and pagination parameters.</param>
        /// <returns>A paginated list of users.</returns>
        public async Task<ApiResponse<List<User>>> GetUsersAsync(FilterParameters? parameters = null)
        {
            var endpoint = "users";
            return await GetAsync<List<User>>(endpoint);
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user details.</returns>
        public async Task<User> GetUserAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentException("User ID must be greater than zero.", nameof(id));

            var endpoint = $"users/{id}";
            var response = await GetAsync<User>(endpoint);
            
            return response.Data ?? throw new Exceptions.LawMaticsNotFoundException("User not found.", 404, null, null, "User", id.ToString());
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user data to create.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The created user details.</returns>
        public async Task<User> CreateUserAsync(CreateUserRequest user, CancellationToken cancellationToken = default)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(user.Email))
                throw new ArgumentException("User email is required.", nameof(user));

            if (string.IsNullOrWhiteSpace(user.FirstName))
                throw new ArgumentException("User first name is required.", nameof(user));

            if (string.IsNullOrWhiteSpace(user.LastName))
                throw new ArgumentException("User last name is required.", nameof(user));

            var endpoint = "users";
            var response = await PostAsync<User>(endpoint, user);
            
            return response.Data ?? throw new Exceptions.LawMaticsException("Failed to create user: No data returned from API.");
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <param name="user">The updated user data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated user details.</returns>
        public async Task<User> UpdateUserAsync(int id, UpdateUserRequest user, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentException("User ID must be greater than zero.", nameof(id));

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var endpoint = $"users/{id}";
            var response = await PutAsync<User>(endpoint, user);
            
            return response.Data ?? throw new Exceptions.LawMaticsException("Failed to update user: No data returned from API.");
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the user was deleted successfully.</returns>
        public async Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentException("User ID must be greater than zero.", nameof(id));

            var endpoint = $"users/{id}";
            await DeleteAsync(endpoint);
            return true;
        }

        /// <summary>
        /// Gets the current authenticated user.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The current user details.</returns>
        public async Task<User> GetCurrentUserAsync(CancellationToken cancellationToken = default)
        {
            var endpoint = "users/me";
            var response = await GetAsync<User>(endpoint);
            
            return response.Data ?? throw new Exceptions.LawMaticsException("Failed to get current user: No data returned from API.");
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Role { get; set; }
        public bool Active { get; set; }
        public string? Avatar { get; set; }
        public string? TimeZone { get; set; }
        public string? Department { get; set; }
        public string? Title { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateUserRequest
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Role { get; set; }
        public string? Department { get; set; }
        public string? Title { get; set; }
        public string? TimeZone { get; set; }
        public string? Password { get; set; }
    }

    public class UpdateUserRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Role { get; set; }
        public bool? Active { get; set; }
        public string? Department { get; set; }
        public string? Title { get; set; }
        public string? TimeZone { get; set; }
    }
}