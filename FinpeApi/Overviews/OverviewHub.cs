using FinpeApi.Banks;
using FinpeApi.Categories;
using FinpeApi.Statements;
using FinpeApi.Utils;
using FinpeApi.ValueObjects;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinpeApi.Overviews
{
    public class OverviewHub : Hub
    {
        private StatementController statementController;

        public OverviewHub(StatementController statementController)
        {
            this.statementController = statementController;
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
            await statementController.MarkStatementPaid(id);
            await BroadcastOverview();
        }

        public async Task UpdateAmount(int id, decimal amount)
        {
            await statementController.UpdateAmount(id, amount);
            await BroadcastOverview();
        }

        public async Task AddStatement(StatementDto statement)
        {
            await statementController.AddStatement(statement);
            await BroadcastOverview();
        }

        private async Task BroadcastOverview()
        {
            await Clients.All.SendAsync(
                "NewOverview",
                "FinpeApp",
                await statementController.BuildCurrentMonth());
        }
    }
}
