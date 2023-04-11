using System.ComponentModel;

namespace QLNH.Models.ViewModels
{
    public class BillViewModel
    {
        public long Id { get; set; }
        public int SoBan { get; set; }
        public long ReservationId { get; set; }
        public int? Status { get; set; }
    }
}
