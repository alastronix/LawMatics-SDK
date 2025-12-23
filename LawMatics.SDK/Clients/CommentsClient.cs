using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using LawMatics.SDK.Configuration;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing comments in the LawMatics API
    /// </summary>
    public class CommentsClient : BaseClient
    {
        public CommentsClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger = null)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Gets a list of comments
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of comments</returns>
        public async Task<List<Comment>> GetCommentsAsync(FilterParameters? parameters = null, CancellationToken cancellationToken = default)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Comment>>($"comments?{queryParams}", cancellationToken);
            return response ?? new List<Comment>();
        }

        /// <summary>
        /// Gets a specific comment by ID
        /// </summary>
        /// <param name="id">Comment ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Comment details</returns>
        public async Task<Comment?> GetCommentAsync(int id, CancellationToken cancellationToken = default)
        {
            var response = await GetAsync<Comment>($"comments/{id}", cancellationToken: cancellationToken);
            return response;
        }

        /// <summary>
        /// Creates a new comment
        /// </summary>
        /// <param name="request">Comment creation request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Created comment</returns>
        public async Task<Comment?> CreateCommentAsync(CreateCommentRequest request, CancellationToken cancellationToken = default)
        {
            var response = await PostAsync<Comment>("comments", request, cancellationToken);
            return response;
        }

        /// <summary>
        /// Updates an existing comment
        /// </summary>
        /// <param name="id">Comment ID</param>
        /// <param name="request">Comment update request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Updated comment</returns>
        public async Task<Comment?> UpdateCommentAsync(int id, UpdateCommentRequest request, CancellationToken cancellationToken = default)
        {
            var response = await PutAsync<ApiResponse<Comment>>($"comments/{id}", request, cancellationToken);
            return response.Data;
        }

        /// <summary>
        /// Deletes a comment
        /// </summary>
        /// <param name="id">Comment ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if successful</returns>
        public async Task<bool> DeleteCommentAsync(int id, CancellationToken cancellationToken = default)
        {
            var response = await DeleteAsync<ApiResponse<object>>($"comments/{id}", cancellationToken);
            return true; // If no exception thrown, deletion was successful
        }

        /// <summary>
        /// Gets comments for a specific entity
        /// </summary>
        /// <param name="entityType">Type of entity (e.g., 'contact', 'matter', 'company')</param>
        /// <param name="entityId">ID of the entity</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of comments for the entity</returns>
        public async Task<PagedResponse<Comment>> GetCommentsByEntityAsync(string entityType, int entityId, GetCommentsParameters? parameters = null, CancellationToken cancellationToken = default)
        {
            var queryParams = parameters ?? new GetCommentsParameters();
            queryParams.EntityType = entityType;
            queryParams.EntityId = entityId;
            
            var queryString = BuildQueryString(queryParams);
            return await GetAsync<PagedResponse<Comment>>($"comments?{queryString}", cancellationToken);
        }

        /// <summary>
        /// Gets comments created by a specific user
        /// </summary>
        /// <param name="createdById">User ID who created the comments</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of comments created by the user</returns>
        public async Task<PagedResponse<Comment>> GetCommentsByUserAsync(int createdById, GetCommentsParameters? parameters = null, CancellationToken cancellationToken = default)
        {
            var queryParams = parameters ?? new GetCommentsParameters();
            queryParams.CreatedById = createdById;
            
            var queryString = BuildQueryString(queryParams);
            return await GetAsync<PagedResponse<Comment>>($"comments?{queryString}", cancellationToken);
        }

        /// <summary>
        /// Gets comments created within a date range
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of comments created within the date range</returns>
        public async Task<PagedResponse<Comment>> GetCommentsByDateRangeAsync(DateTime startDate, DateTime endDate, GetCommentsParameters? parameters = null, CancellationToken cancellationToken = default)
        {
            var queryParams = parameters ?? new GetCommentsParameters();
            queryParams.StartDate = startDate;
            queryParams.EndDate = endDate;
            
            var queryString = BuildQueryString(queryParams);
            return await GetAsync<PagedResponse<Comment>>($"comments?{queryString}", cancellationToken);
        }

        /// <summary>
        /// Gets internal comments
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of internal comments</returns>
        public async Task<PagedResponse<Comment>> GetInternalCommentsAsync(GetCommentsParameters? parameters = null, CancellationToken cancellationToken = default)
        {
            var queryParams = parameters ?? new GetCommentsParameters();
            queryParams.IsInternal = true;
            
            var queryString = BuildQueryString(queryParams);
            return await GetAsync<PagedResponse<Comment>>($"comments?{queryString}", cancellationToken);
        }

        /// <summary>
        /// Gets public comments
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of public comments</returns>
        public async Task<PagedResponse<Comment>> GetPublicCommentsAsync(GetCommentsParameters? parameters = null, CancellationToken cancellationToken = default)
        {
            var queryParams = parameters ?? new GetCommentsParameters();
            queryParams.IsInternal = false;
            
            var queryString = BuildQueryString(queryParams);
            return await GetAsync<PagedResponse<Comment>>($"comments?{queryString}", cancellationToken);
        }

        /// <summary>
        /// Searches comments by content
        /// </summary>
        /// <param name="searchText">Text to search for in comment content</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of comments matching the search text</returns>
        public async Task<PagedResponse<Comment>> SearchCommentsAsync(string searchText, GetCommentsParameters? parameters = null, CancellationToken cancellationToken = default)
        {
            var queryParams = parameters ?? new GetCommentsParameters();
            queryParams.Search = searchText;
            
            var queryString = BuildQueryString(queryParams);
            return await GetAsync<PagedResponse<Comment>>($"comments?{queryString}", cancellationToken);
        }

        /// <summary>
        /// Bulk creates multiple comments
        /// </summary>
        /// <param name="requests">List of comment creation requests</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of created comments</returns>
        public async Task<List<Comment>> BulkCreateCommentsAsync(List<CreateCommentRequest> requests, CancellationToken cancellationToken = default)
        {
            var response = await PostAsync<ApiResponse<List<Comment>>>("comments/bulk", new { comments = requests }, cancellationToken);
            return response.Data ?? new List<Comment>();
        }

        /// <summary>
        /// Bulk deletes multiple comments
        /// </summary>
        /// <param name="commentIds">List of comment IDs to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if successful</returns>
        public async Task<bool> BulkDeleteCommentsAsync(List<int> commentIds, CancellationToken cancellationToken = default)
        {
            var response = await PostAsync<ApiResponse<object>>("comments/bulk/delete", new { comment_ids = commentIds }, cancellationToken);
            return true; // If no exception thrown, deletion was successful
        }
    }
}