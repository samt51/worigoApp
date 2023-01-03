using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.DirectorsDepartmans.Request;
using Worigo.Core.Dtos.DirectorsDepartmans.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class DirectorsDepartmansManager : IDirectorsDepartmansService
    {
        private readonly IDirectorsDepartmansDal _directorsDepartmansDal;
        private readonly IHotelDal _hotelDal;
        private readonly IMapper _mapper;
        private readonly IManagementOfHotelsDal _managementOfHotelsDal;
        public DirectorsDepartmansManager(IDirectorsDepartmansDal directorsDepartmansDal, IHotelDal hotelDal, IMapper mapper,
            IManagementOfHotelsDal managementOfHotelsDal)
        {
            _directorsDepartmansDal = directorsDepartmansDal;
            _hotelDal = hotelDal;
            _managementOfHotelsDal = managementOfHotelsDal;
            _mapper = mapper;
        }



        public ResponseDto<UserAndDirectoryResponse> Create(UserAndDirectoryDepartmentAddOrUpdateRequest entity)
        {
            var response = _directorsDepartmansDal.Create(_mapper.Map<DirectorsDepartmans>(entity));
            return new ResponseDto<UserAndDirectoryResponse>().Success(_mapper.Map<UserAndDirectoryResponse>(response), 200);
        }

        public DirectorsDepartmans GetById(int id)
        {
            return _directorsDepartmansDal.GetById(id);
        }

        public DirectorsDepartmans GetDirectoryByHotelIdAndId(int hotelid, int id)
        {
            return _directorsDepartmansDal.GetDirectoryByHotelIdAndId(hotelid, id);
        }

        public ResponseDto<NoContentResult> ToDepartmentManagerRemove(TokenKeys keys, UserAndDirectoryDepartmentAddOrUpdateRequest request)
        {
            var hotel = _hotelDal.GetById(request.hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                _directorsDepartmansDal.ToDepartmentManagerRemove(request);
                return new ResponseDto<NoContentResult>().Success(200);
            }
            else if (keys.role == 3)
            {
                _managementOfHotelsDal.IsthereManagementByDepartmentDirectory(keys.userId, (int)request.directoryEmployeeId);
                _directorsDepartmansDal.ToDepartmentManagerRemove(request);
                return new ResponseDto<NoContentResult>().Success(200);
            }
            return new ResponseDto<NoContentResult>().Authorization();
        }

        public DirectorsDepartmans Update(DirectorsDepartmans entity)
        {
            return _directorsDepartmansDal.Update(entity);
        }

        public ResponseDto<UserAndDirectoryResponse> Update(UserAndDirectoryDepartmentAddOrUpdateRequest entity)
        {
            throw new System.NotImplementedException();
        }

        ResponseDto<UserAndDirectoryResponse> IDirectorsDepartmansService.GetDirectoryByHotelIdAndId(int hotelid, int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
