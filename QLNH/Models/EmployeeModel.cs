using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLNH.Models
{
	public class EmployeeModel : BaseModel
	{
		public long? Id { get; set; }
		[Required]
		[DisplayName("Tên")]
		[MaxLength(50)]
		public string FirstName { get; set; } = null!;
		[Required]
		[DisplayName("Họ")]
		[MaxLength(50)]
		public string LastName { get; set; } = null!;
		[DisplayName("Mô tả")]
		public string? Description { get; set; }
        [DisplayName("Ảnh đại diện")]
        public string? Avatar { get; set; }
        [DisplayName("Ảnh đại diện")]
		public IFormFile? AvatarFile { get; set; }
        [DisplayName("Vị trí")]
        public long PositionId { get; set; }
	}
}
