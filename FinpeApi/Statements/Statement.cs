using FinpeApi.Categories;
using FinpeApi.ValueObjects;
using System;

namespace FinpeApi.Statements
{
    public class Statement
    {
        private Statement() { }

        public static Statement CreateOutcome(StatementDescription description, MoneyAmount amount, DateTime dueDate, Category category)
        {
            if (!category.Exists())
                throw new ArgumentException("Cannot assign categories not present on database", nameof(category));

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
        public StatementDescription Description { get; private set; }
        public MoneyAmount Amount { get; private set; }
        public DateTime DueDate { get; private set; }
        public bool Paid { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public StatementDirection Direction { get; private set; }
        public Category Category { get; private set; }

        public void MarkAsPaid(DateTime paymentDate)
        {
            Paid = true;
            PaymentDate = paymentDate;
        }

        public void UpdateAmount(MoneyAmount amount) => this.Amount = amount;
  }
}
