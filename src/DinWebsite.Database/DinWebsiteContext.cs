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

        public DbSet<Authentication> Authentication { get; set; }
        public DbSet<AddedMovies> AddedMovies { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddedMovies>().ToTable("AddedMovies");
            modelBuilder.Entity<User>()
                .ToTable("User")
                .HasOne(a => a.Auth)
                .WithOne(b => b.User)
                .HasForeignKey<Authentication>(b => b.UserRef);
            modelBuilder.Entity<Authentication>()
                .ToTable("Auth")
                .HasIndex(a => a.Username).IsUnique();

        }
    }
}
