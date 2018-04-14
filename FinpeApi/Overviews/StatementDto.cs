using FinpeApi.Categories;
using FinpeApi.ValueObjects;
using System;

namespace FinpeApi.Overviews
{
    public class StatementDto
    {
        public StatementDto(EntityId id, DateTime dueDate, StatementDescription description, MoneyAmount amount, Category category)
        {
            Id = id.Value;
            DueDate = dueDate;
            Description = description;
            Amount = amount;
            Category = category.Name;
        }

        public int Id { get; private set; }
        public DateTime DueDate { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }
    }
}
