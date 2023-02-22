using AutoMapper;
using TesteHyperativa_Domain.Entities;
using TesteHyperativa_MinimalAPI.ViewModels;

namespace TesteHyperativa_MinimalAPI.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserViewModel, User>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.Name}")
                )
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(src => $"{src.UserName}")
                )
                .ForMember(
                    dest => dest.Password,
                    opt => opt.MapFrom(src => $"{src.Password}")
                )
                .ForMember(
                    dest => dest.UserRole,
                    opt => opt.MapFrom(src => $"{src.UserRole}")
                );


            CreateMap<User, UserViewModel>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.Name}")
                )
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(src => $"{src.UserName}")
                )
                .ForMember(
                    dest => dest.Password,
                    opt => opt.MapFrom(src => $"{src.Password}")
                )
                .ForMember(
                    dest => dest.UserRole,
                    opt => opt.MapFrom(src => $"{src.UserRole}")
                );

        }
    }
}