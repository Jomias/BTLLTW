using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QLNH.Models.ViewModels
{
	public class EmployeeViewModel
	{
		public long Id { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public string? Avatar { get; set; }
		public string Position { get; set; } = null!;
	}
}
