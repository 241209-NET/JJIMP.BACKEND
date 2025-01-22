using Microsoft.EntityFrameworkCore;
using JJIMP.API.Model;

namespace JJIMP.API.Data;

public partial class JjimpContext : DbContext
{
    public JjimpContext() { }
    public JjimpContext(DbContextOptions<JjimpContext> options) : base(options) { }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<Issue> Issues { get; set; }
    public virtual DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Project>()
            .Property(u => u.createdAt)
            .HasComputedColumnSql("getutcdate()")
            .ValueGeneratedOnAdd();

        builder.Entity<Project>()
            .Property(u => u.updatedAt)
            .HasComputedColumnSql("getutcdate()")
            .ValueGeneratedOnAddOrUpdate();

        builder.Entity<Issue>()
            .Property(i => i.createdAt)
            .HasComputedColumnSql("getutcdate()")
            .ValueGeneratedOnAdd();

        builder.Entity<Issue>()
            .Property(i => i.updatedAt)
            .HasComputedColumnSql("getutcdate()")
            .ValueGeneratedOnAddOrUpdate();

        builder.Entity<Comment>()
            .Property(c => c.createdAt)
            .HasComputedColumnSql("getutcdate()")
            .ValueGeneratedOnAdd();

        builder.Entity<Comment>()
            .Property(c => c.updatedAt)
            .HasComputedColumnSql("getutcdate()")
            .ValueGeneratedOnAddOrUpdate();

    }
}