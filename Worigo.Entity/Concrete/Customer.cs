using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class Customer:IBaseEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public int VerificationId { get; set; }
    }
}
