using System;
using System.Collections.Generic;
using System.Text;
using _66bitProject.Models;
using _66bitProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace _66bitProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<EmployeeCost> EmployeeCosts { get; set; }
        public DbSet<EmployeeRevenue> EmployeeRevenues { get; set; }
        public DbSet<Overwork> Overworks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Config = configuration;
        }

        public IConfiguration Config;

        public ApplicationDbContext(DbSet<User> users, IConfiguration configuration)
        {
            Users = users;
            Config = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            builder.Entity<Bonus>(entity =>
            {
                entity.ToTable(name: "Bonuses");
            });

            builder.Entity<EmployeeProject>()
                .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

            builder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Employee)
                .WithMany(ep => ep.Projects)
                .HasForeignKey(ep => ep.EmployeeId);

            builder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Project)
                .WithMany(ep => ep.Employees)
                .HasForeignKey(ep => ep.ProjectId);
        }
    }
}
