using AutoMapper;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.UserRole.Request;
using Worigo.Core.Dtos.UserRole.Response;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class UserRoleManager : IUserRoleService
    {
        private readonly IUserRoleDal _userRoleDal;
        private readonly IMapper _mapper;
        public UserRoleManager(IUserRoleDal userRoleDal, IMapper mapper)
        {
            _userRoleDal = userRoleDal;
            _mapper = mapper;
        }

        public ResponseDto<UserRoleResponse> Create(UserRoleRequest entity, TokenKeys keys)
        {
            if (keys.role == 1)
            {
                var create = _userRoleDal.Create(_mapper.Map<UserRole>(entity));
                var mapResponse = _mapper.Map<UserRoleResponse>(create);
                return new ResponseDto<UserRoleResponse>().Success(mapResponse, 200);
            }
            return new ResponseDto<UserRoleResponse>().Authorization();
        }
       

        public ResponseDto<List<UserRoleResponse>> GetAll(TokenKeys keys)
        {
            if (keys.role == 1)
            {
                var response = _userRoleDal.ForHotelsListUserRole(true);
                return new ResponseDto<List<UserRoleResponse>>().Success(_mapper.Map<List<UserRoleResponse>>(response), 200);
            }
            else if (keys.role == 2 && keys.role == 3)
            {
                var response = _userRoleDal.ForHotelsListUserRole(false);
                return new ResponseDto<List<UserRoleResponse>>().Success(_mapper.Map<List<UserRoleResponse>>(response), 200);
            }
            return new ResponseDto<List<UserRoleResponse>>().Authorization();
        }

        public ResponseDto<UserRoleResponse> GetById(int id, TokenKeys keys)
        {
            if (keys.role == 1)
            {
                var map = _mapper.Map<UserRole>(_userRoleDal.GetById(id));
                return new ResponseDto<UserRoleResponse>().Success(_mapper.Map<UserRoleResponse>(map), 200);
            }
            return new ResponseDto<UserRoleResponse>().Authorization();
        }

        public ResponseDto<UserRoleResponse> Update(UserRoleRequest entity, TokenKeys keys)
        {
            if (keys.role == 1)
            {
                var data = GetById(entity.id, keys);

                data.data.RoleName = entity.RoleName;
                var response = _userRoleDal.Update(_mapper.Map<UserRole>(data));
                return new ResponseDto<UserRoleResponse>().Success(_mapper.Map<UserRoleResponse>(response), 200);
            }
            return new ResponseDto<UserRoleResponse>().Authorization();
        }
    }
}
