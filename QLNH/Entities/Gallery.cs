using System;
using System.Collections.Generic;

namespace QLNH.Entities;

public partial class Gallery
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<GalleryImage> GalleryImages { get; } = new List<GalleryImage>();
}
