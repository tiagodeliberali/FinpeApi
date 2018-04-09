using FinpeApi.Statements;
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
                .Property(b => b.Amount).HasColumnType("decimal(11, 2)");

            modelBuilder.Entity<Statement>()
                .Property(b => b.Amount).HasColumnType("decimal(11, 2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
