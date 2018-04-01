using System;
using FinpeApi.Models;
using FinpeApi.Models.AppStates;

namespace FinpeApi.Services
{
    public class FinancialService : IFinancialService
    {
        private IFinpeDbContext dbContext;
        private INotificationService notificationService;

        public FinancialService(IFinpeDbContext dbContext, INotificationService notificationService) => 
            (this.dbContext, this.notificationService) = (dbContext, notificationService);

        public void AddStatement(OverviewStatement statement)
        {
            throw new NotImplementedException();
        }

        public OverviewState BuildMonth(int year, int month)
        {
            throw new NotImplementedException();
        }

        public void MarkStatementPaid(int id)
        {
            throw new NotImplementedException();
        }
    }
}
