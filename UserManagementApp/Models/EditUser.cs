using System.ComponentModel.DataAnnotations;

namespace UserManagementApp.Models
{
    public class EditUser
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

        [Required] //update if needed
        public string? Role { get; set; }
    }
}
