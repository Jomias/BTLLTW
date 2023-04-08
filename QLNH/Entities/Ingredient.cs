using System;
using System.Collections.Generic;

namespace QLNH.Entities;

public partial class Ingredient
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public string Unit { get; set; } = null!;

    public string? Summary { get; set; }

    public string? Avatar { get; set; }

    public long CategoryId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Recipe> Recipes { get; } = new List<Recipe>();
}
