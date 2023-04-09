namespace QLNH.Models.ViewModels
{
    public class GalleryImageViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Path { get; set; } = null!;
        public string Gallery { get; set; } = null!;
        public string? Slug { get; set; } = null!;
    }
}
