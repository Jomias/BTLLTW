using System;
using System.Collections.Generic;

namespace QLNH.Entities;

public partial class Bill
{
    public long Id { get; set; }

    public decimal? SubTotal { get; set; }

    public int? DiscountPercentage { get; set; }

    public decimal? Total { get; set; }

    public string? Note { get; set; }

    public DateTime? CheckIn { get; set; }

    public DateTime? CheckOut { get; set; }

    public long ReservationId { get; set; }

    public long TableId { get; set; }

    public long? PaymentId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual Payment? Payment { get; set; }

    public virtual Reservation Reservation { get; set; } = null!;

    public virtual Table Table { get; set; } = null!;
}
