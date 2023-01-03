using System.Collections.Generic;
using Worigo.Core.Dtos.ContentsOfMenuDetail.Response;

namespace Worigo.Core.Dtos.FoodMenuDetailDto.Response
{
    public class FoodMenuDetailResponse
    {
        public int id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int FoodMenuId { get; set; }
        public string MenuName { get; set; }
        public List<ContentsOfMenuResponse> ContentOfMenu{ get; set; }
    }
}
