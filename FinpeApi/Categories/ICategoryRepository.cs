using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinpeApi.Categories
{
    public interface ICategoryRepository
    {
        Task<Category> Get(string name);
        IReadOnlyList<Category> GetList();
        Task Save(Category category);
    }
}
