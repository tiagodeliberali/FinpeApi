using FinpeApi.Categories;
using FinpeApi.ValueObjects;

namespace FinpeApi.Overviews
{
    public class ExpenseDto
    {
        public ExpenseDto(Category category, MoneyAmount amount)
        {
            this.Category = category.Name;
            this.Amount = amount;
        }

        public string Category { get; private set; }
        public decimal Amount { get; private set; }
    }
}
