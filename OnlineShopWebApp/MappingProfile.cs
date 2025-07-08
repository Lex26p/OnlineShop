using AutoMapper;
using OnlineShopWebApp.Models;
using Ozon.Db.Models;

namespace OnlineShopWebApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EditUserVM, User>();
            CreateMap<NewUserVM, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<User, UserRolesVM>();
            CreateMap<Product, ProductVM>().ReverseMap();
            CreateMap<OrderInputVM, Order>().ReverseMap();
        } 
    }
}