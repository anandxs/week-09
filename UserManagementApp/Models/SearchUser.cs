using System.ComponentModel.DataAnnotations;

namespace UserManagementApp.Models
{
	public class SearchUser
	{
		[Required]
		public string SearchString { get; set; } = null!;

        public IEnumerable<ListUser>? Users { get; set; }
    }
}
