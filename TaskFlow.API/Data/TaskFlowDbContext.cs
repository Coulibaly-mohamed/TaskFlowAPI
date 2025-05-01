using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Models;

namespace TaskFlow.API.Data
{
    public class TaskFlowDbContext : DbContext
    {
        public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique email constraint
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Commentaire de tâche : converti en JSON (ou string)
            modelBuilder.Entity<TaskItem>()
                .Property(t => t.Comments)
                .HasConversion(
                    v => string.Join("||", v),
                    v => v.Split("||", System.StringSplitOptions.RemoveEmptyEntries)
                );
        }
    }
}
