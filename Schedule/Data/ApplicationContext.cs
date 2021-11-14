using Schedule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Schedule.Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Schedulee> Schedulee { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<SubGroups> SubGroups { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<EducationalInstitutions> EducationalInstitutions { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Levels> Levels { get; set; }
        public DbSet<Specialization> Specializations{ get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
          //Database.EnsureDeleted();   // удаляем бд со старой схемой
          //Database.EnsureCreated();   // создаем бд с новой схемой
        }
    }
}