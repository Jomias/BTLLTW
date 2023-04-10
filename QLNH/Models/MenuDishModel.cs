using System.ComponentModel;

namespace QLNH.Models
{
    public class MenuDishModel : BaseModel
    {
        public long? Id { get; set; }

        [DisplayName("Mã thực đơn")]
        public long MenuId { get; set; }
        [DisplayName("Mã món ăn")]
        public long DishId { get; set; }

    }
}
