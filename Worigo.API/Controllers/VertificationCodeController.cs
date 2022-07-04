using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Worigo.API.Model;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class VertificationCodeController : CustomBaseController
    {
        public readonly IVertificationCodeService _vertificationCodeService;
        private readonly IMapper _mapper;
        public VertificationCodeController(IMapper mapper, IVertificationCodeService vertificationCodeService)
        {
            _vertificationCodeService = vertificationCodeService;
            _mapper = mapper;
        }
        [HttpPost]
        public IActionResult CodeCreate(VertificationCodeDto vertificationCodeDto)
        {
            var entity = new VertificationCodes
            {

                StartDate = vertificationCodeDto.StartDate,
                FinishDate = vertificationCodeDto.FinishDate,
                CreatedDate=System.DateTime.Now,
                ModifyDate=System.DateTime.Now,
                Code=RandomGeneration.RandomVertificationCodeCreate(4).ToString(),
                isActive=vertificationCodeDto.isActive,
                isDeleted=false,
                roomid=vertificationCodeDto.roomid
            };
            _vertificationCodeService.Create(entity);
            return CreateActionResult(ResponseDto<VertificationCodes>.Success(entity, 200));

        }
        [HttpGet]
        public IActionResult RoomOfCodeGetById(int id)
        {
            var entity = _vertificationCodeService.GetById(id);
            var commentSingularDto = _mapper.Map<VertificationCodeDto>(entity);
            return CreateActionResult(ResponseDto<VertificationCodeDto>.Success(commentSingularDto, 200));
        }
        [HttpPost]
        public IActionResult RoomOfCodeUpdate(VertificationCodeDto vertificationCodeDto)
        {
            var entity = _vertificationCodeService.GetById(vertificationCodeDto.id);
            entity.StartDate = vertificationCodeDto.StartDate;
            entity.ModifyDate = vertificationCodeDto.ModifyDate;
            entity.Code = RandomGeneration.RandomVertificationCodeCreate(4).ToString();
            _vertificationCodeService.Update(entity);
            return CreateActionResult(ResponseDto<VertificationCodes>.Success(entity, 200));
        }
    }
}
