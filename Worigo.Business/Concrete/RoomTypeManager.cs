using AutoMapper;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.RoomType.Request;
using Worigo.Core.Dtos.RoomType.Response;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class RoomTypeManager : IRoomTypeService
    {
        private readonly IRoomTypeDal _roomTypeDal;
        private readonly IMapper _mapper;
        public RoomTypeManager(IRoomTypeDal roomTypeDal, IMapper mapper)
        {
            _roomTypeDal = roomTypeDal;
            _mapper = mapper;
        }

        public ResponseDto<RoomTypeResponse> Create(RoomTypeAddOrUpdateRequest entity, TokenKeys keys)
        {
            var mapper = _mapper.Map<RoomType>(entity);
            var add = _roomTypeDal.Create(mapper);
            return new ResponseDto<RoomTypeResponse>().Success(_mapper.Map<RoomTypeResponse>(add), 200);
        }

        public ResponseDto<List<RoomTypeResponse>> GetAll(TokenKeys keys)
        {
            var data = _roomTypeDal.GetAll(x => x.isActive == true && x.isDeleted == false);
            var response = _mapper.Map<List<RoomTypeResponse>>(data);
            return new ResponseDto<List<RoomTypeResponse>>().Success(response, 200);
        }


        public ResponseDto<RoomTypeResponse> GetById(int id, TokenKeys keys)
        {
            var data = _roomTypeDal.GetById(id);
            var response = _mapper.Map<RoomTypeResponse>(data);
            return new ResponseDto<RoomTypeResponse>().Success(response, 200);
        }

     
        public ResponseDto<RoomTypeResponse> Update(RoomTypeAddOrUpdateRequest entity, TokenKeys keys)
        {
            var data = _roomTypeDal.GetById(entity.id);
            data.typeName = entity.typeName;
            data.ModifyDate = System.DateTime.Now;
            var update = _roomTypeDal.Update(_mapper.Map<RoomType>(entity));
            var response = _mapper.Map<RoomTypeResponse>(update);
            return new ResponseDto<RoomTypeResponse>().Success(response, 200);
        }

       
    }
}
