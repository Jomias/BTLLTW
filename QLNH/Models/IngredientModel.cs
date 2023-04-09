using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLNH.Models
{
	public class IngredientModel : BaseModel
	{
		public long? Id { get; set; }

		[Required]
		[DisplayName("Tên")]
		[MaxLength(50)]
        public string Name { get; set; } = null!;

        [DisplayName("Đường dẫn thân thiện")]
        public string? Slug { get; set; }

        [DisplayName("Giá")]
        public decimal? Price { get; set; }

        [DisplayName("Số lượng")]
        public int? Quantity { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Đơn vị tính")]
        public string Unit { get; set; } = null!;

        [MaxLength(50)]
        [DisplayName("Tóm tắt")]
        public string? Summary { get; set; }

        [DisplayName("Ảnh đại diện")]
        public string? Avatar { get; set; }

        [DisplayName("Ảnh đại diện")]
        public IFormFile? AvatarFile { get; set; }

        [DisplayName("Danh mục")]
        public long CategoryId { get; set; }
    }
}
