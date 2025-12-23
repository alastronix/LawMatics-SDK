using LawMatics.SDK.Configuration;
using LawMatics.SDK.Exceptions;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing folders in the Lawmatics API
    /// </summary>
    public class FoldersClient : BaseClient
    {
        public FoldersClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger = null)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Get all folders with optional filtering and pagination
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>Paginated list of folders</returns>
        public async Task<ApiResponse<List<Folder>>> GetFoldersAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Folder>>($"/folders?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get a specific folder by ID
        /// </summary>
        /// <param name="id">Folder ID</param>
        /// <returns>Folder details</returns>
        public async Task<Folder> GetFolderAsync(int id)
        {
            var response = await GetAsync<Folder>($"/folders/{id}");
            return response.Data;
        }

        /// <summary>
        /// Create a new folder
        /// </summary>
        /// <param name="request">Folder creation request</param>
        /// <returns>Created folder</returns>
        public async Task<Folder> CreateFolderAsync(CreateFolderRequest request)
        {
            var response = await PostAsync<Folder>("/folders", request);
            return response.Data;
        }

        /// <summary>
        /// Update an existing folder
        /// </summary>
        /// <param name="id">Folder ID</param>
        /// <param name="request">Folder update request</param>
        /// <returns>Updated folder</returns>
        public async Task<Folder> UpdateFolderAsync(int id, UpdateFolderRequest request)
        {
            var response = await PutAsync<Folder>($"/folders/{id}", request);
            return response.Data;
        }

        /// <summary>
        /// Delete a folder
        /// </summary>
        /// <param name="id">Folder ID</param>
        public async Task DeleteFolderAsync(int id)
        {
            await DeleteAsync($"/folders/{id}");
        }

        /// <summary>
        /// Get root folders (folders without parent)
        /// </summary>
        /// <returns>List of root folders</returns>
        public async Task<ApiResponse<List<Folder>>> GetRootFoldersAsync()
        {
            var response = await GetAsync<List<Folder>>("/folders?parent_id=null");
            return response;
        }

        /// <summary>
        /// Get subfolders of a parent folder
        /// </summary>
        /// <param name="parentId">Parent folder ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of subfolders</returns>
        public async Task<ApiResponse<List<Folder>>> GetSubfoldersAsync(int parentId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Folder>>($"/folders?parent_id={parentId}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get folder tree (hierarchical structure)
        /// </summary>
        /// <param name="rootFolderId">Optional root folder ID to start from</param>
        /// <returns>Folder tree structure</returns>
        public async Task<ApiResponse<List<Folder>>> GetFolderTreeAsync(int? rootFolderId = null)
        {
            var endpoint = rootFolderId.HasValue ? $"/folders/tree?root_id={rootFolderId.Value}" : "/folders/tree";
            var response = await GetAsync<List<Folder>>(endpoint);
            return response;
        }

        /// <summary>
        /// Get folder with full path
        /// </summary>
        /// <param name="id">Folder ID</param>
        /// <returns>Folder with full path information</returns>
        public async Task<Folder> GetFolderWithPathAsync(int id)
        {
            var response = await GetAsync<Folder>($"/folders/{id}/path");
            return response.Data;
        }

        /// <summary>
        /// Search folders by name
        /// </summary>
        /// <param name="name">Folder name to search for</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of matching folders</returns>
        public async Task<ApiResponse<List<Folder>>> SearchFoldersByNameAsync(string name, FilterParameters? parameters = null)
        {
            var filterParams = parameters ?? new FilterParameters();
            filterParams.Search = name;
            
            var queryParams = BuildQueryString(filterParams);
            var response = await GetAsync<List<Folder>>($"/folders?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get public folders
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of public folders</returns>
        public async Task<ApiResponse<List<Folder>>> GetPublicFoldersAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Folder>>($"/folders?is_public=true&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get private folders
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of private folders</returns>
        public async Task<ApiResponse<List<Folder>>> GetPrivateFoldersAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Folder>>($"/folders?is_public=false&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get folders created within a date range
        /// </summary>
        /// <param name="startDate">Start date filter</param>
        /// <param name="endDate">End date filter</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of folders created within date range</returns>
        public async Task<ApiResponse<List<Folder>>> GetFoldersByDateRangeAsync(DateTime startDate, DateTime endDate, FilterParameters? parameters = null)
        {
            var filterParams = parameters ?? new FilterParameters();
            filterParams.StartDate = startDate;
            filterParams.EndDate = endDate;
            
            var queryParams = BuildQueryString(filterParams);
            var response = await GetAsync<List<Folder>>($"/folders?{queryParams}");
            return response;
        }

        /// <summary>
        /// Move folder to a new parent
        /// </summary>
        /// <param name="folderId">Folder ID to move</param>
        /// <param name="newParentId">New parent folder ID (null for root)</param>
        /// <returns>Moved folder</returns>
        public async Task<Folder> MoveFolderAsync(int folderId, int? newParentId)
        {
            var response = await PostAsync<Folder>($"/folders/{folderId}/move", new { parent_id = newParentId });
            return response.Data;
        }

        /// <summary>
        /// Copy folder with all its contents
        /// </summary>
        /// <param name="folderId">Folder ID to copy</param>
        /// <param name="newParentId">Target parent folder ID</param>
        /// <param name="newName">Optional new name for copied folder</param>
        /// <returns>Copied folder</returns>
        public async Task<Folder> CopyFolderAsync(int folderId, int newParentId, string? newName = null)
        {
            var request = new { parent_id = newParentId, name = newName };
            var response = await PostAsync<Folder>($"/folders/{folderId}/copy", request);
            return response.Data;
        }

        /// <summary>
        /// Get folder statistics
        /// </summary>
        /// <param name="id">Folder ID</param>
        /// <returns>Folder statistics (file count, total size, etc.)</returns>
        public async Task<object> GetFolderStatisticsAsync(int id)
        {
            var response = await GetAsync<object>($"/folders/{id}/statistics");
            return response.Data;
        }

        /// <summary>
        /// Get folder size
        /// </summary>
        /// <param name="id">Folder ID</param>
        /// <returns>Total size of folder in bytes</returns>
        public async Task<long> GetFolderSizeAsync(int id)
        {
            var response = await GetAsync<long>($"/folders/{id}/size");
            return response.Data;
        }

        /// <summary>
        /// Get folder file count
        /// </summary>
        /// <param name="id">Folder ID</param>
        /// <returns>Total number of files in folder (including subfolders)</returns>
        public async Task<int> GetFolderFileCountAsync(int id)
        {
            var response = await GetAsync<int>($"/folders/{id}/file-count");
            return response.Data;
        }

        /// <summary>
        /// Get folder contents (both files and subfolders)
        /// </summary>
        /// <param name="id">Folder ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>Folder contents</returns>
        public async Task<Folder> GetFolderContentsAsync(int id, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<Folder>($"/folders/{id}/contents?{queryParams}");
            return response.Data;
        }

        /// <summary>
        /// Set folder permissions
        /// </summary>
        /// <param name="folderId">Folder ID</param>
        /// <param name="isPublic">Whether folder should be public</param>
        public async Task SetFolderPermissionsAsync(int folderId, bool isPublic)
        {
            await PostAsync<object>($"/folders/{folderId}/permissions", new { is_public = isPublic });
        }

        /// <summary>
        /// Share folder with specific users
        /// </summary>
        /// <param name="folderId">Folder ID</param>
        /// <param name="userIds">List of user IDs to share with</param>
        /// <param name="permissions">Permission level</param>
        public async Task ShareFolderAsync(int folderId, List<int> userIds, string permissions = "read")
        {
            await PostAsync<object>($"/folders/{folderId}/share", new { user_ids = userIds, permissions });
        }

        /// <summary>
        /// Unshare folder with specific users
        /// </summary>
        /// <param name="folderId">Folder ID</param>
        /// <param name="userIds">List of user IDs to unshare from</param>
        public async Task UnshareFolderAsync(int folderId, List<int> userIds)
        {
            await PostAsync<object>($"/folders/{folderId}/unshare", new { user_ids = userIds });
        }

        /// <summary>
        /// Bulk create multiple folders
        /// </summary>
        /// <param name="requests">List of folder creation requests</param>
        /// <returns>List of created folders</returns>
        public async Task<List<Folder>> BulkCreateFoldersAsync(List<CreateFolderRequest> requests)
        {
            var response = await PostAsync<List<Folder>>("/folders/bulk", new { folders = requests });
            return response.Data;
        }

        /// <summary>
        /// Bulk delete multiple folders
        /// </summary>
        /// <param name="folderIds">List of folder IDs to delete</param>
        public async Task BulkDeleteFoldersAsync(List<int> folderIds)
        {
            await PostAsync<object>("/folders/bulk-delete", new { folder_ids = folderIds });
        }

        /// <summary>
        /// Bulk move multiple folders
        /// </summary>
        /// <param name="folderIds">List of folder IDs to move</param>
        /// <param name="newParentId">New parent folder ID</param>
        public async Task BulkMoveFoldersAsync(List<int> folderIds, int? newParentId)
        {
            await PostAsync<object>("/folders/bulk-move", new { folder_ids = folderIds, parent_id = newParentId });
        }

        /// <summary>
        /// Get folder path breadcrumb
        /// </summary>
        /// <param name="id">Folder ID</param>
        /// <returns>Breadcrumb path to folder</returns>
        public async Task<ApiResponse<List<Folder>>> GetFolderBreadcrumbAsync(int id)
        {
            var response = await GetAsync<List<Folder>>($"/folders/{id}/breadcrumb");
            return response;
        }

        /// <summary>
        /// Find folder by path
        /// </summary>
        /// <param name="path">Folder path to search for</param>
        /// <returns>Matching folder</returns>
        public async Task<Folder?> FindFolderByPathAsync(string path)
        {
            var response = await GetAsync<Folder>($"/folders/find/by-path?path={Uri.EscapeDataString(path)}");
            return response.Data;
        }
    }
}