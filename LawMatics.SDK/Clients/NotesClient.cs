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
    /// Client for managing notes in the LawMatics API.
    /// </summary>
    public class NotesClient : BaseClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotesClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="options">The client options.</param>
        /// <param name="logger">The optional logger.</param>
        public NotesClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger = null)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Gets a list of notes with optional filtering.
        /// </summary>
        /// <param name="parameters">The optional filter and pagination parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A paginated list of notes.</returns>
        public async Task<ApiResponse<List<Note>>> GetNotesAsync(FilterParameters? parameters = null, CancellationToken cancellationToken = default)
        {
            var endpoint = "notes";
            return await GetAsync<List<Note>>(endpoint);
        }

        /// <summary>
        /// Gets a note by ID.
        /// </summary>
        /// <param name="id">The note ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The note details.</returns>
        public async Task<Note> GetNoteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentException("Note ID must be greater than zero.", nameof(id));

            var endpoint = $"notes/{id}";
            var response = await GetAsync<Note>(endpoint);
            
            return response.Data ?? throw new Exceptions.LawMaticsNotFoundException("Note not found.", 404, null, null, "Note", id.ToString());
        }

        /// <summary>
        /// Creates a new note.
        /// </summary>
        /// <param name="note">The note data to create.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The created note details.</returns>
        public async Task<Note> CreateNoteAsync(CreateNoteRequest note, CancellationToken cancellationToken = default)
        {
            if (note == null)
                throw new ArgumentNullException(nameof(note));

            if (string.IsNullOrWhiteSpace(note.Content))
                throw new ArgumentException("Note content is required.", nameof(note));

            var endpoint = "notes";
            var response = await PostAsync<Note>(endpoint, note);
            
            return response.Data ?? throw new Exceptions.LawMaticsException("Failed to create note: No data returned from API.");
        }

        /// <summary>
        /// Updates an existing note.
        /// </summary>
        /// <param name="id">The note ID.</param>
        /// <param name="note">The updated note data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated note details.</returns>
        public async Task<Note> UpdateNoteAsync(int id, UpdateNoteRequest note, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentException("Note ID must be greater than zero.", nameof(id));

            if (note == null)
                throw new ArgumentNullException(nameof(note));

            var endpoint = $"notes/{id}";
            var response = await PutAsync<Note>(endpoint, note);
            
            return response.Data ?? throw new Exceptions.LawMaticsException("Failed to update note: No data returned from API.");
        }

        /// <summary>
        /// Deletes a note.
        /// </summary>
        /// <param name="id">The note ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the note was deleted successfully.</returns>
        public async Task<bool> DeleteNoteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentException("Note ID must be greater than zero.", nameof(id));

            var endpoint = $"notes/{id}";
            await DeleteAsync(endpoint);
            return true;
        }
    }

    public class Note
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public string? NoteType { get; set; }
        public int? ContactId { get; set; }
        public int? MatterId { get; set; }
        public int? CompanyId { get; set; }
        public int? CreatedById { get; set; }
        public string? CreatedByName { get; set; }
        public bool Private { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateNoteRequest
    {
        public string Content { get; set; } = string.Empty;
        public string? NoteType { get; set; }
        public int? ContactId { get; set; }
        public int? MatterId { get; set; }
        public int? CompanyId { get; set; }
        public bool Private { get; set; }
    }

    public class UpdateNoteRequest
    {
        public string? Content { get; set; }
        public string? NoteType { get; set; }
        public bool? Private { get; set; }
    }
}