namespace QLNH.Models
{
    public class OrderModel : BaseModel
    {
        public long? Id { get; set; }

        public long BillId { get; set; }

        public long DishId { get; set; }

        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? Note { get; set; }
    }
}
