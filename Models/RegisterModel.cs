using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class RegisterModel
    {
        [Key]
        [EmailAddress] // Email validation
        public string Email { get; set; }

        [Required] // Ensure the username is required
        [StringLength(100, MinimumLength = 3)] // Minimum length of 3 for username
        public string Username { get; set; }

        [Required] // Ensure password is required
        [StringLength(100, MinimumLength = 6)] // Minimum length of 6 for password
        public string Password { get; set; }

        // Flag to track if the user is an admin (this is optional, depending on your application needs)
        public bool IsAdmin { get; set; } = false;

        // Approval flag: Default is false (not approved)
        public bool IsApproved { get; set; } = false;

        // Store the approval token (for email validation/approval)
        public string ApprovalToken { get; set; }
    }
}
