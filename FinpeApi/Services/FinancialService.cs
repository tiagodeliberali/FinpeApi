using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FinpeApi.Models;
using FinpeApi.Overviews;
using FinpeApi.Statements;

namespace FinpeApi.Services
{
    public class FinancialService : IFinancialService
    {
        private IFinpeDbContext dbContext;
        
        public FinancialService(IFinpeDbContext dbContext) => this.dbContext = dbContext;

        public OverviewDto BuildMonth(int year, int month)
        {
            return new OverviewDto()
            {
                MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month),
                TotalIncome = GetIncome(year, month).Sum(x => x.Amount),
                BankAmount = GetBankAmount(),
                Year = year,
                Categories = dbContext.Categories.Select(x => x.Name).ToList(),
                Expenses = GetExpenses(year, month),
                PendingStatements = GetPendingStatements(year, month)
            };
        }

        private decimal GetBankAmount()
        {
            decimal total = 0;

            dbContext.Banks.ToList().ForEach(bank =>
            {
                total += dbContext.BankStatements
                    .Where(x => x.Id == bank.Id)
                    .OrderByDescending(x => x.ExecutionDate)
                    .Last()
                    .Amount;
            });

            return total;
        }

        private IQueryable<Statement> GetIncome(int year, int month)
        {
            return dbContext.Statements
                .Where(x => x.DueDate.Year == year 
                    && x.DueDate.Month == month 
                    && x.Direction == StatementDirection.Income);
        }

        private List<StatementDto> GetPendingStatements(int year, int month)
        {
            return GetOutcome(year, month)
                .Where(x => !x.Paid)
                .Select(x => new StatementDto()
                {
                    Category = x.Category.Name,
                    Amount = x.Amount,
                    Description = x.Description,
                    DueDate = x.DueDate,
                    Id = x.Id
                })
                .OrderBy(x => x.DueDate)
                .ToList();
        }

        private List<ExpenseDto> GetExpenses(int year, int month)
        {
            return GetOutcome(year, month)
                .GroupBy(x => x.Category)
                .Select(x => new ExpenseDto()
                {
                    Category = x.Key.Name,
                    Amount = x.Sum(values => values.Amount)
                })
                .ToList();
        }

        private IQueryable<Statement> GetOutcome(int year, int month)
        {
            return dbContext.Statements
                .Where(x => x.DueDate.Year == year
                    && x.DueDate.Month == month
                    && x.Direction == StatementDirection.Outcome);
        }
    }
}
