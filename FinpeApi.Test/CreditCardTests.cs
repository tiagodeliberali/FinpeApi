using FinpeApi.CreditCards;
using FinpeApi.ValueObjects;
using System;
using Xunit;

namespace FinpeApi.Test
{
    public class CreditCardTests
    {
        [Fact]
        public void Create_CreditCardStatement()
        {
            // Arrange
            string description = "test description";
            decimal amount = 100m;
            DateTime date = DateTime.Parse("2018-05-02");
            int id = 10;

            CreditCardBill bill = new CreditCardBill();
            bill.SetId(id);

            // Act
            CreditCardStatement statement = CreditCardStatement.Create(
                bill, StatementDescription.Create(description), MoneyAmount.Create(amount), date);

            // Assert
            Assert.Equal(id, statement.Bill.Id);
            Assert.Equal(description, statement.Description.Value);
            Assert.Equal(amount, statement.Amount.Value);
            Assert.Equal(date, statement.BuyDate);
        }

        [Fact]
        public void Create_CreditCardStatement_BillShoudHaveId()
        {
            // Arrange
            string description = "test description";
            decimal amount = 100m;
            DateTime date = DateTime.Parse("2018-05-02");
            
            CreditCardBill billWithoutId = new CreditCardBill();
            
            // Act
            Action createStatement = () => CreditCardStatement.Create(
                billWithoutId, StatementDescription.Create(description), MoneyAmount.Create(amount), date);

            // Assert
            Assert.Throws<ArgumentException>(createStatement);
        }

        [Fact]
        public void Create_CreditCardStatement_BillShoudNotBeNull()
        {
            // Arrange
            string description = "test description";
            decimal amount = 100m;
            DateTime date = DateTime.Parse("2018-05-02");

            CreditCardBill nullBill = null;

            // Act
            Action createStatement = () => CreditCardStatement.Create(
                nullBill, StatementDescription.Create(description), MoneyAmount.Create(amount), date);

            // Assert
            Assert.Throws<ArgumentNullException>(createStatement);
        }
    }
}
