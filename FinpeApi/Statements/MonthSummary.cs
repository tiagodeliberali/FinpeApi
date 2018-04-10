using FinpeApi.Banks;
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
            this.statements = statements;
            this.banks = banks;
        }

        public decimal GetTotalIncome() => statements.Where(x => x.Direction == StatementDirection.Income).Sum(x => x.Amount);

        public IReadOnlyList<Statement> GetPendingStatements() => statements
            .Where(x => !x.Paid && x.Direction == StatementDirection.Outcome)
            .OrderBy(x => x.DueDate)
            .ToList();

        public IReadOnlyList<Statement> GetExpenses() => statements
            .Where(x => x.Direction == StatementDirection.Outcome)
            .ToList();

        public decimal GetCurrentBalance() => banks.Sum(x => x.GetLatestStatement().Amount);
    }
}
