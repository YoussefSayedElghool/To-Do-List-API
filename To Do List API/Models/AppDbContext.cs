using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using To_Do_List_API.Models;

namespace To_Do_List_API.Models
{
    public class AppDbContext :IdentityDbContext<User>
    {
            public AppDbContext(DbContextOptions options) : base(options)
            {

            }

            public DbSet<User> Users { get; set; }
            public DbSet<ToDo> ToDos { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
