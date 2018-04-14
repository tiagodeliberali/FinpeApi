using FinpeApi.Banks;
using FinpeApi.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinpeApi.Statements
{
    public class MonthSummary
    {
        private IReadOnlyList<Statement> statements;
        private IReadOnlyList<Bank> banks;

        public MonthSummary(IReadOnlyList<Statement> statements, IReadOnlyList<Bank> banks)
        {
            this.statements = statements ?? throw new ArgumentNullException("statements");
            this.banks = banks ?? throw new ArgumentNullException("banks");
        }

        public MoneyAmount GetTotalIncome() => statements.Where(x => x.Direction == StatementDirection.Income).Sum(x => x.Amount.Value);

        public IReadOnlyList<Statement> GetPendingStatements() => statements
            .Where(x => !x.Paid && x.Direction == StatementDirection.Outcome)
            .OrderBy(x => x.DueDate)
            .ToList();

        public IReadOnlyList<Statement> GetExpenses() => statements
            .Where(x => x.Direction == StatementDirection.Outcome)
            .ToList();

        public MoneyAmount GetCurrentBalance() => banks.Sum(x => x.GetLatestStatement().Amount);
    }
}
