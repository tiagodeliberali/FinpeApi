using Microsoft.EntityFrameworkCore;

namespace FinpeApi.Models
{
    public interface IFinpeDbContext
    {
        DbSet<Bank> Banks { get; set; }
        DbSet<BankStatement> BankStatements { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Statement> Statements { get; set; }
    }
}