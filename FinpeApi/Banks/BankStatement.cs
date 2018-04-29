using FinpeApi.ValueObjects;
using System;

namespace FinpeApi.Banks
{
    public class BankStatement
    {
        public int Id { get; private set; }
        public Bank Bank { get; private set; }
        public DateTime ExecutionDate { get; private set; }
        public MoneyAmount Amount { get; private set; }

        public static BankStatement Create(Bank bank, DateTime executionDate, MoneyAmount amount)
        {
            if (bank == null)
                throw new ArgumentNullException(nameof(bank));

            if (bank.Id < 1)
                throw new ArgumentException("Bank entity must have a valid id.", nameof(bank));

            return new BankStatement()
            {
                Bank = bank,
                ExecutionDate = executionDate,
                Amount = amount
            };
        }
    }
}
