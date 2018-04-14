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

        public async Task<Category> Get(string name) => 
            await dbContext.Categories.FirstOrDefaultAsync(x => x.Name == name);

        public async Task Save(Category category)
        {
            if (category.Id == 0)
                await dbContext.Categories.AddAsync(category);

            await dbContext.SaveChangesAsync();
        }

        public IReadOnlyList<Category> GetList() => 
            dbContext.Categories.OrderBy(x => x.Name).ToList();
    }
}
