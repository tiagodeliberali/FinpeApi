﻿using FinpeApi.Banks;
using FinpeApi.Categories;
using FinpeApi.Statements;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinpeApi.Models
{
    public class FinpeDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankStatement> BankStatements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Statement> Statements { get; set; }

        public FinpeDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankStatement>()
                .OwnsOne(b => b.Amount, cb =>
                    cb.Property(c => c.Value)
                    .HasColumnName("Amount")
                    .HasColumnType("decimal(11, 2)"));

            modelBuilder.Entity<Statement>()
                .OwnsOne(b => b.Amount, cb =>
                    cb.Property(c => c.Value)
                    .HasColumnName("Amount")
                    .HasColumnType("decimal(11, 2)"));

            modelBuilder.Entity<Statement>()
                .OwnsOne(b => b.Description, cb =>
                    cb.Property(c => c.Value)
                    .HasColumnName("Description"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
