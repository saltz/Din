using Din.ExternalModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace Din.Data
{
    public class DinContext : DbContext
    {
        public DinContext(DbContextOptions<DinContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<AddedContent> AddedContent { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("User")
                .HasOne(u => u.Account)
                .WithOne(a => a.User)
                .HasForeignKey<Account>(a => a.UserRef);
            modelBuilder.Entity<Account>()
                .HasMany(a => a.AddedContent)
                .WithOne(am => am.Account);
            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Username)
                .IsUnique();
            modelBuilder.Entity<AddedContent>().ToTable("AddedContent");
        }
    }
}
