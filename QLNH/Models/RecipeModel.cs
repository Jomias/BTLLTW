using Microsoft.Build.Framework;
using System.ComponentModel;

namespace QLNH.Models
{
    public class RecipeModel : BaseModel
    {
        public long? Id { get; set; }

        [Required]
        [DisplayName("Mã món ăn")]
        public long DishId { get; set; }

        [Required]
        [DisplayName("Mã nguyên liệu")]
        public long IngredientId { get; set; }

        [Required]
        [DisplayName("Số lượng")]
        public int Quantity { get; set; }

        [DisplayName("Hướng dẫn")]
        public string? Instructions { get; set; }


    }
}
