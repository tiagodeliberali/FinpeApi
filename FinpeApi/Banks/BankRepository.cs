using FinpeApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FinpeApi.Banks
{
    public class BankRepository
    {
        private FinpeDbContext dbContext;

        public BankRepository(FinpeDbContext dbContext) => this.dbContext = dbContext;

        public IReadOnlyList<Bank> GetList() => dbContext.Banks
            .Include(x => x.BankStatements)
            .ToList();
    }
}
