using System;
using System.Collections.Generic;

namespace QLNH.Entities;

public partial class Order
{
    public long Id { get; set; }

    public long BillId { get; set; }

    public long DishId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public string? Note { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Bill Bill { get; set; } = null!;

    public virtual Dish Dish { get; set; } = null!;
}
