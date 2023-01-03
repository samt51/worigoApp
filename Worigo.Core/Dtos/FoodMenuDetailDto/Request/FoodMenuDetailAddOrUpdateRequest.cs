namespace Worigo.Core.Dtos.FoodMenuDetailDto.Request
{
    public class FoodMenuDetailAddOrUpdateRequest
    {
        public int? id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public int foodMenuId { get; set; }
    }
}
