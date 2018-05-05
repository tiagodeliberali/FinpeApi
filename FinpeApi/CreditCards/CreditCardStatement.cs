using FinpeApi.ValueObjects;
using System;

namespace FinpeApi.CreditCards
{
    public class CreditCardStatement
    {
        public int Id { get; private set; }
        public CreditCardBill Bill { get; private set;  }
        public StatementDescription Description { get; private set; }
        public MoneyAmount Amount { get; private set; }
        public DateTime BuyDate { get; private set; }

        public static CreditCardStatement Create(CreditCardBill bill, StatementDescription description, MoneyAmount amount, DateTime date)
        {
            if (bill == null)
                throw new ArgumentNullException(nameof(bill));

            if (bill.Id < 1)
                throw new ArgumentException("Credit card bill entity must have a valid id.", nameof(bill));

            return new CreditCardStatement()
            {
                Bill = bill,
                Description = description,
                Amount  = amount,
                BuyDate = date
            };
        }
    }
}
