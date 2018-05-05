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

        [Fact]
        public void ShouldReturnFirstDayOfMonthYear()
        {
            // Arrange
            MonthYear monthYear = MonthYear.Create(2018, 5);

            // Act
            DateTime firstDay = monthYear.GetFirstDay();

            // Assert
            Assert.Equal(DateTime.Parse("2018-05-01"), firstDay);
        }

        [Theory]
        [InlineData(2018, 12, true)]
        [InlineData(2018, 11, false)]
        [InlineData(2017, 12, false)]
        [InlineData(2017, 11, false)]
        public void ShouldCompareMonthYears(int year, int month, bool expectedResult)
        {
            // Arrange
            MonthYear monthYear = MonthYear.Create(2018, 12);
            MonthYear anotherMonthYear = MonthYear.Create(year, month);

            // Act
            bool result1 = anotherMonthYear == monthYear;
            bool result2 = anotherMonthYear.Equals(monthYear);

            // Assert
            Assert.Equal(expectedResult, result1);
            Assert.Equal(expectedResult, result2);
        }

        [Theory]
        [InlineData(2018, 12, 2019, 1)]
        [InlineData(2018, 11, 2018, 12)]
        public void ShouldReturnNextMonthYear(int year, int month, int nextYear, int nextMonth)
        {
            // Arrange
            MonthYear monthYear = MonthYear.Create(year, month);

            // Act
            MonthYear nextMonthYear = monthYear.GetNextMonthYear();

            // Assert
            Assert.Equal(MonthYear.Create(nextYear, nextMonth), nextMonthYear);
        }

        [Fact]
        public void GetDate()
        {
            // Arrange
            int year = 2018;
            int month = 5;
            int day = 6;

            MonthYear monthYear = MonthYear.Create(year, month);

            // Act
            DateTime result = monthYear.GetDate(day);

            // Assert
            Assert.Equal(year, result.Year);
            Assert.Equal(month, result.Month);
            Assert.Equal(day, result.Day);
        }
    }
}
