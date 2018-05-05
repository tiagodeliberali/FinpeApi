using FinpeApi.Categories;
using FinpeApi.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace FinpeApi.CreditCards
{
    public class CreditCard
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Owner { get; private set; }
        public int EndNumbers { get; private set; }
        public int ClosingDay { get; private set; }
        public int PaymentDay { get; private set; }
        public Category Category { get; private set; }
        public IReadOnlyList<CreditCardBill> Bills { get; private set; }

        public CreditCardBill GetCurrentBill(MonthYear monthYear)
        {
            return Bills
                .Where(x => x.DueDate.Year == monthYear.Year && x.DueDate.Month == monthYear.Month)
                .SingleOrDefault();
        }

        public CreditCardBill GetNextBill(MonthYear monthYear)
        {
            MonthYear nextMonthYear = monthYear.GetNextMonthYear();
            return Bills
                .Where(x => x.DueDate.Year == nextMonthYear.Year && x.DueDate.Month == nextMonthYear.Month)
                .SingleOrDefault();
        }

        public CreditCardBill CreateBill(MonthYear monthYear) => CreditCardBill.Create(this, monthYear);
    }
}
