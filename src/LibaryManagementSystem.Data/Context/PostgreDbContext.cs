using LibaryManagementSystem.Core.Models.Entities;
using LibaryManagementSystem.Data.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace LibaryManagementSystem.Data.Context
{
    public class PostgreDbContext : DbContext, IDbContext
    {
        public PostgreDbContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        public DbSet<Author> Authhors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(p => p.Author)
                .WithMany()
                .HasForeignKey(p => p.AuthorId);
        }

    }
}
