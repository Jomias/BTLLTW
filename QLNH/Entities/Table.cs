using System;
using System.Collections.Generic;

namespace QLNH.Entities;

public partial class Table
{
    public long Id { get; set; }

    public int Number { get; set; }

    public int Capacity { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Bill> Bills { get; } = new List<Bill>();
}
