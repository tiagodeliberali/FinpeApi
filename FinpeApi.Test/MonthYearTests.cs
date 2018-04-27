using FinpeApi.ValueObjects;
using System;
using Xunit;

namespace FinpeApi.Test
{
    public class MonthYearTests
    {
        [Fact]
        public void ShouldReturnMonthName()
        {
            // Arrange
            string[] possibleNames = new string[] { "janeiro", "january", "January" };
            var monthYear = MonthYear.Create(2018, 1);

            // Act
            string result = monthYear.GetMonthName();

            // Assert
            Assert.Contains(result, possibleNames);
        }

        [Fact]
        public void ShouldBeAValidYear()
        {
            // Arrange
            var invalidYear = 3000;

            // Act
            Action create = () => MonthYear.Create(invalidYear, 1);

            // Assert
            Assert.Throws<ArgumentException>(create);
        }

        [Fact]
        public void ShouldBeAValidMonth()
        {
            // Arrange
            var invalidMonth = 13;

            // Act
            Action create = () => MonthYear.Create(2018, invalidMonth);

            // Assert
            Assert.Throws<ArgumentException>(create);
        }
    }
}
