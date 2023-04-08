using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLNH.Models
{
    public class GalleryModel : BaseModel
    {
        public long? Id { get; set; }
        [Required]
        [MaxLength(50)]
		[DisplayName("Tên")]
        public string Name { get; set; } = null!;
        [DisplayName("Đường dẫn thân thiện")]
        [RegularExpression("^[a-z0-9]+(?:-[a-z0-9]+)*$", ErrorMessage = "Chỉ có thể là kí tự, số và dấu gạch")]
        public string? Slug { get; set; }
    }
}
