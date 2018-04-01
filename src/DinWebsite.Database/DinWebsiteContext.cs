using System;
using System.Collections.Generic;
using System.Text;
using DinWebsite.ExternalModels.Authentication;
using DinWebsite.ExternalModels.Content;
using Microsoft.EntityFrameworkCore;

namespace DinWebsite.Database
{
    public class DinWebsiteContext : DbContext
    {
        public DinWebsiteContext(DbContextOptions<DinWebsiteContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<AddedMovie> AddedMovie { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("User")
                .HasOne(u => u.Account)
                .WithOne(a => a.User)
                .HasForeignKey<Account>(a => a.UserRef);
            modelBuilder.Entity<Account>()
                .HasMany(a => a.AddedMovies)
                .WithOne(am => am.Account);
            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Username)
                .IsUnique();
            modelBuilder.Entity<AddedMovie>().ToTable("AddedMovies");
        }
    }
}
