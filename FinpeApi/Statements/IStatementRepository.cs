using System.Threading.Tasks;

namespace FinpeApi.Statements
{
    public interface IStatementRepository
    {
        Task<Statement> Get(int id);
        Task Save(Statement statement);
        Task<Category> GetCategory(string categoryName);
    }
}