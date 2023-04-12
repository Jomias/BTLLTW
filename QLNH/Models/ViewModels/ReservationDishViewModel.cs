using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QLNH.Models.ViewModels
{
	public class ReservationDishViewModel
	{
		public long Id { get; set; }

        public string Dish { get; set; } = null!;
		public int? Quantity { get; set; }

	}
}
