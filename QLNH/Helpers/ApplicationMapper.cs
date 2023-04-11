using AutoMapper;
using QLNH.Entities;
using QLNH.Models;

namespace QLNH.Helpers
{
	public class ApplicationMapper : Profile
	{
        public ApplicationMapper()
        {
            CreateMap<Contact, ContactModel>().ReverseMap();
            CreateMap<Position, PositionModel>().ReverseMap();
            CreateMap<Dish, DishModel>().ReverseMap();
			CreateMap<Employee, EmployeeModel>().ReverseMap();
            CreateMap<Gallery, GalleryModel>().ReverseMap();
            CreateMap<GalleryImage, GalleryImageModel>().ReverseMap();
			CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<Ingredient, IngredientModel>().ReverseMap();
            CreateMap<Menu, MenuModel>().ReverseMap();
            CreateMap<MenuDish, MenuDishModel>().ReverseMap();
            CreateMap<Recipe, RecipeModel>().ReverseMap();
            CreateMap<Table, TableModel>().ReverseMap();
            CreateMap<Reservation, ReservationModel>().ReverseMap();
            CreateMap<Payment, PaymentModel>().ReverseMap();
            CreateMap<Role, RoleModel>().ReverseMap();
            CreateMap<Bill, BillModel>().ReverseMap();
            CreateMap<Order, OrderModel>().ReverseMap();

        }
    }
}
