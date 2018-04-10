using FinpeApi.Categories;
using System;

namespace FinpeApi.Statements
{
    public class Statement
    {
        private Statement() { }

        public static Statement CreateOutcome(string description, decimal amount, DateTime dueDate, Category category)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("Must supply a valid description", "description");

            if (amount < 0)
                throw new ArgumentException("Must supply a positive amount", "amount");

            if (amount % 0.01m != 0)
                throw new ArgumentException("Must supply an amount with max precision of 2 decimals", "amount");

            if (!category.Exists())
                throw new ArgumentException("Cannot assign categories not present on database", "category");

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
