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
            this.statements = statements ?? throw new ArgumentNullException(nameof(statements));
            this.banks = banks ?? throw new ArgumentNullException(nameof(banks));
        }

        public MoneyAmount GetTotalIncome() => statements.Where(x => x.Direction == StatementDirection.Income).Sum(x => x.Amount.Value);

        public IReadOnlyList<Statement> GetPendingExpenses() => statements
            .Where(x => !x.Paid && x.Direction == StatementDirection.Outcome)
            .OrderBy(x => x.DueDate)
            .ToList();

        public IReadOnlyList<Statement> GetExpenses() => statements
            .Where(x => x.Direction == StatementDirection.Outcome)
            .ToList();

        public MoneyAmount GetCurrentBalance()
        {
            DateTime? latestDate = banks.First().GetLatestStatement()?.ExecutionDate;

            decimal bankBalance;
            decimal outcomePaidBalance;

            if (latestDate.HasValue)
            {
                bankBalance = banks.Sum(x => x.GetLatestStatement().Amount);

                outcomePaidBalance = statements
                    .Where(x => x.Paid && x.Direction == StatementDirection.Outcome && x.PaymentDate > latestDate)
                    .Sum(x => x.Amount);
            }
            else
            {
                bankBalance = 0;

                outcomePaidBalance = statements
                    .Where(x => x.Paid && x.Direction == StatementDirection.Outcome)
                    .Sum(x => x.Amount);
            }

            return bankBalance - outcomePaidBalance;
        }

        public decimal GetInitialBalance()
        {
            BankStatement statement = banks.First().BankStatements.FirstOrDefault();
            return statement == null ? 0m : statement.Amount.Value;
        }
    }
}
