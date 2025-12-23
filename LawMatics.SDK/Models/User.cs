using System.Text.Json.Serialization;

namespace LawMatics.SDK.Models
{
    /// <summary>
    /// Represents a user in Lawmatics
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique identifier for the user
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// User's last name
        /// </summary>
        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// User's full name
        /// </summary>
        [JsonPropertyName("full_name")]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// User's email address
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// User's phone number
        /// </summary>
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        /// <summary>
        /// User's mobile phone number
        /// </summary>
        [JsonPropertyName("mobile")]
        public string? Mobile { get; set; }

        /// <summary>
        /// User's job title
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// User's department
        /// </summary>
        [JsonPropertyName("department")]
        public string? Department { get; set; }

        /// <summary>
        /// User's role
        /// </summary>
        [JsonPropertyName("role")]
        public string? Role { get; set; }

        /// <summary>
        /// User's permissions
        /// </summary>
        [JsonPropertyName("permissions")]
        public List<string>? Permissions { get; set; }

        /// <summary>
        /// User's status (active, inactive, suspended)
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Whether the user is an admin
        /// </summary>
        [JsonPropertyName("is_admin")]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// User's avatar URL
        /// </summary>
        [JsonPropertyName("avatar_url")]
        public string? AvatarUrl { get; set; }

        /// <summary>
        /// User's timezone
        /// </summary>
        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }

        /// <summary>
        /// User's locale/language
        /// </summary>
        [JsonPropertyName("locale")]
        public string? Locale { get; set; }

        /// <summary>
        /// User's address
        /// </summary>
        [JsonPropertyName("address")]
        public Address? Address { get; set; }

        /// <summary>
        /// Last login date
        /// </summary>
        [JsonPropertyName("last_login_at")]
        public DateTime? LastLoginAt { get; set; }

        /// <summary>
        /// Email verification status
        /// </summary>
        [JsonPropertyName("email_verified")]
        public bool EmailVerified { get; set; }

        /// <summary>
        /// Two-factor authentication enabled
        /// </summary>
        [JsonPropertyName("two_factor_enabled")]
        public bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// Date when user was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when user was last updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Custom fields for the user
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }

    /// <summary>
    /// Request model for creating a new user
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// User's first name (required)
        /// </summary>
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// User's last name (required)
        /// </summary>
        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// User's email address (required)
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// User's password (required for creation)
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// User's phone number
        /// </summary>
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        /// <summary>
        /// User's mobile phone number
        /// </summary>
        [JsonPropertyName("mobile")]
        public string? Mobile { get; set; }

        /// <summary>
        /// User's job title
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// User's department
        /// </summary>
        [JsonPropertyName("department")]
        public string? Department { get; set; }

        /// <summary>
        /// User's role
        /// </summary>
        [JsonPropertyName("role")]
        public string? Role { get; set; }

        /// <summary>
        /// User's permissions
        /// </summary>
        [JsonPropertyName("permissions")]
        public List<string>? Permissions { get; set; }

        /// <summary>
        /// User's status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = "active";

        /// <summary>
        /// Whether the user is an admin
        /// </summary>
        [JsonPropertyName("is_admin")]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// User's timezone
        /// </summary>
        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }

        /// <summary>
        /// User's locale/language
        /// </summary>
        [JsonPropertyName("locale")]
        public string? Locale { get; set; }

        /// <summary>
        /// User's address
        /// </summary>
        [JsonPropertyName("address")]
        public Address? Address { get; set; }

        /// <summary>
        /// Whether to send invitation email
        /// </summary>
        [JsonPropertyName("send_invitation")]
        public bool SendInvitation { get; set; } = true;

        /// <summary>
        /// Custom fields for the user
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }

    /// <summary>
    /// Request model for updating an existing user
    /// </summary>
    public class UpdateUserRequest
    {
        /// <summary>
        /// User's first name
        /// </summary>
        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        /// <summary>
        /// User's email address
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        /// User's phone number
        /// </summary>
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        /// <summary>
        /// User's mobile phone number
        /// </summary>
        [JsonPropertyName("mobile")]
        public string? Mobile { get; set; }

        /// <summary>
        /// User's job title
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// User's department
        /// </summary>
        [JsonPropertyName("department")]
        public string? Department { get; set; }

        /// <summary>
        /// User's role
        /// </summary>
        [JsonPropertyName("role")]
        public string? Role { get; set; }

        /// <summary>
        /// User's permissions
        /// </summary>
        [JsonPropertyName("permissions")]
        public List<string>? Permissions { get; set; }

        /// <summary>
        /// User's status
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Whether the user is an admin
        /// </summary>
        [JsonPropertyName("is_admin")]
        public bool? IsAdmin { get; set; }

        /// <summary>
        /// User's timezone
        /// </summary>
        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }

        /// <summary>
        /// User's locale/language
        /// </summary>
        [JsonPropertyName("locale")]
        public string? Locale { get; set; }

        /// <summary>
        /// User's address
        /// </summary>
        [JsonPropertyName("address")]
        public Address? Address { get; set; }

        /// <summary>
        /// Custom fields for the user
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }

    /// <summary>
    /// Request model for changing user password
    /// </summary>
    public class ChangePasswordRequest
    {
        /// <summary>
        /// Current password (required)
        /// </summary>
        [JsonPropertyName("current_password")]
        public string CurrentPassword { get; set; } = string.Empty;

        /// <summary>
        /// New password (required)
        /// </summary>
        [JsonPropertyName("new_password")]
        public string NewPassword { get; set; } = string.Empty;

        /// <summary>
        /// Confirm new password (required)
        /// </summary>
        [JsonPropertyName("confirm_password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    /// <summary>
    /// Request model for resetting user password
    /// </summary>
    public class ResetPasswordRequest
    {
        /// <summary>
        /// User's email address (required)
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Password reset token
        /// </summary>
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        /// <summary>
        /// New password
        /// </summary>
        [JsonPropertyName("new_password")]
        public string? NewPassword { get; set; }
    }
}