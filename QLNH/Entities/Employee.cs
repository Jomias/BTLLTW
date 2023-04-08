using System;
using System.Collections.Generic;

namespace QLNH.Entities;

public partial class Employee
{
    public long Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Avatar { get; set; }

    public long PositionId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Position Position { get; set; } = null!;
}
