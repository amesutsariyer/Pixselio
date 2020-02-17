 
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

 
namespace Pixselio.Data.Context
{
   public class PixselioDbContext : IdentityDbContext<User>
    {
        public PixselioDbContext(DbContextOptions<IdentityDbContext> dbContext) : base(dbContext)
        {

        }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Tag> Tags { get; set; }
      
    }
}
