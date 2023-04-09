using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QLNH.Models.ViewModels
{
	public class IngredientViewModel
	{
		public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Slug { get; set; } = null!;

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        public string Unit { get; set; } = null!;

        public string? Summary { get; set; }

        public string? Avatar { get; set; }

        public string Category { get; set; } = null!;
	}
}
