using System.Collections.Generic;
using Worigo.Core.Dtos.ContentsOfMenuDetail.Dto;

namespace Worigo.Core.Dtos.FoodMenuDetailDto.Dto
{
    public class FoodMenuDetailDtoAddOrUpdateRequest
    {
        public int? id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public int foodMenuId { get; set; }
        public List<ContentsOfMenuAddOrUpdateRequest> ContentsOfMenu { get; set; }
    }
}
