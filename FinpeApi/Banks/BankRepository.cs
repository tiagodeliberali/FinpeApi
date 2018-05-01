using FinpeApi.Models;
using FinpeApi.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinpeApi.Banks
{
    public class BankRepository
    {
        private FinpeDbContext dbContext;

        public BankRepository(FinpeDbContext dbContext) => this.dbContext = dbContext;

        public IReadOnlyList<Bank> GetList(MonthYear monthYear)
        {
            List<Bank> result = new List<Bank>();

            var banks = dbContext.Banks;

            foreach (var bank in banks)
            {
                dbContext.Entry(bank)
                    .Collection(x => x.BankStatements)
                    .Query()
                    .Where(x => x.ExecutionDate.Year == monthYear.Year && x.ExecutionDate.Month == monthYear.Month)
                    .ToList();

                result.Add(bank);
            }

            return result;
        }

        public async Task SaveStatement(BankStatement statement)
        {
            dbContext.BankStatements.Add(statement);
            await dbContext.SaveChangesAsync();
        }
    }
}
