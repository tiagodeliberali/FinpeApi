using FinpeApi.Banks;
using FinpeApi.ValueObjects;
using System;
using Xunit;

namespace FinpeApi.Test
{
    public class BankTests
    {
        [Fact]
        public void CreateBankStatement()
        {
            // Arrange
            int bankId = 10;
            MoneyAmount amount = MoneyAmount.Create(12.50m);
            DateTime date = DateTime.Parse("2018-04-28 18:35:27");

            Bank bank = new Bank();
            bank.SetId(bankId);

            // Act
            BankStatement statement = bank.NewBankStatement(amount, date);

            // Assert
            Assert.Equal(bankId, statement.Bank.Id);
            Assert.Equal(date, statement.ExecutionDate);
            Assert.Equal(amount, statement.Amount);
        }

        [Fact]
        public void CreateBankStatement_BankShouldHaveId()
        {
            // Arrange
            MoneyAmount amount = MoneyAmount.Create(12.50m);
            DateTime date = DateTime.Parse("2018-04-28 18:35:27");

            Bank bank = new Bank();

            // Act
            Action createStatement = () => bank.NewBankStatement(amount, date);

            // Assert
            Assert.Throws<ArgumentException>(createStatement);
        }

        [Fact]
        public void GetLatestStatement_ShouldReturnNullIfNothingAvailable()
        {
            // Arrange
            Bank bank = new Bank();

            // Act
            BankStatement nullStatement = bank.GetLatestStatement();

            // Assert
            Assert.Null(nullStatement);
        }
    }
}
