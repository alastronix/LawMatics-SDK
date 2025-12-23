using LawMatics.SDK.Configuration;
using LawMatics.SDK.Exceptions;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing files in the Lawmatics API
    /// </summary>
    public class FilesClient : BaseClient
    {
        public FilesClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger = null)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Get all files with optional filtering and pagination
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>Paginated list of files</returns>
        public async Task<ApiResponse<List<LawMaticsFile>>> GetFilesAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<LawMaticsFile>>($"/files?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get a specific file by ID
        /// </summary>
        /// <param name="id">File ID</param>
        /// <returns>File details</returns>
        public async Task<LawMaticsFile> GetFileAsync(int id)
        {
            var response = await GetAsync<LawMaticsFile>($"/files/{id}");
            return response.Data;
        }

        /// <summary>
        /// Upload a file from a file path
        /// </summary>
        /// <param name="filePath">Local file path</param>
        /// <param name="description">Optional file description</param>
        /// <param name="folderId">Optional folder ID</param>
        /// <param name="contactId">Optional contact ID</param>
        /// <param name="matterId">Optional matter ID</param>
        /// <param name="isPublic">Whether file is public</param>
        /// <returns>Uploaded file details</returns>
        public async Task<LawMaticsFile> UploadFileAsync(string filePath, string? description = null, int? folderId = null, int? contactId = null, int? matterId = null, bool isPublic = false)
        {
            if (!File.Exists(filePath))
                throw new ArgumentException($"File not found: {filePath}");

            using var fileStream = File.OpenRead(filePath);
            var fileName = Path.GetFileName(filePath);
            var contentType = GetContentType(filePath);

            return await UploadFileAsync(fileStream, fileName, contentType, description, folderId, contactId, matterId, isPublic);
        }

        /// <summary>
        /// Upload a file from a stream
        /// </summary>
        /// <param name="fileStream">File content stream</param>
        /// <param name="fileName">File name</param>
        /// <param name="contentType">MIME content type</param>
        /// <param name="description">Optional file description</param>
        /// <param name="folderId">Optional folder ID</param>
        /// <param name="contactId">Optional contact ID</param>
        /// <param name="matterId">Optional matter ID</param>
        /// <param name="isPublic">Whether file is public</param>
        /// <returns>Uploaded file details</returns>
        public async Task<LawMaticsFile> UploadFileAsync(Stream fileStream, string fileName, string contentType, string? description = null, int? folderId = null, int? contactId = null, int? matterId = null, bool isPublic = false)
        {
            using var content = new MultipartFormDataContent();
            
            using var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
            content.Add(fileContent, "file", fileName);

            if (!string.IsNullOrEmpty(description))
                content.Add(new StringContent(description), "description");

            if (folderId.HasValue)
                content.Add(new StringContent(folderId.Value.ToString()), "folder_id");

            if (contactId.HasValue)
                content.Add(new StringContent(contactId.Value.ToString()), "contact_id");

            if (matterId.HasValue)
                content.Add(new StringContent(matterId.Value.ToString()), "matter_id");

            content.Add(new StringContent(isPublic.ToString().ToLower()), "is_public");

            var request = new HttpRequestMessage(HttpMethod.Post, "/files/upload") { Content = content };
            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new LawMaticsApiException($"Failed to upload file: {errorContent}", (int)response.StatusCode);
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = System.Text.Json.JsonSerializer.Deserialize<ApiResponse<LawMaticsFile>>(responseContent, _jsonOptions);
            return result?.Data ?? throw new LawMaticsException("Failed to parse upload response");
        }

        /// <summary>
        /// Update file metadata
        /// </summary>
        /// <param name="id">File ID</param>
        /// <param name="request">File update request</param>
        /// <returns>Updated file</returns>
        public async Task<LawMaticsFile> UpdateFileAsync(int id, UpdateFileRequest request)
        {
            var response = await PutAsync<LawMaticsFile>($"/files/{id}", request);
            return response.Data;
        }

        /// <summary>
        /// Delete a file
        /// </summary>
        /// <param name="id">File ID</param>
        public async Task DeleteFileAsync(int id)
        {
            await DeleteAsync($"/files/{id}");
        }

        /// <summary>
        /// Download a file as a stream
        /// </summary>
        /// <param name="id">File ID</param>
        /// <returns>File content stream</returns>
        public async Task<Stream> DownloadFileAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/files/{id}/download");
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new LawMaticsApiException($"Failed to download file: {errorContent}", (int)response.StatusCode);
            }

            return await response.Content.ReadAsStreamAsync();
        }

        /// <summary>
        /// Download a file to a local path
        /// </summary>
        /// <param name="id">File ID</param>
        /// <param name="localPath">Local file path to save to</param>
        /// <returns>Local file path</returns>
        public async Task<string> DownloadFileAsync(int id, string localPath)
        {
            using var fileStream = await DownloadFileAsync(id);
            using var localFileStream = File.Create(localPath);
            await fileStream.CopyToAsync(localFileStream);
            return localPath;
        }

        /// <summary>
        /// Get files by folder
        /// </summary>
        /// <param name="folderId">Folder ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of files in folder</returns>
        public async Task<ApiResponse<List<LawMaticsFile>>> GetFilesByFolderAsync(int folderId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<LawMaticsFile>>($"/files?folder_id={folderId}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get files by contact
        /// </summary>
        /// <param name="contactId">Contact ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of files for contact</returns>
        public async Task<ApiResponse<List<LawMaticsFile>>> GetFilesByContactAsync(int contactId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<LawMaticsFile>>($"/files?contact_id={contactId}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get files by matter
        /// </summary>
        /// <param name="matterId">Matter ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of files for matter</returns>
        public async Task<ApiResponse<List<LawMaticsFile>>> GetFilesByMatterAsync(int matterId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<LawMaticsFile>>($"/files?matter_id={matterId}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Search files by name
        /// </summary>
        /// <param name="fileName">File name to search for</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of matching files</returns>
        public async Task<ApiResponse<List<LawMaticsFile>>> SearchFilesByNameAsync(string fileName, FilterParameters? parameters = null)
        {
            var filterParams = parameters ?? new FilterParameters();
            filterParams.Search = fileName;
            
            var queryParams = BuildQueryString(filterParams);
            var response = await GetAsync<List<LawMaticsFile>>($"/files?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get files by type
        /// </summary>
        /// <param name="fileType">File type to filter by</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of files of specified type</returns>
        public async Task<ApiResponse<List<LawMaticsFile>>> GetFilesByTypeAsync(string fileType, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<LawMaticsFile>>($"/files?file_type={fileType}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get public files
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of public files</returns>
        public async Task<ApiResponse<List<LawMaticsFile>>> GetPublicFilesAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<LawMaticsFile>>($"/files?is_public=true&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get private files
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of private files</returns>
        public async Task<ApiResponse<List<LawMaticsFile>>> GetPrivateFilesAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<LawMaticsFile>>($"/files?is_public=false&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get files uploaded within a date range
        /// </summary>
        /// <param name="startDate">Start date filter</param>
        /// <param name="endDate">End date filter</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of files uploaded within date range</returns>
        public async Task<ApiResponse<List<LawMaticsFile>>> GetFilesByDateRangeAsync(DateTime startDate, DateTime endDate, FilterParameters? parameters = null)
        {
            var filterParams = parameters ?? new FilterParameters();
            filterParams.StartDate = startDate;
            filterParams.EndDate = endDate;
            
            var queryParams = BuildQueryString(filterParams);
            var response = await GetAsync<List<LawMaticsFile>>($"/files?{queryParams}");
            return response;
        }

        /// <summary>
        /// Move file to different folder
        /// </summary>
        /// <param name="fileId">File ID</param>
        /// <param name="folderId">New folder ID</param>
        /// <returns>Updated file</returns>
        public async Task<LawMaticsFile> MoveFileAsync(int fileId, int folderId)
        {
            var response = await PostAsync<LawMaticsFile>($"/files/{fileId}/move", new { folder_id = folderId });
            return response.Data;
        }

        /// <summary>
        /// Copy file to different folder
        /// </summary>
        /// <param name="fileId">File ID</param>
        /// <param name="folderId">Target folder ID</param>
        /// <returns>Copied file</returns>
        public async Task<LawMaticsFile> CopyFileAsync(int fileId, int folderId)
        {
            var response = await PostAsync<LawMaticsFile>($"/files/{fileId}/copy", new { folder_id = folderId });
            return response.Data;
        }

        /// <summary>
        /// Get file thumbnail URL
        /// </summary>
        /// <param name="id">File ID</param>
        /// <returns>Thumbnail URL</returns>
        public async Task<string> GetFileThumbnailUrlAsync(int id)
        {
            var response = await GetAsync<string>($"/files/{id}/thumbnail");
            return response.Data;
        }

        /// <summary>
        /// Generate public share link for file
        /// </summary>
        /// <param name="id">File ID</param>
        /// <param name="expiresInDays">Number of days until link expires</param>
        /// <returns>Public share link</returns>
        public async Task<string> GenerateShareLinkAsync(int id, int expiresInDays = 7)
        {
            var response = await PostAsync<string>($"/files/{id}/share", new { expires_in_days = expiresInDays });
            return response.Data;
        }

        /// <summary>
        /// Bulk delete multiple files
        /// </summary>
        /// <param name="fileIds">List of file IDs to delete</param>
        public async Task BulkDeleteFilesAsync(List<int> fileIds)
        {
            await PostAsync<object>("/files/bulk-delete", new { file_ids = fileIds });
        }

        /// <summary>
        /// Bulk move multiple files
        /// </summary>
        /// <param name="fileIds">List of file IDs to move</param>
        /// <param name="folderId">Target folder ID</param>
        public async Task BulkMoveFilesAsync(List<int> fileIds, int folderId)
        {
            await PostAsync<object>("/files/bulk-move", new { file_ids = fileIds, folder_id = folderId });
        }

        /// <summary>
        /// Get file metadata only (without content)
        /// </summary>
        /// <param name="id">File ID</param>
        /// <returns>File metadata</returns>
        public async Task<LawMaticsFile> GetFileMetadataAsync(int id)
        {
            var response = await GetAsync<LawMaticsFile>($"/files/{id}/metadata");
            return response.Data;
        }

        private string GetContentType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xls" => "application/vnd.ms-excel",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".ppt" => "application/vnd.ms-powerpoint",
                ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                ".txt" => "text/plain",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".tiff" => "image/tiff",
                ".zip" => "application/zip",
                ".rar" => "application/x-rar-compressed",
                _ => "application/octet-stream"
            };
        }
    }
}