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
    }
}
