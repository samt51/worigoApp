using Microsoft.EntityFrameworkCore;
using Worigo.Entity.Concrete;
namespace Worigo.DataAccess.CodeFirst
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\NETAX;Database=WorigoTestDb;Trusted_Connection=True");
            optionsBuilder.UseSqlServer(@"Server=77.245.159.10\MSSQLSERVER2019;Database=worigo_db;User ID=samet123;Password=1425369As;Trusted_Connection=false;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<ServicesValues> ServicesValues { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Departman> Departman { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<RoomType> RoomType { get; set; }
        public DbSet<AllImages> AllImages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VertificationCodes> vertificationCodes { get; set; }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<EmployeesType> employeesType { get; set; }
        public DbSet<ManagementOfHotels> ManagementOfHotels { get; set; }
        public DbSet<ResetPasswordForCode> ResetPasswordForCodes { get; set; }
        public DbSet<TasksOfEmployees> TasksOfEmployees { get; set; }
        public DbSet<DirectorsDepartmans> DirectorsDepartmans { get; set; }
        public DbSet<FoodMenu> FoodMenu { get; set; }
        public DbSet<FoodMenuDetail> FoodMenuDetail { get; set; }
        public DbSet<ContentsOfFood> ContentsOfFood { get; set; }
        public DbSet<OrderList> OrderList { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<EmployeeOfOrder> EmployeeOfOrder { get; set; }
        public DbSet<WaitingOrders> WaitingOrders { get; set; }
    }
}
