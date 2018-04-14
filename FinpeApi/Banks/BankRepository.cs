using FinpeApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FinpeApi.Banks
{
    public class BankRepository : IBankRepository
    {
        private IFinpeDbContext dbContext;

        public BankRepository(IFinpeDbContext dbContext) => this.dbContext = dbContext;

        public IReadOnlyList<Bank> GetList() => dbContext.Banks
            .Include(x => x.BankStatements)
            .ToList();
    }
}
