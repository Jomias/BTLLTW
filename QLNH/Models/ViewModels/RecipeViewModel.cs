namespace QLNH.Models.ViewModels
{
    public class RecipeViewModel
    {
        public long Id { get; set; }
        public string Ingredient { get; set; } = null!;
        public int Quantity { get; set; }
        public string? Instructions { get; set; }
    }
}
