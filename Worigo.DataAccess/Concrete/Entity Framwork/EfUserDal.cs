using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.JoinClass;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfUserDal : EfRepositoryDal<User, DataContext>, IUserDal
    {
        public List<UserAndUserRoleJoin> GetAllJoin()
        {
            using (var db = new DataContext())
            {
                var joinsorgu = from d1 in db.Users.Where(x=>x.isDeleted==false)
                                join d3 in db.Hotel on d1.hotelid equals d3.id
                                join d2 in db.UserRole on d1.roleid equals d2.id
                                select new UserAndUserRoleJoin
                                {
                                    id = d1.id,
                                    email = d1.email,
                                    password = d1.password,
                                    role = d2.RoleName, 
                                    hotel = d3.HotelName
                                };
                return joinsorgu.ToList();
            }
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            using (var db = new DataContext())
            {
                return db.Users.Where(x => x.email == email && x.password == password && x.isDeleted == false).FirstOrDefault();
            }
        }
    }
}
