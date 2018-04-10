using FinpeApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinpeApi.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private IFinpeDbContext dbContext;

        public CategoryRepository(IFinpeDbContext dbContext) => this.dbContext = dbContext;

        public async Task<Category> Get(string name)
        {
            var category = await dbContext.Categories.FirstOrDefaultAsync(x => x.Name == name);

            if (category == null)
            {
                category = Category.Create(name);
                await dbContext.Categories.AddAsync(category);
            }

            return category;
        }

        public async Task<IReadOnlyList<Category>> GetList() => 
            await dbContext.Categories.ToAsyncEnumerable().OrderBy(x => x.Name).ToList();
    }
}
