using System.ComponentModel;

namespace QLNH.Models
{
    public class PaymentModel
    {
        public long Id { get; set; }
        [DisplayName("Phương thức thanh toán")]
        public string PaymentType { get; set; } = null!;
    }
}
