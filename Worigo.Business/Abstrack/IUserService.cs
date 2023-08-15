using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Core.Dtos.DirectorsDepartmans.Request;
using Worigo.Core.Dtos.DirectorsDepartmans.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ManagerDto.Response;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.User.Dto;
using Worigo.Core.Dtos.User.Request;
using Worigo.Core.Dtos.User.Response;

namespace Worigo.Business.Abstrack
{
    public interface IUserService
    {

        ResponseDto<UserResponse> GetUserByEmail(string email, TokenKeys keys);
     
        ResponseDto<List<ManagementUserResponse>> GetManagemetByHotelid(TokenKeys token, int hotelid);
        ResponseDto<List<UserResponse>> GetAllJoin(TokenKeys keys);

        string ProduceToken(string id, string email, string role, string companyid, string deviceid,string lng);
        ResponseDto<UserResponse> GetUserByEmailAndPassword(string email, string password);
        ResponseDto<List<UserResponse>> GetUserByHotelid(int hotelid, TokenKeys keys);

        ResponseDto<List<ManagementUserResponse>> GetHotelAdminByCompaniesid(int companiesid);
        ResponseDto<ManagementUserResponse> GetHotelAdminByAdminUserId(int userid, TokenKeys keys);
 

        ResponseDto<NoContentResult> DirectoryUpdate(TokenKeys keys, UserAndDirectoryDepartmentAddOrUpdateRequest request);
        ResponseDto<UserAndDirectoryResponse> GetDirectoryByDirectoryUserId(TokenKeys keys, int directoryEmployeeId);


        #region

 
        ResponseDto<UserResponse> GetById(int id, TokenKeys keys);
        ResponseDto<UserResponse> Create(UserRequest entity, TokenKeys keys);
        ResponseDto<UserResponse> Update(UserRequest entity, TokenKeys keys);
        #endregion
    }
}
