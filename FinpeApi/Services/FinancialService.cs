using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FinpeApi.Models;
using FinpeApi.Models.AppStates;
using Microsoft.EntityFrameworkCore;

namespace FinpeApi.Services
{
    public class FinancialService : IFinancialService
    {
        private IFinpeDbContext dbContext;
        
        public FinancialService(IFinpeDbContext dbContext) => this.dbContext = dbContext;

        public async Task AddStatement(OverviewStatement statement)
        {
            var dbStatement = new Statement
            {
                Amount = statement.Amount,
                Description = statement.Description,
                DueDate = statement.DueDate,
                Category = await GetCategory(statement.Category),
                Paid = false
            };

            await dbContext.Statements.AddAsync(dbStatement);
            await dbContext.SaveChangesAsync();
        }

        private async Task<Category> GetCategory(string categoryName)
        {
            var category = await dbContext.Categories.FirstOrDefaultAsync(x => x.Name == categoryName);

            if (category == null)
            {
                category = new Category()
                {
                    Name = categoryName
                };

                await dbContext.Categories.AddAsync(category);
            }

            return category;
        }

        public OverviewState BuildMonth(int year, int month)
        {
            return new OverviewState()
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

        private List<OverviewStatement> GetPendingStatements(int year, int month)
        {
            return GetOutcome(year, month)
                .Where(x => !x.Paid)
                .Select(x => new OverviewStatement()
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

        private List<OverviewExpense> GetExpenses(int year, int month)
        {
            return GetOutcome(year, month)
                .GroupBy(x => x.Category)
                .Select(x => new OverviewExpense()
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

        public async Task MarkStatementPaid(int id)
        {
            var statement = await dbContext.Statements.FindAsync(id);
            statement.Paid = true;

            await dbContext.SaveChangesAsync();
        }
    }
}
