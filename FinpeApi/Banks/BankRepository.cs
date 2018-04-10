using FinpeApi.Models;
using FinpeApi.Utils;
using System.Collections.Generic;
using System.Linq;

namespace FinpeApi.Banks
{
    public class BankRepository : IBankRepository
    {
        private IFinpeDbContext dbContext;

        public BankRepository(IFinpeDbContext dbContext) => this.dbContext = dbContext;

        public IReadOnlyList<Bank> GetList(MonthYear monthYear)
        {
            var bankStatements = dbContext.Banks
                .Select(x => new
                {
                    Bank = x,
                    MonthStatements = x.BankStatements
                        .Where(statement => statement.ExecutionDate.Year == monthYear.Year && statement.ExecutionDate.Month == monthYear.Month)
                        .OrderBy(statement => statement.ExecutionDate)
                });

            return bankStatements.Select(x => Bank.Create(x.Bank, x.MonthStatements)).ToList();
        }
    }
}
