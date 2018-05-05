using System.Collections.Generic;
using System.Linq;
using FinpeApi.Models;
using FinpeApi.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FinpeApi.CreditCards
{
    public class CreditCardRepository
    {
        private FinpeDbContext dbContext;

        public CreditCardRepository(FinpeDbContext dbContext) => this.dbContext = dbContext;

        public IReadOnlyList<CreditCard> GetList(MonthYear monthYear)
        {
            List<CreditCard> result = new List<CreditCard>();

            var creditCards = dbContext.CreditCards;

            foreach (var creditCard in creditCards)
            {
                var currentBill = dbContext.Entry(creditCard)
                    .Collection(x => x.Bills)
                    .Query()
                    .Include(x => x.Statements)
                    .Where(x => x.DueDate.Year == monthYear.Year && x.DueDate.Month == monthYear.Month)
                    .SingleOrDefault();

                var nextMonthYear = monthYear.GetNextMonthYear();

                var nextBill = dbContext.Entry(creditCard)
                    .Collection(x => x.Bills)
                    .Query()
                    .Include(x => x.Statements)
                    .Where(x => x.DueDate.Year == nextMonthYear.Year && x.DueDate.Month == nextMonthYear.Month)
                    .SingleOrDefault();

                result.Add(creditCard);
            }

            return result;
        }

        public void AddBill(CreditCardBill bill)
        {
            dbContext.CreditCardBills.Add(bill);
            dbContext.SaveChanges();
        }
    }
}
