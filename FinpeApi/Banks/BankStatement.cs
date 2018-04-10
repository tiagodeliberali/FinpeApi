using System;

namespace FinpeApi.Banks
{
    public class BankStatement
    {
        public int Id { get; set; }
        public Bank Bank { get; set; }
        public DateTime ExecutionDate { get; private set; }
        public decimal Amount { get; private set; }
    }
}
