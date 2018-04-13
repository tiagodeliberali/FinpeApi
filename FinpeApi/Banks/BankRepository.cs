using FinpeApi.Models;
using FinpeApi.Utils;
using Microsoft.EntityFrameworkCore;
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
            var bankStatements = dbContext.Banks.Include(x => x.BankStatements).ToList();
            return bankStatements;
        }
    }
}
