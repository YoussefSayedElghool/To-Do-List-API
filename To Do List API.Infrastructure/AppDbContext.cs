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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

           
            builder.Entity<ToDo>()
                    .HasOne<User>(t => (User)t.User)
                    .WithMany(u => u.ToDos)
                    .HasForeignKey(t => t.UserId);

            builder.Entity<RefreshToken>()
                .HasOne<User>(r => (User)r.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(r => r.UserId);
        }


    }
}
