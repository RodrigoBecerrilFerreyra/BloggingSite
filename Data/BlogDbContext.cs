using Microsoft.EntityFrameworkCore;
using BlogSite.Models;

namespace BlogSite.Data;

public class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options)
        : base(options)
    {
    }

    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // --- BlogPost ---
        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Title)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(p => p.Slug)
                .HasMaxLength(200);

            // Unique only when Slug is not null, so multiple posts
            // can have no slug and still be reached by Id.
            entity.HasIndex(p => p.Slug)
                .IsUnique()
                .HasFilter("Slug IS NOT NULL");

            entity.Property(p => p.Content)
                .IsRequired();

            // One category -> many posts
            entity.HasOne(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Implicit many-to-many with Tag.
            // EF Core creates a BlogPostTag join table for you.
            entity.HasMany(p => p.Tags)
                .WithMany(t => t.Posts);
        });

        // --- Tag ---
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Name)
                .HasMaxLength(50)
                .IsRequired();

            entity.HasIndex(t => t.Name)
                .IsUnique();
        });

        // --- Category ---
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name)
                .HasMaxLength(50)
                .IsRequired();

            entity.HasIndex(c => c.Name)
                .IsUnique();
        });
    }
}
