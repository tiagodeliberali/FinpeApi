using FinpeApi.Models.AppStates;
using FinpeApi.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace FinpeApi.Hubs
{
    public class OverviewHub : Hub
    {
        private IFinancialService financialService;

        public OverviewHub(IFinancialService financialService) => this.financialService = financialService;

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
            await financialService.MarkStatementPaid(id);
            await BuldOverview();
        }

        public async Task AddStatement(OverviewStatement statement)
        {
            await financialService.AddStatement(statement);
            await BuldOverview();
        }

        private async Task BuldOverview()
        {
            await Clients.All.SendAsync("NewOverview", "FinpeApp", financialService.BuildMonth(DateTime.Today.Year, DateTime.Today.Month));
        }
    }
}
