namespace QLNH.Models.ViewModels
{
    public class OrderViewModel
    {
        public long? Id { get; set; }
        public string DishName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        public string? Note { get; set; }
    }
}
