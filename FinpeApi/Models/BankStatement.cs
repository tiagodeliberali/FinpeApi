using System;

namespace FinpeApi.Models
{
    public class BankStatement
    {
        public int Id { get; set; }
        public DateTime ExecutionDate { get; set; }
        public decimal Amount { get; set; }
        public int BankId { get; set; }
        public Bank Bank { get; set; }
    }
}
