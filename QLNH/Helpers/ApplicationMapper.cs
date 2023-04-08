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


        }
    }
}
