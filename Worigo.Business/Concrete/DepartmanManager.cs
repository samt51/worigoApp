using AutoMapper;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.Departman.Request;
using Worigo.Core.Dtos.Departman.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Extension;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class DepartmanManager : IDepartmanService
    {
        private readonly IDepartmanDal _departmanDal;
        private readonly IMapper _mapper;
        public DepartmanManager(IDepartmanDal departmanDal, IMapper mapper)
        {
            _departmanDal = departmanDal;
            _mapper = mapper;
        }


        public ResponseDto<DepartmentResponse> Create(DepartmentAddOrUpdateRequest entity, TokenKeys keys)
        {
            if (keys.role >= 1 && keys.role <= 3)
            {
                entity.ImageUrl = FileToByteConvert.FromFileToByte(entity.file);
                var data = _departmanDal.Create(_mapper.Map<Departman>(entity));
                return new ResponseDto<DepartmentResponse>().Success(_mapper.Map<DepartmentResponse>(data), 200);
            }
            return new ResponseDto<DepartmentResponse>().Authorization();
        }

        
        public ResponseDto<List<DepartmentResponse>> GetAll(TokenKeys keys)
        {
            if (keys.role >= 1 && keys.role <= 3)
            {
                var getall = _departmanDal.GetAll();
                var mapping = _mapper.Map<List<DepartmentResponse>>(getall);
                return new ResponseDto<List<DepartmentResponse>>().Success(mapping, 200);
            }
            return new ResponseDto<List<DepartmentResponse>>().Authorization();

        }
        public ResponseDto<DepartmentResponse> GetById(int id, TokenKeys keys)
        {
            if (keys.role >= 1 && keys.role <= 3)
            {
                var data = _mapper.Map<DepartmentResponse>(_departmanDal.GetById(id));
                return new ResponseDto<DepartmentResponse>().Success(data, 200);
            }
            return new ResponseDto<DepartmentResponse>().Authorization();
        }

  

        public ResponseDto<DepartmentResponse> Update(DepartmentAddOrUpdateRequest entity, TokenKeys keys)
        {
            if (keys.role >= 1 && keys.role <= 3)
            {
                var data = GetById(entity.Id, keys);
                data.data.DepartmanName = entity.DepartmanName;
                var update = _departmanDal.Update(_mapper.Map<Departman>(data.data));
                var response = _mapper.Map<DepartmentResponse>(update);
                return new ResponseDto<DepartmentResponse>().Success(response, 200);
            }
            return new ResponseDto<DepartmentResponse>().Authorization();
        }

      
        ResponseDto<DepartmentCommentRateResponse> IDepartmanService.DepartmanCommentRateResponse(int hotelid, int departmanid, TokenKeys keys)
        {
            return new ResponseDto<DepartmentCommentRateResponse>().Success(_departmanDal.DepartmanCommentRateResponse(hotelid, departmanid), 200);
        }
    }
}
