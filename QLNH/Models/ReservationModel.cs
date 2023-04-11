using Microsoft.Build.Framework;
using System.ComponentModel;

namespace QLNH.Models
{
    public class ReservationModel : BaseModel
    {
        public long? Id { get; set; }

        [DisplayName("Số người")]
        public int GroupOf { get; set; }

        [DisplayName("Thời gian đặt")]
        public DateTime BookingDate { get; set; }

    }
}
