using FinpeApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinpeApi.Statements
{
    public class StatementRepository : IStatementRepository
    {
        private IFinpeDbContext dbContext;

        public StatementRepository(IFinpeDbContext dbContext) => this.dbContext = dbContext;

        public async Task<Statement> Get(int id) => await dbContext.Statements.FindAsync(id);

        public async Task<IReadOnlyList<Statement>> GetList(int year, int month) => await dbContext.Statements
                .Include(x => x.Category)
                .ToAsyncEnumerable()
                .Where(x => x.DueDate.Year == year && x.DueDate.Month == month)
                .ToList();

        public async Task Save(Statement statement)
        {
            if (statement.Id == 0)
                await dbContext.Statements.AddAsync(statement);

            await dbContext.SaveChangesAsync();
        }
    }
}
