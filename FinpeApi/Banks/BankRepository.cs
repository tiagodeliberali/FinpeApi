using FinpeApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinpeApi.Banks
{
    public class BankRepository
    {
        private FinpeDbContext dbContext;

        public BankRepository(FinpeDbContext dbContext) => this.dbContext = dbContext;

        public IReadOnlyList<Bank> GetList() => dbContext.Banks
            .Include(x => x.BankStatements)
            .ToList();

        public async Task SaveStatement(BankStatement statement)
        {
            dbContext.BankStatements.Add(statement);
            await dbContext.SaveChangesAsync();
        }
    }
}
