using FinpeApi.Overviews;

namespace FinpeApi.Services
{
    public interface IFinancialService
    {
        OverviewDto BuildMonth(int year, int month);
    }
}
