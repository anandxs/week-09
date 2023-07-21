using System.ComponentModel.DataAnnotations;

namespace UserManagementApp.Models
{
	public class SearchUser
	{
		[Required(ErrorMessage = "Cannot search for empty value")]
		public string SearchString { get; set; } = null!;

        public IEnumerable<DetailsUser>? Users { get; set; }
    }
}
