using Worigo.Core.Dtos.Departman.Request;
using Worigo.Core.Dtos.Departman.Response;
using Worigo.Core.Dtos.Employee.Request;
using Worigo.Core.Dtos.Employee.Response;
using Worigo.Core.Dtos.EmployeeType.Request;
using Worigo.Core.Dtos.FoodMenu.Request;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ManagerDto.Request;
using Worigo.Core.Dtos.UserRole.Request;
using Worigo.Core.FluentValidation;
using Worigo.Entity.Concrete;

namespace Worigo.Core.Mapping.ListProfile
{
    public class ListMapProfile : IProfile
    {
        public ListMapProfile()
        {
            CreateMap<Hotel, HotelDto>().ReverseMap();
            CreateMap<Companies, CompaniesDto>().ReverseMap();
            CreateMap<AllImages, AllImagesDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Companies, CommentDto>().ReverseMap();
            CreateMap<Departman, DepartmentAddOrUpdateRequest>().ReverseMap();
            CreateMap<Employees, EmployeeResponse>().ReverseMap();
            CreateMap<ServicesValues, ServiceValueDto>().ReverseMap();
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<Services, ServicesDto>().ReverseMap();
            CreateMap<RoomType, RoomTypeDto>().ReverseMap();
            CreateMap<UserRole, UserRoleRequest>().ReverseMap();
            CreateMap<User, UserListDto>().ReverseMap();
            CreateMap<EmployeesType, EmployeeTypeAddOrUpdateRequest>().ReverseMap();
            CreateMap<Employees, EmployeesAndUserAddOrUpdateDto>().ReverseMap();
            CreateMap<User, AddHotelAdminModelDto>().ReverseMap();
            CreateMap<User, ManagementAddDto>().ReverseMap();
            CreateMap<Employees, ManagementAddDto>().ReverseMap();
            CreateMap<FoodMenu, AddNewMenuRequest>().ReverseMap();
            CreateMap<Order, OrderAddValidator>().ReverseMap();
            CreateMap<Departman, DepartmentResponse>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
        }
    }
}
