using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QLNH.Models
{
	public class MenuModel : BaseModel
	{
		public long? Id { get; set; }
		[Required]
		[MaxLength(50)]
		[DisplayName("Tên danh mục nguyên liệu")]
		public string Name { get; set; } = null!;
		[DisplayName("Đường dẫn thân thiện")]
		[RegularExpression("^[a-z0-9]+(?:-[a-z0-9]+)*$", ErrorMessage = "Chỉ có thể là kí tự, số và dấu gạch")]
		public string? Slug { get; set; }
		[DisplayName("Tóm tắt")]
		public string? Summary { get; set; }
	}
}
