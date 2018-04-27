using FinpeApi.Banks;
using FinpeApi.Categories;
using FinpeApi.Models;
using FinpeApi.Statements;
using FinpeApi.Integration.DatabaseDTOs;
using FinpeApi.Utils;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FinpeApi.Integration
{
    public class StatementControllerTest
    {
        private FinpeDbContext db;
        private DbUtils dbUtils;
        private const string connectionString = "Server=tcp:database,1433;Initial Catalog=finpedb;Persist Security Info=False;User ID=SA;Password=P24d!dBX!qRf;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

        public StatementControllerTest()
        {
            db = BuildDbContext();
            db.Database.Migrate();

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            dbUtils = new DbUtils(connection);
        }

        private static FinpeDbContext BuildDbContext()
        {
            DbContextOptionsBuilder contextBuidler = new DbContextOptionsBuilder();
            contextBuidler.UseSqlServer(connectionString);
            FinpeDbContext db = new FinpeDbContext(contextBuidler.Options);
            return db;
        }

        [Fact]
        public async Task MarkStatementAsPaid()
        {
            // Arrange
            int statementId = AddSingleStatement();
            DateTime currentDateTime = DateTime.Parse("2018-04-21");
            
            var sut = new StatementController(
                new StatementRepository(db),
                new CategoryRepository(db),
                new BankRepository(db),
                MockDateService(currentDateTime)
            );

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
            int statementId = AddSingleStatement();
            
            var sut = new StatementController(
                new StatementRepository(db),
                new CategoryRepository(db),
                new BankRepository(db),
                new Mock<IDateService>().Object
            );

            // Act
            decimal amount = 150.53m;
            await sut.UpdateAmount(statementId, amount);

            // Assert
            var statement = dbUtils.GetAll<DbStatementDto>().Single();
            Assert.Equal(amount, statement.Amount);
        }

        [Fact]
        public async Task AddStatement()
        {
            // Arrange
            CleanAll();
            DateTime currentDateTime = DateTime.Parse("2018-04-21");

            var sut = new StatementController(
                new StatementRepository(db),
                new CategoryRepository(db),
                new BankRepository(db),
                new Mock<IDateService>().Object
            );

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

        private int AddSingleStatement()
        {
            CleanAll();

            DbCategoryDto category = new DbCategoryDto()
            {
                Name = "TestCategory"
            };
            dbUtils.Insert(category);

            DbStatementDto statement = new DbStatementDto()
            {
                Amount = 10,
                Direction = 0,
                DueDate = DateTime.Parse("2018-04-15"),
                Paid = false,
                CategoryId = category.Id,
                Description = "Test Description"
            };
            dbUtils.Insert(statement);

            return statement.Id;
        }

        private void CleanAll()
        {
            dbUtils.DeteleAll<DbStatementDto>();
            dbUtils.DeteleAll<DbCategoryDto>();
        }

        private static IDateService MockDateService(DateTime currentDateTime)
        {
            Mock<IDateService> dateServiceMock = new Mock<IDateService>();
            dateServiceMock.Setup(x => x.GetCurrentDateTime()).Returns(currentDateTime);

            return dateServiceMock.Object;
        }
    }
}
