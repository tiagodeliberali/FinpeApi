using FinpeApi.Categories;
using FinpeApi.Statements;
using FinpeApi.ValueObjects;
using System;
using Xunit;

namespace FinpeApi.Test
{
    public class StatementTests
    {
        [Fact]
        public void CreateOutcome()
        {
            // Arrange
            var amount = MoneyAmount.Create(10m);
            var description = StatementDescription.Create("test");
            var date = DateTime.Parse("2018-04-14");
            var category = Category.Create("category");
            category.SetId(1);

            // Act
            var statement = Statement.CreateOutcome(description, amount, date, category);

            // Assert
            Assert.Equal(0, statement.Id);
            Assert.Equal(amount, statement.Amount);
            Assert.Equal(description, statement.Description);
            Assert.Equal(category, statement.Category);
            Assert.Equal(date, statement.DueDate);
            Assert.Null(statement.PaymentDate);
            Assert.False(statement.Paid);
            Assert.Equal(StatementDirection.Outcome, statement.Direction);
        }

        [Fact]
        public void MarkAsPaidShouldSavePaymentDate()
        {
            // Arrange
            var date = DateTime.Parse("2018-04-14");
            var category = Category.Create("category");
            category.SetId(1);

            var statement = Statement.CreateOutcome(StatementDescription.Create("test"), MoneyAmount.Create(10m), date, category);

            // Act
            statement.MarkAsPaid(date);

            // Assert
            Assert.Equal(date, statement.PaymentDate);
            Assert.True(statement.Paid);
        }

        [Fact]
        public void ShouldNotCreateStatementWithCategoryWithoutId()
        {
            // Arrange
            var date = DateTime.Parse("2018-04-14");
            var category = Category.Create("category");

            // Act
            Action create = () => Statement.CreateOutcome(StatementDescription.Create("test"), MoneyAmount.Create(10m), date, category);

            //Assert
            Assert.Throws<ArgumentException>(create);
        }
    }
}
