using Microsoft.EntityFrameworkCore;
using Worigo.Entity.Concrete;
namespace Worigo.DataAccess.CodeFirst
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=DESKTOP-3IKQL6T\SQLEXPRESS;Database=WorigoDb;Trusted_Connection=True");
            optionsBuilder.UseSqlServer(@"Server=77.245.159.10\MSSQLSERVER2019;Database=worigo_db;User ID=samet123;Password=1425369As;Trusted_Connection=false;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GeneralServiceAndService>()
            .HasKey(x => new { x.Serviceid, x.GeneralServiceid });
            modelBuilder.Entity<GeneralServiceAndService>()
           .HasOne(x => x.Services)
           .WithMany(x => x.GeneralServiceAndServices)
           .HasForeignKey(x => x.Serviceid);
            modelBuilder.Entity<GeneralServiceAndService>()
           .HasOne(x => x.GeneralService)
           .WithMany(x => x.GeneralServiceAndServices)
           .HasForeignKey(x => x.GeneralServiceid);
            #region



            #endregion
        }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<GeneralService> GeneralService { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Departman> Departman { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<RoomType> RoomType { get; set; }
        public DbSet<AllImages> AllImages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VertificationCodes> vertificationCodes { get; set; }
    }
}
