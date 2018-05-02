using System;
using System.Collections.Generic;
using System.Linq;

namespace FinpeApi.Banks
{
    public class Bank
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyList<BankStatement> BankStatements { get; private set; }

        public BankStatement GetLatestStatement()
        {
            if (BankStatements == null || BankStatements.Count == 0)
            {
                return null;
            }

            return BankStatements
                .OrderBy(x => x.ExecutionDate)
                .Last();
        }

        public BankStatement NewBankStatement(decimal amount, DateTime executionDate)
        {
            return BankStatement.Create(this, executionDate, amount);
        }
    }
}
