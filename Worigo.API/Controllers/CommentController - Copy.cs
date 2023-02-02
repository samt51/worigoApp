using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.Comment.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]

    public class CommentController : CustomBaseController
    {
        private readonly ICommentService _commentService;
        private readonly IHotelService _hotelService;
        private readonly IManagementOfHotelService _managementOfHotelService;
        private readonly IMapper _mapper;
        public CommentController(IHotelService hotelService, IManagementOfHotelService managementOfHotelService, ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _managementOfHotelService = managementOfHotelService;
            _hotelService = hotelService;
            _mapper = mapper;
        }
        [HttpGet("{hotelid}")]
        public IActionResult GetCommentByHotelId([FromHeader] string Authorization, int hotelid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var hotel = _hotelService.GetById(keys, hotelid);
            if ((keys.companyid == hotel.Companyid) && keys.role == 2 || keys.role == 1)
            {
                var listcomment = _commentService.commentListJoins(hotelid);
                return CreateActionResult(ResponseDto<List<CommentResponse>>.Success(listcomment, 200));
            }
            else if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                var listcomment = _commentService.commentListJoins(hotelid);
                return CreateActionResult(ResponseDto<List<CommentResponse>>.Success(listcomment, 200));
            }
            return CreateActionResult(ResponseDto<List<Companies>>.Authorization());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id, [FromHeader] string Authorization)
        {
            //TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var commentSingularData = _commentService.GetByIdJoin(id);
            //if ((keys.hotelid == commentSingularData.hotelid && keys.role <= 3) || keys.role == 1)
            //{
            return CreateActionResult(ResponseDto<CommentResponse>.Success(commentSingularData, 200));
        }
        [HttpPost]
        public IActionResult Add(CommentDto entity)
        {
            var mapperData = _mapper.Map<Comment>(entity);
            _commentService.Create(mapperData);
            return CreateActionResult(ResponseDto<Comment>.Success(200));
        }
        [HttpPost("{id}")]
        public IActionResult Delete(int id)
        {
            var commentSingularData = _commentService.GetById(id);
            commentSingularData.isDeleted = true;
            return CreateActionResult(ResponseDto<Comment>.Success(200));
        }
        [HttpPost]
        public IActionResult Update(CommentDto entity)
        {
            var commentSingularData = _commentService.GetById(entity.Id);
            commentSingularData.ModifyDate = System.DateTime.Now;
            commentSingularData.Point = entity.Point;
            commentSingularData.Commentary = entity.Commentary;
            _commentService.Update(commentSingularData);
            return CreateActionResult(ResponseDto<Comment>.Success(200));
        }
    }
}
