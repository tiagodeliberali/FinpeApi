using FinpeApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace FinpeApi.Banks
{
    public class BankRepository : IBankRepository
    {
        private IFinpeDbContext dbContext;

        public BankRepository(IFinpeDbContext dbContext) => this.dbContext = dbContext;

        public IReadOnlyList<Bank> GetList()
        {
            var bankStatements = dbContext.Banks
                .Select(x => new
                {
                    Bank = x,
                    LastStatement = x.BankStatements.OrderBy(statement => statement.ExecutionDate).Last()
                });

            foreach (var item in bankStatements)
            {
                item.Bank.SetLatestStatement(item.LastStatement);
            }

            IReadOnlyList<Bank> banks = bankStatements
                .Select(x => x.Bank)
                .ToList();

            return banks;
        }
    }
}
