using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.Departman.Request;
using Worigo.Core.Dtos.Departman.Response;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : CustomBaseController
    {
        private readonly IDepartmanService _departmanService;

        public DepartmentController(IDepartmanService departmanService)
        {
            _departmanService = departmanService;
            



        }
        [HttpGet]
        public ResponseDto<List<DepartmentResponse>> GetAll([FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _departmanService.GetAll(keys);
        }
        [HttpGet("{id}")]
        public ResponseDto<DepartmentResponse> GetById(int id, [FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _departmanService.GetById(id, keys);

        }
        [HttpPost]
        public ResponseDto<DepartmentResponse> Add([FromForm] DepartmentAddOrUpdateRequest entity, [FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _departmanService.Create(entity, keys);
        }
        [HttpPost]
        public ResponseDto<DepartmentResponse> Update([FromForm] DepartmentAddOrUpdateRequest entity, [FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _departmanService.Update(entity, keys);
        }
    }
}
