using System.ComponentModel;

namespace QLNH.Models
{
    public class RoleModel
    {
        public long Id { get; set; }
        [DisplayName("Vai trò")]
        public string Name { get; set; } = null!;
    }
}
