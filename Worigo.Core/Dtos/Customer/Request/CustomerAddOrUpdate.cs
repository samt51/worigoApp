namespace Worigo.Core.Dtos.Customer.Request
{
    public class CustomerAddOrUpdate
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Code { get; set; }
        public string lng { get; set; }
        public string DeviceId { get; set; }
        public int VerificationId { get; set; }
    }
}
