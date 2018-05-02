using FinpeApi.Models;
using FinpeApi.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinpeApi.Statements
{
    public class StatementRepository
    {
        private FinpeDbContext dbContext;

        public StatementRepository(FinpeDbContext dbContext) => this.dbContext = dbContext;

        public async Task<Statement> Get(int id) => await dbContext.Statements.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IReadOnlyList<Statement>> GetList(MonthYear monthYear)
        {
            var statements = new List<Statement>();

            statements.AddRange(await dbContext.Statements
                .Include(x => x.Category)
                .ToAsyncEnumerable()
                .Where(x => x.DueDate.Year == monthYear.Year && x.DueDate.Month == monthYear.Month)
                .ToList());

            statements.AddRange(await dbContext.Statements
                .Include(x => x.Category)
                .ToAsyncEnumerable()
                .Where(x => !x.Paid && x.DueDate < monthYear.GetFirstDay())
                .ToList());

            return statements;
        }

        public async Task Save(Statement statement)
        {
            dbContext.Categories.Attach(statement.Category);

            if (statement.Id == 0)
                await dbContext.Statements.AddAsync(statement);

            await dbContext.SaveChangesAsync();
        }
    }
}
