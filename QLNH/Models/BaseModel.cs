namespace QLNH.Models
{
	public class BaseModel
	{

        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }

		public string? UpdatedBy { get; set; }

		public DateTime? UpdatedAt { get; set; }

		public int? Status { get; set; }

		public bool? IsDeleted { get; set; }
	}
}
