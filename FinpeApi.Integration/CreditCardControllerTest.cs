using FinpeApi.CreditCards;
using FinpeApi.Integration.DatabaseDTOs;
using FinpeApi.ValueObjects;
using System;
using System.Linq;
using Xunit;

namespace FinpeApi.Integration
{
    [Collection("IntegrationTests")]
    public class CreditCardControllerTest
    {
        private DbUtils dbUtils;
        private TestsUtils testsUtils;

        public CreditCardControllerTest()
        {
            dbUtils = new DbUtils();
            testsUtils = new TestsUtils(dbUtils);
        }

        [Fact]
        public void GetCreditCardWithCurrentAndNextBills_NoBillExistingBefore()
        {
            // Arrange
            MonthYear monthYear = MonthYear.Create(2018, 4);
            int creditCardId = testsUtils.AddSingleCreditCard().Id;

            var sut = new CreditCardController(new CreditCardRepository(dbUtils.DbContext));

            // Act
            var creditCardList = sut.GetList(monthYear);

            // Assert
            Assert.Equal(1, creditCardList.Count);

            CreditCardBill currentBill = creditCardList.First().GetCurrentBill(monthYear);
            Assert.True(currentBill.Id > 0);
            Assert.Equal(TestsUtils.CLOSING_DAY, currentBill.ClosingDay);
            Assert.Equal(creditCardId, currentBill.CreditCard.Id);
            Assert.Equal(0, currentBill.Statements.Count);

            CreditCardBill nextBill = creditCardList.First().GetNextBill(monthYear);
            Assert.True(nextBill.Id > 0);
            Assert.Equal(TestsUtils.CLOSING_DAY, nextBill.ClosingDay);
            Assert.Equal(creditCardId, nextBill.CreditCard.Id);
            Assert.Equal(0, nextBill.Statements.Count);
        }

        [Fact]
        public void GetCreditCardWithCurrentAndNextBills_RecoveryExistingBills()
        {
            // Arrange
            MonthYear monthYear = MonthYear.Create(2018, 4);
            DbCreditCardDto creditCard = testsUtils.AddSingleCreditCard();

            var bill = new DbCreditCardBillDto()
            {
                CreditCardId = creditCard.Id,
                ClosingDay = TestsUtils.CLOSING_DAY + 1,
                DueDate = monthYear.GetFirstDay(),
                CategoryId = creditCard.CategoryId
            };
            dbUtils.Insert(bill);

            var statement = new DbCreditCardStatementDto()
            {
                BillId = bill.Id,
                Amount = 100m,
                BuyDate = DateTime.Parse("2018-05-05"),
                Description = "test credit card statement"
            };
            dbUtils.Insert(statement);

            var sut = new CreditCardController(new CreditCardRepository(dbUtils.DbContext));

            // Act
            var creditCardList = sut.GetList(monthYear);

            // Assert
            Assert.Equal(1, creditCardList.Count);

            CreditCardBill currentBill = creditCardList.First().GetCurrentBill(monthYear);
            Assert.Equal(bill.Id, currentBill.Id);
            Assert.Equal(TestsUtils.CLOSING_DAY + 1, currentBill.ClosingDay);
            Assert.Equal(creditCard.Id, currentBill.CreditCard.Id);
            Assert.Equal(1, currentBill.Statements.Count);

            CreditCardBill nextBill = creditCardList.First().GetNextBill(monthYear);
            Assert.True(nextBill.Id > 0);
            Assert.Equal(TestsUtils.CLOSING_DAY, nextBill.ClosingDay);
            Assert.Equal(creditCard.Id, nextBill.CreditCard.Id);
            Assert.Equal(0, nextBill.Statements.Count);
        }
    }
}
