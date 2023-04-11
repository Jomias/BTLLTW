namespace QLNH.Models.ViewModels
{
    public class MenuPageDishViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Summary { get; set; }
        public decimal Price { get; set; }
        public string? Avatar { get; set; }
        public string? Slug { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
