using FinpeApi.Services;
using FinpeApi.Statements;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace FinpeApi.Overviews
{
    public class OverviewHub : Hub
    {
        private IFinancialService financialService;
        private IStatementRepository statementRepository;

        public OverviewHub(IFinancialService financialService, IStatementRepository statementRepository) {
            this.financialService = financialService;
            this.statementRepository = statementRepository;
        }

        public override async Task OnConnectedAsync()
        {
            await BuldOverview();
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
            await BuldOverview();
        }

        public async Task AddStatement(StatementDto statement)
        {
            var dbStatement = Statement.CreateOutcome(
                statement.Description,
                statement.Amount,
                statement.DueDate,
                Category.Create(statement.Category));

            await statementRepository.Save(dbStatement);
            await BuldOverview();
        }

        private async Task BuldOverview()
        {
            await Clients.All.SendAsync("NewOverview", "FinpeApp", financialService.BuildMonth(DateTime.Today.Year, DateTime.Today.Month));
        }
    }
}
