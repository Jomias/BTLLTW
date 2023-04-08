using System.ComponentModel.DataAnnotations;

namespace QLNH.Models
{
    public class PositionModel : BaseModel
    {
        public long? Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}
