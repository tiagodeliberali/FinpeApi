using System.Collections.Generic;
using FinpeApi.ValueObjects;

namespace FinpeApi.CreditCards
{
    public class CreditCardController
    {
        private CreditCardRepository creditCardRepository;

        public CreditCardController(CreditCardRepository creditCardRepository)
        {
            this.creditCardRepository = creditCardRepository;
        }

        public IReadOnlyList<CreditCard> GetList(MonthYear monthYear)
        {
            bool reloadList = false;
            var creditCards = creditCardRepository.GetList(monthYear);

            foreach (var creditCard in creditCards)
            {
                reloadList |= CreateBillIfNotExists(creditCard, monthYear);
                reloadList |= CreateBillIfNotExists(creditCard, monthYear.GetNextMonthYear());
            }

            return reloadList ? creditCardRepository.GetList(monthYear) : creditCards;
        }

        private bool CreateBillIfNotExists(CreditCard creditCard, MonthYear monthYear)
        {
            bool billCreated = false;

            if (creditCard.GetCurrentBill(monthYear) == null)
            {
                creditCardRepository.AddBill(creditCard.CreateBill(monthYear));
                billCreated = true;
            }

            return billCreated;
        }
    }
}
