using System.ComponentModel.DataAnnotations;

namespace UserManagementApp.Models
{
    public class ListUser
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}
