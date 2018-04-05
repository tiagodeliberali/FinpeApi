using FinpeApi.Models.AppStates;
using System.Threading.Tasks;

namespace FinpeApi.Services
{
    public interface IFinancialService
    {
        OverviewState BuildMonth(int year, int month);
        Task AddStatement(OverviewStatement statement);
        Task MarkStatementPaid(int id);
    }
}
