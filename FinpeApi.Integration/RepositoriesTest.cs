using FinpeApi.Banks;
using FinpeApi.Integration.DatabaseDTOs;
using FinpeApi.Statements;
using FinpeApi.ValueObjects;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FinpeApi.Integration
{
    [Collection("IntegrationTests")]
    public class RepositoriesTest
    {
        private DbUtils dbUtils;
        private TestsUtils testsUtils;
        
        public RepositoriesTest()
        {
            dbUtils = new DbUtils();
            testsUtils = new TestsUtils(dbUtils);
        }

        [Fact]
        public void GetBankStatement()
        {
            // Arrange
            int bankId = testsUtils.AddSingleBank();
            dbUtils.Insert(new DbBankStatementDto()
            {
                BankId = bankId,
                Amount = 100,
                ExecutionDate = DateTime.Parse("2018-03-05")
            });
            dbUtils.Insert(new DbBankStatementDto()
            {
                BankId = bankId,
                Amount = 200,
                ExecutionDate = DateTime.Parse("2018-04-05")
            });
            dbUtils.Insert(new DbBankStatementDto()
            {
                BankId = bankId,
                Amount = 300,
                ExecutionDate = DateTime.Parse("2018-05-05")
            });

            var sut = new BankRepository(dbUtils.DbContext);

            // Act
            var statementList = sut.GetList(MonthYear.Create(2018, 4));

            // Assert
            Assert.Equal(1, statementList.Count);
            Assert.Equal(1, statementList.First().BankStatements.Count);
            Assert.Equal(200m, statementList.First().BankStatements.First().Amount.Value);
        }

        [Fact]
        public async Task GetPastPendingStatement()
        {
            // Arrange
            int categoryId = testsUtils.AddSingleCategory();
            dbUtils.Insert(new DbStatementDto()
            {
                Amount = 104,
                CategoryId = categoryId,
                Direction = 0,
                DueDate = DateTime.Parse("2018-04-15")
            });
            dbUtils.Insert(new DbStatementDto()
            {
                Amount = 204,
                CategoryId = categoryId,
                Direction = 0,
                DueDate = DateTime.Parse("2018-04-20"),
                Paid = true,
                PaymentDate = DateTime.Parse("2018-04-20")
            });
            dbUtils.Insert(new DbStatementDto()
            {
                Amount = 105,
                CategoryId = categoryId,
                Direction = 0,
                DueDate = DateTime.Parse("2018-05-15")
            });
            dbUtils.Insert(new DbStatementDto()
            {
                Amount = 205,
                CategoryId = categoryId,
                Direction = 0,
                DueDate = DateTime.Parse("2018-05-20"),
                Paid = true,
                PaymentDate = DateTime.Parse("2018-05-20")
            });
            dbUtils.Insert(new DbStatementDto()
            {
                Amount = 106,
                CategoryId = categoryId,
                Direction = 0,
                DueDate = DateTime.Parse("2018-06-15")
            });
            dbUtils.Insert(new DbStatementDto()
            {
                Amount = 206,
                CategoryId = categoryId,
                Direction = 0,
                DueDate = DateTime.Parse("2018-06-20"),
                Paid = true,
                PaymentDate = DateTime.Parse("2018-06-20")
            });

            var sut = new StatementRepository(dbUtils.DbContext);

            // Act
            var statementList = await sut.GetList(MonthYear.Create(2018, 5));

            // Assert
            Assert.Equal(3, statementList.Count);
            Assert.Contains(statementList, x => x.Amount == 104);
            Assert.Contains(statementList, x => x.Amount == 105);
            Assert.Contains(statementList, x => x.Amount == 205);
        }
    }
}
