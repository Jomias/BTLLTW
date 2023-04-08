using System;
using System.Collections.Generic;

namespace QLNH.Entities;

public partial class User
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime? RegisteredAt { get; set; }

    public bool? Gender { get; set; }

    public string? Avatar { get; set; }

    public long RoleId { get; set; }

    public int? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Role Role { get; set; } = null!;
}
