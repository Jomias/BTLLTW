using System.ComponentModel;

namespace QLNH.Models
{
    public class BillModel : BaseModel
    {
        [DisplayName("Mã hóa đơn")]
        public long? Id { get; set; }
        [DisplayName("Tổng tiền món ăn")]
        public decimal? SubTotal { get; set; }
        [DisplayName("Phần trăm giảm giá")]
        public int? DiscountPercentage { get; set; }
        [DisplayName("Tổng cộng")]
        public decimal? Total { get; set; }
        [DisplayName("Ghi chú")]
        public string? Note { get; set; }
        [DisplayName("Thời gian Check In")]
        public DateTime? CheckIn { get; set; }
        [DisplayName("Thời gian Thanh toán")]
        public DateTime? CheckOut { get; set; }
        [DisplayName("Mã đặt bàn")]
        public long ReservationId { get; set; }
        [DisplayName("Mã bàn")]
        public long TableId { get; set; }
        [DisplayName("Mã phương thức thanh toán")]
        public long? PaymentId { get; set; }
    }
}
