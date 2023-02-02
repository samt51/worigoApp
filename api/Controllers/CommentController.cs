using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.Comment.Request;
using Worigo.Core.Dtos.Comment.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class CommentController : CustomBaseController
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet("{hotelid}")]
        public ResponseDto<List<CommentResponse>> GetCommentByHotelId([FromHeader] string Authorization, int hotelid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _commentService.commentListJoins(hotelid, keys);

        }
        [HttpGet("{id}")]
        public ResponseDto<CommentResponse> GetById(int id, [FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _commentService.GetById(id, keys);
        }
        [HttpPost]
        public ResponseDto<CommentResponse> Add(CommentAddOrUpdateRequest entity, [FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _commentService.Create(entity, keys);
        }
        [HttpPost]
        public ResponseDto<CommentResponse> Update(CommentAddOrUpdateRequest entity, [FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _commentService.Update(entity, keys);
        }
    }
}
