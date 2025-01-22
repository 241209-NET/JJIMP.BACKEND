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
}