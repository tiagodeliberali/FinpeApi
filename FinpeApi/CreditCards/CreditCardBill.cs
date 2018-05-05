using FinpeApi.Categories;
using FinpeApi.ValueObjects;
using System;
using System.Collections.Generic;

namespace FinpeApi.CreditCards
{
    public class CreditCardBill
    {
        public int Id { get; private set; }
        public CreditCard CreditCard { get; private set; }
        public Category Category { get; private set; }
        public DateTime DueDate { get; private set; }
        public int ClosingDay { get; private set; }
        public bool Paid { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public IReadOnlyList<CreditCardStatement> Statements { get; private set; }

        public static CreditCardBill Create(CreditCard creditCard, MonthYear monthYear)
        {
            return new CreditCardBill()
            {
                ClosingDay = creditCard.ClosingDay,
                CreditCard = creditCard,
                DueDate = monthYear.GetDate(creditCard.PaymentDay),
                Category = creditCard.Category
            };
        }
    }
}
