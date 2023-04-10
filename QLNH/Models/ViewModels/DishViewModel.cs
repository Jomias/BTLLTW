namespace QLNH.Models.ViewModels
{
    public class DishViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Slug { get; set; } = null!;

        public decimal Price { get; set; }

        public string Unit { get; set; } = null!;

        public string? Summary { get; set; }

        public string? Content { get; set; }

        public string? Instructions { get; set; }

        public string? Avatar { get; set; }

    }
}
