using FinpeApi.Banks;
using FinpeApi.Categories;
using FinpeApi.Statements;
using FinpeApi.Integration.DatabaseDTOs;
using FinpeApi.Utils;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FinpeApi.ValueObjects;

namespace FinpeApi.Integration
{
    [Collection("IntegrationTests")]
    public class StatementControllerTest
    {
        private DbUtils dbUtils;
        private TestsUtils testsUtils;

        public StatementControllerTest()
        {
            dbUtils = new DbUtils();
            testsUtils = new TestsUtils(dbUtils);
        }

        [Fact]
        public async Task MarkStatementAsPaid()
        {
            // Arrange
            int statementId = testsUtils.AddSingleStatement();
            DateTime currentDateTime = DateTime.Parse("2018-04-21");
            
            var sut = BuildStatementController(currentDateTime);

            // Act
            await sut.MarkStatementPaid(statementId);

            // Assert
            var statement = dbUtils.GetAll<DbStatementDto>().Single();
            Assert.True(statement.Paid);
            Assert.Equal(currentDateTime, statement.PaymentDate);
        }

        [Fact]
        public async Task UpdateAmount()
        {
            // Arrange
            int statementId = testsUtils.AddSingleStatement();
            
            var sut = BuildStatementController();

            // Act
            decimal amount = 150.53m;
            await sut.UpdateAmount(statementId, amount);

            // Assert
            var statement = dbUtils.GetAll<DbStatementDto>().Single();
            Assert.Equal(amount, statement.Amount);
        }

        [Fact]
        public async Task UpdateBalance()
        {
            // Arrange
            int bankId = testsUtils.AddSingleBank();
            DateTime currentDateTime = DateTime.Parse("2018-04-28");

            var sut = BuildStatementController(currentDateTime);

            // Act
            decimal amount = 220.56m;
            await sut.UpdateBalance(amount);

            // Assert
            var statement = dbUtils.GetAll<DbBankStatementDto>().Single();
            Assert.Equal(bankId, statement.BankId);
            Assert.Equal(amount, statement.Amount);
            Assert.Equal(currentDateTime, statement.ExecutionDate);
        }

        [Fact]
        public async Task AddStatement()
        {
            // Arrange
            testsUtils.CleanAll();
            DateTime currentDateTime = DateTime.Parse("2018-04-21");

            var sut = BuildStatementController();

            // Act
            StatementDto dto = new StatementDto(0, currentDateTime, "test description dto", 200, "new dto category");
            await sut.AddStatement(dto);

            // Assert
            var category = dbUtils.GetAll<DbCategoryDto>().Single();
            Assert.Equal(dto.Category, category.Name);

            var statement = dbUtils.GetAll<DbStatementDto>().Single();
            Assert.Equal(dto.Amount, statement.Amount);
            Assert.Equal(dto.Description, statement.Description);
            Assert.Equal(dto.DueDate, statement.DueDate);
            Assert.Equal(category.Id, statement.CategoryId);
        }

        private  StatementController BuildStatementController(DateTime? dateTime = null)
        {
            return new StatementController(
                new StatementRepository(dbUtils.DbContext),
                new CategoryRepository(dbUtils.DbContext),
                new BankRepository(dbUtils.DbContext),
                MockDateService(dateTime));
        }

        private static IDateService MockDateService(DateTime? currentDateTime)
        {
            Mock<IDateService> dateServiceMock = new Mock<IDateService>();

            if (currentDateTime.HasValue)
            {
                DateTime date = currentDateTime.Value;
                dateServiceMock.Setup(x => x.GetCurrentDateTime()).Returns(date);
                dateServiceMock.Setup(x => x.GetCurrentMonthYear()).Returns(MonthYear.Create(date.Year, date.Month));
            }

            return dateServiceMock.Object;
        }
    }
}
