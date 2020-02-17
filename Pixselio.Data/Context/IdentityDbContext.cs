using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Pixselio.Data.Context
{
    public class IdentityDbContext : IdentityDbContext<User>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> dbContext) : base(dbContext)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PhotosTag>()
        .HasKey(e => new { e.PhotoId, e.TagId});
            modelBuilder.Entity<PhotosTag>()
             .HasOne(pt => pt.Photo)
             .WithMany(p => p.PhotosTag)
             .HasForeignKey(pt => pt.PhotoId);

            modelBuilder.Entity<PhotosTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PhotosTag)
                .HasForeignKey(pt => pt.TagId);
        }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PhotosTag> PhotosTags{ get; set; }
    }
}
