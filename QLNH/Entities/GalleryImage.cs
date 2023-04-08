using System;
using System.Collections.Generic;

namespace QLNH.Entities;

public partial class GalleryImage
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Path { get; set; } = null!;

    public long GalleryId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Gallery Gallery { get; set; } = null!;
}
