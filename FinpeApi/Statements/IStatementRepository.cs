using FinpeApi.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinpeApi.Statements
{
    public interface IStatementRepository
    {
        Task<Statement> Get(int id);
        Task<IReadOnlyList<Statement>> GetList(MonthYear monthYear);
        Task Save(Statement statement);
    }
}