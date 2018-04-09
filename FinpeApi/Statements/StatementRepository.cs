using FinpeApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FinpeApi.Statements
{
    public class StatementRepository : IStatementRepository
    {
        private IFinpeDbContext dbContext;

        public StatementRepository(IFinpeDbContext dbContext) => this.dbContext = dbContext;

        public async Task<Statement> Get(int id)
        {
            return await dbContext.Statements.FindAsync(id);
        }

        public async Task Save(Statement statement)
        {
            if (statement.Id == 0)
                await Insert(statement);

            await dbContext.SaveChangesAsync();
        }

        private async Task Insert(Statement statement)
        {
            Category category = await GetCategory(statement.Category.Name);
            statement.Category.Id = category.Id;
            await dbContext.Statements.AddAsync(statement);
        }

        public async Task<Category> GetCategory(string categoryName)
        {
            var category = await dbContext.Categories.FirstOrDefaultAsync(x => x.Name == categoryName);

            if (category == null)
            {
                category = Category.Create(categoryName);
                await dbContext.Categories.AddAsync(category);
            }

            return category;
        }
    }
}
