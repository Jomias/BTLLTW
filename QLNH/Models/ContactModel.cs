using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QLNH.Models
{
	public class ContactModel
	{
		public long? Id { get; set; }
		[Required]
		[DisplayName("Tên")]
		[MaxLength(50)]
		public string Name { get; set; } = null!;
		[Required]
		[DisplayName("Địa chỉ email")]
		[EmailAddress]
		public string Email { get; set; } = null!;
		[Required]
		[MaxLength(100)]
		[DisplayName("Lời nhắn")]
		public string Message { get; set; } = null!;
		[Required]
		[Phone]
		[DisplayName("Số điện thoại")]
		public string Phone { get; set; } = null!;
		public DateTime? CreatedAt { get; set; }

		public string? UpdatedBy { get; set; }

		public DateTime? UpdatedAt { get; set; }

		public int? Status { get; set; }

		public bool? IsDeleted { get; set; }
	}
}
