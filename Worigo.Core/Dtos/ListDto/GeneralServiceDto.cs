namespace Worigo.Core.Dtos.ListDto
{
    public class GeneralServiceDto: BaseEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public string ImageUrl { get; set; }
        public bool isActive { get; set; }
    }
}
