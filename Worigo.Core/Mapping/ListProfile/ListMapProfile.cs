using Worigo.Core.Dtos.Comment.Request;
using Worigo.Core.Dtos.Comment.Response;
using Worigo.Core.Dtos.Companies.Request;
using Worigo.Core.Dtos.Companies.Response;
using Worigo.Core.Dtos.Customer.Request;
using Worigo.Core.Dtos.Customer.Response;
using Worigo.Core.Dtos.Departman.Request;
using Worigo.Core.Dtos.Departman.Response;
using Worigo.Core.Dtos.DirectorsDepartmans.Request;
using Worigo.Core.Dtos.Employee.Request;
using Worigo.Core.Dtos.Employee.Response;
using Worigo.Core.Dtos.EmployeeType.Request;
using Worigo.Core.Dtos.EmployeeType.Response;
using Worigo.Core.Dtos.FoodMenu.Request;
using Worigo.Core.Dtos.FoodMenu.Response;
using Worigo.Core.Dtos.FoodMenuDetailDto.Dto;
using Worigo.Core.Dtos.FoodMenuDetailDto.Response;
using Worigo.Core.Dtos.Hotel.Request;
using Worigo.Core.Dtos.Hotel.Response;
using Worigo.Core.Dtos.HotelOfServiceDto.Request;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ManagerDto.Request;
using Worigo.Core.Dtos.RoomType.Request;
using Worigo.Core.Dtos.RoomType.Response;
using Worigo.Core.Dtos.Services.Request;
using Worigo.Core.Dtos.Services.Response;
using Worigo.Core.Dtos.ServicesValue.Request;
using Worigo.Core.Dtos.ServicesValue.Response;
using Worigo.Core.Dtos.User.Dto;
using Worigo.Core.Dtos.User.Request;
using Worigo.Core.Dtos.User.Response;
using Worigo.Core.Dtos.UserRole.Request;
using Worigo.Core.Dtos.UserRole.Response;
using Worigo.Core.Dtos.VerificationCodeDto.Request;
using Worigo.Core.Dtos.VerificationCodeDto.Response;
using Worigo.Core.FluentValidation;
using Worigo.Entity.Concrete;

namespace Worigo.Core.Mapping.ListProfile
{
    public class ListMapProfile : IProfile
    {
        public ListMapProfile()
        {

             //management Map
            CreateMap<ManagementOfHotels, ManagementUserAddOrUpdateRequest>().ReverseMap();
            CreateMap<UserRequest, ManagementUserAddOrUpdateRequest>().ReverseMap();
            CreateMap<User, ManagementUserAddOrUpdateRequest>().ReverseMap();
            CreateMap<Employees, ManagementUserAddOrUpdateRequest>().ReverseMap();
            CreateMap<EmployeeRequest, ManagementUserAddOrUpdateRequest>().ReverseMap();
            CreateMap<DirectorsDepartmans, ManagementUserAddOrUpdateRequest>().ReverseMap();

            CreateMap<Hotel, HotelDto>().ReverseMap();
          
            CreateMap<AllImages, AllImagesDto>().ReverseMap();
            CreateMap<Companies, CommentDto>().ReverseMap();
          
     
           
            CreateMap<Room, RoomDto>().ReverseMap();
           
            CreateMap<RoomType, RoomTypeDto>().ReverseMap();
        
            CreateMap<User, UserListDto>().ReverseMap();
           
        
            CreateMap<User, AddHotelAdminModelDto>().ReverseMap();
            CreateMap<User, ManagementAddDto>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();
            CreateMap<User, UserRequest>().ReverseMap();
            CreateMap<Employees, ManagementAddDto>().ReverseMap();


            CreateMap<VerificationCodes, VerificationCodeRequest>().ReverseMap();
            CreateMap<VerificationCodes, VerificationCodeResponse>().ReverseMap();
          
            CreateMap<Order, OrderAddValidator>().ReverseMap();

            CreateMap<UserRole, UserRoleRequest>().ReverseMap();
            CreateMap<UserRole, UserRoleResponse>().ReverseMap();

            CreateMap<ServicesValues, ServicesValuesAddOrUpdateRequest>().ReverseMap();
            CreateMap<ServicesValues, ServicesValueResponse>().ReverseMap();

            CreateMap<Services, ServicesAddOrUpdateRequest>().ReverseMap();
            CreateMap<Services, ServicesResponse>().ReverseMap();

            CreateMap<RoomType, RoomTypeDto>().ReverseMap();
            CreateMap<RoomType, RoomTypeAddOrUpdateRequest>().ReverseMap();
            CreateMap<RoomType, RoomTypeResponse>().ReverseMap();

            CreateMap<Room, RoomDto>().ReverseMap();
                
            CreateMap<Hotel, HotelAddOrUpdateRequest>().ReverseMap();
            CreateMap<Hotel, HotelResponse>().ReverseMap();

            CreateMap<FoodMenu, FoodMenuRequest>().ReverseMap();
            CreateMap<FoodMenu, FoodMenuResponse>().ReverseMap();


            CreateMap<FoodMenuDetail, FoodMenuDetailDtoAddOrUpdateRequest>().ReverseMap();
            CreateMap<FoodMenuDetail, FoodMenuDetailResponse>().ReverseMap();

            CreateMap<EmployeesType, EmployeeTypeAddOrUpdateRequest>().ReverseMap();
            CreateMap<EmployeesType, EmployeeTypeResponse>().ReverseMap();

            CreateMap<Employees, EmployeeResponse>().ReverseMap();
            CreateMap<Employees, EmployeeRequest>().ReverseMap();

            CreateMap<DirectorsDepartmans, UserAndDirectoryDepartmentAddOrUpdateRequest>().ReverseMap();
            CreateMap<DirectorsDepartmans, DirectorsDepartmans>().ReverseMap();

            CreateMap<Departman, DepartmentAddOrUpdateRequest>().ReverseMap();
            CreateMap<Departman, DepartmentResponse>().ReverseMap();

            CreateMap<Companies, CompaniesAddOrUpdateRequest>().ReverseMap();
            CreateMap<Companies, CompaniesResponse>().ReverseMap();

            CreateMap<Comment, CommentAddOrUpdateRequest>().ReverseMap();
            CreateMap<Comment, CommentResponse>().ReverseMap();

            CreateMap<Customer, CustomerAddOrUpdate>().ReverseMap();
            CreateMap<Customer, CustomerResponse>().ReverseMap();


            CreateMap<HotelOfService, HotelOfServiceAddOrUpdate>().ReverseMap();

        }
    }
}
