using System;

namespace FinpeApi.Statements
{
    public class StatementDto
    {
        public StatementDto(int id, DateTime dueDate, string description, decimal amount, string category)
        {
            Id = id;
            DueDate = dueDate;
            Description = description;
            Amount = amount;
            Category = category;
        }

        public int Id { get; private set; }
        public DateTime DueDate { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }
    }
}
