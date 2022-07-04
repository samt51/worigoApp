using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.API.Model.UserViewModel;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Enum;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CommentController : CustomBaseController
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll([FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var commentList = _commentService.GetAll();
            var commentListDto = _mapper.Map<List<CommentDto>>(commentList);
            return CreateActionResult(ResponseDto<List<CommentDto>>.Success(commentListDto, 200));

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var commentSingularData = _commentService.GetById(id);
            var commentSingularDto = _mapper.Map<CommentDto>(commentSingularData);
            return CreateActionResult(ResponseDto<CommentDto>.Success(commentSingularDto, 200));
        }
        [HttpPost]
        public IActionResult Add(CommentDto entity)
        {
            var entitydto = new Comment
            {
                isDeleted=false,
                CreatedDate=System.DateTime.Now,
                ModifyDate=System.DateTime.Now,
                employeesid=entity.employeesid,
                Point=entity.Point,
                Commentary=entity.Commentary,
                isActive=entity.isActive
            };
            _commentService.Create(_mapper.Map<Comment>(entitydto));
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
            commentSingularData.isActive = entity.isActive;
            _commentService.Update(commentSingularData);
            return CreateActionResult(ResponseDto<Comment>.Success(200));
        }
    }
}
