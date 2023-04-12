using System;
using System.Collections.Generic;

namespace QLNH.Entities;

public partial class ReservationDish
{
    public long Id { get; set; }

    public long DishId { get; set; }

    public long ReservationId { get; set; }

    public int? Quantity { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Dish Dish { get; set; } = null!;

    public virtual Reservation Reservation { get; set; } = null!;
}
