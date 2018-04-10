using FinpeApi.Banks;
using FinpeApi.Categories;
using FinpeApi.Statements;
using FinpeApi.Utils;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinpeApi.Overviews
{
    public class OverviewHub : Hub
    {
        private IStatementRepository statementRepository;
        private ICategoryRepository categoryRepository;
        private IBankRepository bankRepository;
        private IDateService dateService;

        public OverviewHub(IStatementRepository statementRepository,
            ICategoryRepository categoryRepository,
            IBankRepository bankRepository,
            IDateService dateService)
        {
            this.statementRepository = statementRepository;
            this.categoryRepository = categoryRepository;
            this.bankRepository = bankRepository;
            this.dateService = dateService;
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
            dbStatement.MarkAsPaid(dateService.GetCurrentDateTime());
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
                await BuildMonth(dateService.GetCurrentMonthYear()));
        }

        private async Task<OverviewDto> BuildMonth(MonthYear monthYear)
        {
            IReadOnlyList<Statement> statements = await statementRepository.GetList(monthYear);
            IReadOnlyList<Bank> banks = bankRepository.GetList(monthYear);
            IReadOnlyList<Category> categories = await categoryRepository.GetList();

            MonthSummary summary = new MonthSummary(statements, banks);

            return OverviewDto.Create(monthYear, summary, categories);
        }
    }
}
