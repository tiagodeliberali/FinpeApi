using FinpeApi.Banks;
using FinpeApi.Categories;
using FinpeApi.Statements;
using FinpeApi.Utils;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinpeApi.Models
{
    public class FinpeDbContext : IdentityDbContext<AppUser>, IFinpeDbContext
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
                .OwnsOne<MoneyAmount>(b => b.Amount, cb =>
                    cb.Property(c => c.Value)
                    .HasColumnName("Amount")
                    .HasColumnType("decimal(11, 2)"));

            modelBuilder.Entity<Statement>()
                .OwnsOne<MoneyAmount>(b => b.Amount, cb =>
                    cb.Property(c => c.Value)
                    .HasColumnName("Amount")
                    .HasColumnType("decimal(11, 2)"));

            modelBuilder.Entity<Statement>()
                .OwnsOne<StatementDescription>(b => b.Description, cb =>
                    cb.Property(c => c.Value)
                    .HasColumnName("Description"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
