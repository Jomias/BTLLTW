using Microsoft.Build.Framework;
using System.ComponentModel;

namespace QLNH.Models
{
    public class TableModel : BaseModel
    {
        public long? Id { get; set; }

        [DisplayName("Số bàn")]
        [Required]
        public int Number { get; set; }
        [DisplayName("Số người chứa tối đa")]
        [Required]
        public int Capacity { get; set; }

    }
}
