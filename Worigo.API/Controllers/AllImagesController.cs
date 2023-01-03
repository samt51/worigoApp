using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    //public class AllImagesController : CustomBaseController
    //{
    //    private IAllImagesService _allImagesService;
    //    private readonly IMapper _mapper;
    //    public AllImagesController(IMapper mapper, IAllImagesService allImagesService)
    //    {
    //        _allImagesService = allImagesService;
    //        _mapper = mapper;

    //    }
    //    [HttpGet]
    //    public IActionResult GetAll()
    //    {
    //        var imagesListData = _allImagesService.GetAll();
    //        var imageListDto = _mapper.Map<List<AllImagesDto>>(imagesListData);
    //        return CreateActionResult(ResponseDto<List<AllImagesDto>>.Success(imageListDto,200));
    //    }
    //    [HttpGet("{id}")]
    //    public IActionResult GetById(int id)
    //    {
    //        var allimagesSingularData = _allImagesService.GetById(id);
    //        var allimagesSingularDto = _mapper.Map<AllImagesDto>(allimagesSingularData);
    //        return CreateActionResult(ResponseDto<AllImagesDto>.Success(allimagesSingularDto, 200));
    //    }
    //    [HttpPost]
    //    public IActionResult Add(AllImagesDto entity)
    //    {
    //        var entitydto = new AllImages
    //        {
    //            CreatedDate=System.DateTime.Now,
    //            ModifyDate=System.DateTime.Now,
    //            isDeleted=false,
    //            ImageUrl=entity.ImageUrl,
    //            isActive=entity.isActive
    //        };
    //        _allImagesService.Create(_mapper.Map<AllImages>(entitydto));
    //        return CreateActionResult(ResponseDto<AllImages>.Success(200));
    //    }
    //    [HttpPost("{id}")]
    //    public IActionResult Delete(int id)
    //    {
    //        var allimagesSingularData = _allImagesService.GetById(id);
    //        allimagesSingularData.isDeleted = true;
    //        _allImagesService.Update(allimagesSingularData);
    //        return CreateActionResult(ResponseDto<AllImages>.Success(200));
    //    }
    //    [HttpPost]
    //    public IActionResult Update(AllImagesDto entity)
    //    {
    //        var allimagesSingularData = _allImagesService.GetById(entity.Id);
    //        allimagesSingularData.ModifyDate = System.DateTime.Now;
    //        allimagesSingularData.ImageUrl = entity.ImageUrl;
    //        _allImagesService.Update(allimagesSingularData);
    //        return CreateActionResult(ResponseDto<AllImages>.Success(200));
    //    }
    //}
}
