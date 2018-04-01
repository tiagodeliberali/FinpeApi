using FinpeApi.Models.AppStates;

namespace FinpeApi.Services
{
    public interface IFinancialService
    {
        OverviewState BuildMonth(int year, int month);
        void AddStatement(OverviewStatement statement);
        void MarkStatementPaid(int id);
    }
}
