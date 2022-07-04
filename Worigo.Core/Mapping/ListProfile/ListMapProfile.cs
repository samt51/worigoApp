using Worigo.Core.Dtos.ListDto;
using Worigo.Entity.Concrete;

namespace Worigo.Core.Mapping.ListProfile
{
    public class ListMapProfile : IProfile
    {
        public ListMapProfile()
        {
            CreateMap<Hotel, HotelDto>().ReverseMap();
            CreateMap<AllImages, AllImagesDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Departman, DepartmanDto>().ReverseMap();
            CreateMap<Employees, EmployeesDto>().ReverseMap();
            CreateMap<GeneralService, GeneralServiceDto>().ReverseMap();
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<Services, ServicesDto>().ReverseMap();
            CreateMap<Employees, EmployeesDto>().ReverseMap();
            CreateMap<RoomType, RoomTypeDto>().ReverseMap();
            CreateMap<UserRole, UserRoleDto>().ReverseMap();
            CreateMap<User, UserListDto>().ReverseMap();

        }
    }
}
