using Microsoft.EntityFrameworkCore;
using Project_GIS_Login.Entidade;

namespace Project_GIS_Login.Context
{
    public class LoginContext : DbContext
    {

        public LoginContext() { }
        public LoginContext(DbContextOptions<LoginContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoless { get; set; }
    }
}
