using FinpeApi.Statements;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FinpeApi.Models
{
    public interface IFinpeDbContext
    {
        DbSet<Bank> Banks { get; set; }
        DbSet<BankStatement> BankStatements { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Statement> Statements { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}