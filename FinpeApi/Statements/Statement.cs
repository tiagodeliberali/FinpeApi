using System;

namespace FinpeApi.Statements
{
    public class Statement
    {
        private Statement() { }

        public static Statement CreateOutcome(string description, decimal amount, DateTime dueDate, Category category)
        {
            return new Statement()
            {
                Description = description,
                Amount = amount,
                DueDate = dueDate,
                Category = category,
                Direction = StatementDirection.Outcome,
                Paid = false
            };
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime DueDate { get; private set; }
        public bool Paid { get; private set; }
        public StatementDirection Direction { get; private set; }
        public Category Category { get; private set; }

        public void MarkAsPaid() => Paid = true;
    }
}
