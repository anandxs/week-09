using System.ComponentModel.DataAnnotations;

namespace UserManagementApp.Models
{
    public class CreateUser
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "{0} should be at least {2} and at most {0} characters long", MinimumLength = 6)]
        public string? Password { get; set; }

        [Required]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match")]
        public string? ConfirmPassword { get; set; }

        public string Role { get; set; } = "User"; // change default if needed
    }
}
