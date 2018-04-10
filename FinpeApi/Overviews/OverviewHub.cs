using FinpeApi.Banks;
using FinpeApi.Categories;
using FinpeApi.Statements;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FinpeApi.Overviews
{
    public class OverviewHub : Hub
    {
        private IStatementRepository statementRepository;
        private ICategoryRepository categoryRepository;
        private IBankRepository bankRepository;

        public OverviewHub(IStatementRepository statementRepository, 
            ICategoryRepository categoryRepository,
            IBankRepository bankRepository)
        {
            this.statementRepository = statementRepository;
            this.categoryRepository = categoryRepository;
            this.bankRepository = bankRepository;
        }

        public override async Task OnConnectedAsync()
        {
            await BroadcastOverview();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.All.SendAsync("SendAction", Context.User.Identity.Name, "left");
        }

        public async Task MarkStatementPaid(int id)
        {
            Statement dbStatement = await statementRepository.Get(id);
            dbStatement.MarkAsPaid();
            await statementRepository.Save(dbStatement);
            await BroadcastOverview();
        }

        public async Task AddStatement(StatementDto statement)
        {
            var category = await categoryRepository.Get(statement.Category);

            var dbStatement = Statement.CreateOutcome(
                statement.Description,
                statement.Amount,
                statement.DueDate,
                category);

            await statementRepository.Save(dbStatement);
            await BroadcastOverview();
        }

        private async Task BroadcastOverview()
        {
            await Clients.All.SendAsync(
                "NewOverview", 
                "FinpeApp", 
                await BuildMonth(DateTime.Today.Year, DateTime.Today.Month));
        }

        private async Task<OverviewDto> BuildMonth(int year, int month)
        {
            IReadOnlyList<Statement> statements = await statementRepository.GetList(year, month);
            IReadOnlyList<Category> categories = await categoryRepository.GetList();
            IReadOnlyList<Bank> banks = bankRepository.GetList();

            return new OverviewDto()
            {
                MonthName = GetMonthName(month),
                TotalIncome = GetTotalIncome(statements),
                BankAmount = GetBankAmount(banks),
                Year = year,
                Categories = GetCategories(categories),
                Expenses = GetExpenses(statements),
                PendingStatements = GetPendingStatements(statements)
            };
        }

        private string GetMonthName(int month) => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);

        private decimal GetTotalIncome(IReadOnlyList<Statement> statements) => statements.Where(x => x.Direction == StatementDirection.Income).Sum(x => x.Amount);

        private decimal GetBankAmount(IReadOnlyList<Bank> banks) => banks.Sum(x => x.GetLatestStatement().Amount);

        private IReadOnlyList<string> GetCategories(IReadOnlyList<Category> categories) => categories
            .Select(x => x.Name)
            .ToList();

        private IReadOnlyList<StatementDto> GetPendingStatements(IReadOnlyList<Statement> statements) => statements
            .Where(x => !x.Paid && x.Direction == StatementDirection.Outcome)
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

        private IReadOnlyList<ExpenseDto> GetExpenses(IReadOnlyList<Statement> statements) => statements
            .Where(x => x.Direction == StatementDirection.Outcome)
            .GroupBy(x => x.Category)
            .Select(x => new ExpenseDto()
            {
                Category = x.Key.Name,
                Amount = x.Sum(values => values.Amount)
            })
            .ToList();
    }
}
