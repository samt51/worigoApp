namespace Worigo.Core.Dtos.FoodMenu.Request
{
    public class AddNewMenuRequest
    {
        public int? Id { get; set; } 
        public string categoryName { get; set; }
        public int hotelid { get; set; }
    }
}
