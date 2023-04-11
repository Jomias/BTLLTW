using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLNH.Models
{
    public class UserModel
    {
        [DisplayName("Tên tài khoản")]
        public string Username { get; set; } = null!;
        [DisplayName("Mật khẩu")]
        public string Password { get; set; } = null!;
        [DisplayName("Tên")]
        public string? FirstName { get; set; }
        [DisplayName("Họ")]
        public string? LastName { get; set; }
        [EmailAddress]
        [DisplayName("Đại chỉ Email")]
        public string Email { get; set; } = null!;
        [EmailAddress]
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; } = null!;
        [DisplayName("Thời gian đăng ký")]
        public DateTime? RegisteredAt { get; set; }
        [DisplayName("Giới tính")]
        public bool? Gender { get; set; }
        [DisplayName("Ảnh đại diện")]
        public string? Avatar { get; set; }
        [DisplayName("Mã vai trò")]
        public long RoleId { get; set; }
        public int? Status { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
