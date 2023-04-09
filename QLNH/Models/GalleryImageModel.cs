using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLNH.Models
{
    public class GalleryImageModel : BaseModel
    {
        public long? Id { get; set; }
        [DisplayName("Tên ảnh")]
        [MaxLength(50)]
        public string? Name { get; set; } = null!;
        [DisplayName("Đường dẫn ảnh")]
        [MaxLength(100)]
        public string? Path { get; set; }
        [DisplayName("Ảnh")]
        public IFormFile? PathFile { get; set; }
        public long GalleryId { get; set; }
    }
}
