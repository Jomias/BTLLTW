using System;
using System.Collections.Generic;

namespace QLNH.Entities;

public partial class Reservation
{
    public long Id { get; set; }

    public int GroupOf { get; set; }

    public DateTime BookingDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Bill> Bills { get; } = new List<Bill>();
}
