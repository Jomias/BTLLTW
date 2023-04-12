using System.ComponentModel;

namespace QLNH.Models
{
    public class ReservationDishModel : BaseModel
    {
        public long? Id { get; set; }

        [DisplayName("Mã đặt bàn")]
        public long ReservationId { get; set; }
        [DisplayName("Mã món ăn")]
        public long DishId { get; set; }
        public int? Quantity { get; set; }
    }
}
