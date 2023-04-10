using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QLNH.Models.ViewModels
{
	public class MenuDishViewModel
	{
		public long Id { get; set; }

        public string Dish { get; set; } = null!;

	}
}
