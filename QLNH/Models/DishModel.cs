using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLNH.Models
{
	public class DishModel : BaseModel
	{
		public long? Id { get; set; }
		[Required]
		[MaxLength(50)]
		[DisplayName("Tên món")]
		public string Name { get; set; } = null!;
		[MaxLength(100)]
		[DisplayName("Đường dẫn")]
		public string? Slug { get; set; }
		[Required]
		[DisplayName("Giá món ăn")]
		public decimal Price { get; set; }
		[Required]
		[MaxLength(20)]
		[DisplayName("Đơn vị")]
		public string Unit { get; set; } = null!;
		[DisplayName("Tóm tắt")]
		public string? Summary { get; set; }
		[DisplayName("Nội dung")]
		public string? Content { get; set; }
		[DisplayName("Hướng dẫn dùng")]
		public string? Instructions { get; set; }
		[DisplayName("Ảnh đại diện")]
		public string? Avatar { get; set; }
        [DisplayName("Ảnh đại diện")]
        public IFormFile? AvatarFile { get; set; }
    }
}
