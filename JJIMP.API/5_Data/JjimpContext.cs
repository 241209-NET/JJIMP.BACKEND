using Microsoft.EntityFrameworkCore;
using JJIMP.API.Model;
using Microsoft.EntityFrameworkCore.Metadata;

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
        // Configure relations
        // Many-to-many relationship between User and Project
        builder.Entity<Project>()
            .HasMany(p => p.Users)
            .WithMany(u => u.Projects)
            .UsingEntity<UserProject>(
                p => p.HasOne<User>().WithMany().HasForeignKey(up => up.UserId)
                    .OnDelete(DeleteBehavior.NoAction),
                u => u.HasOne<Project>().WithMany().HasForeignKey(up => up.ProjectId).OnDelete(DeleteBehavior.NoAction)
            );
        // One-to-many relationship between Project and Issue
        builder.Entity<Project>()
            .HasMany(p => p.Issues)
            .WithOne(i => i.Project)
            .HasForeignKey(i => i.ProjectId);
        // One-to-many relationship between Issue and Comment
        builder.Entity<Issue>()
            .HasMany(i => i.Comments)
            .WithOne(c => c.Issue)
            .HasForeignKey(c => c.IssueId);
        // One-to-many relationship between User and Issue
        builder.Entity<Issue>()
            .HasOne(i => i.CreatedBy)
            .WithMany(u => u.CreatedIssues)
            .HasForeignKey(i => i.CreatedById)
            .OnDelete(DeleteBehavior.NoAction);
        // One-to-many relationship between User and Issue
        builder.Entity<Issue>()
            .HasOne(i => i.Assignee)
            .WithMany(u => u.AssignedIssues)
            .HasForeignKey(i => i.AssigneeId)
            .OnDelete(DeleteBehavior.NoAction);
        // One-to-many relationship between User and Comment
        builder.Entity<Comment>()
            .HasOne(c => c.PostedBy)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.PostedById)
            .OnDelete(DeleteBehavior.NoAction);
        // One-to-many relationship between User and Project
        builder.Entity<Project>()
            .HasOne(p => p.ProjectManager)
            .WithMany(u => u.ManagedProjects)
            .HasForeignKey(p => p.ProjectManagerId);
        
        // Value conversions
        builder.Entity<Issue>()
            .Property(i => i.Status)
            .HasConversion(
                value => value.ToString(),
                value => (StatusEnum)Enum.Parse(typeof(StatusEnum), value)
            );
    }
}