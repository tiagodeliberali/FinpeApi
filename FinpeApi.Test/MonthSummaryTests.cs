using FinpeApi.Banks;
using FinpeApi.Categories;
using FinpeApi.Statements;
using FinpeApi.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FinpeApi.Test
{
    public class MonthSummaryTests
    {
        private const string IncomeCategoryName = "outcome-category";
        private const string IncomePaidCategoryName = "paid-outcome-category";

        [Fact]
        public void GetTotalIncome()
        {
            // Arrange
            MonthSummary summary = BuildSummary();

            // Act
            decimal total = summary.GetTotalIncome();

            // Assert
            Assert.Equal(1300.50m, total);
        }

        [Fact]
        public void GetPendingExpenses()
        {
            // Arrange
            MonthSummary summary = BuildSummary();

            // Act
            var statements = summary.GetPendingExpenses();

            // Assert
            Assert.Equal(2, statements.Count);
            Assert.Equal(621.10m, statements.Sum(x => x.Amount.Value));
        }

        [Fact]
        public void GetExpenses()
        {
            // Arrange
            MonthSummary summary = BuildSummary();

            // Act
            var statements = summary.GetExpenses();

            // Assert
            Assert.Equal(4, statements.Count);
            Assert.Equal(621.10m, statements.Where(x => x.Category.Name == IncomeCategoryName).Sum(x => x.Amount.Value));
            Assert.Equal(500.20m, statements.Where(x => x.Category.Name == IncomePaidCategoryName).Sum(x => x.Amount.Value));
        }

        [Fact]
        public void GetCurrentBalance_OnlyBankStatements()
        {
            // Arrange
            var statementList = new List<Statement>();
            List<Bank> bankList = BuildBankWithStatements(DateTime.Parse("2018-04-01"));

            var summary = new MonthSummary(statementList, bankList);

            // Act
            decimal result = summary.GetCurrentBalance();

            // Assert
            Assert.Equal(401m, result);
        }

        [Fact]
        public void GetCurrentBalance_MultipleStatementsInOneBank()
        {
            // Arrange
            var statementList = new List<Statement>();
            var bankList = new List<Bank>()
            {
                BuildBank(1, new List<BankStatement> {
                    BuildBankStatement(200.50m, DateTime.Parse("2018-04-01")),
                    BuildBankStatement(300.80m, DateTime.Parse("2018-04-02"))
                })
            };

            var summary = new MonthSummary(statementList, bankList);

            // Act
            decimal result = summary.GetCurrentBalance();

            // Assert
            Assert.Equal(300.80m, result);
        }

        [Fact]
        public void GetCurrentBalance_FutureBankStatementsAndPaidStatements()
        {
            // Arrange
            var statementList = BuildStatementList();
            List<Bank> bankList = BuildBankWithStatements(DateTime.Parse("2018-04-20"));

            var summary = new MonthSummary(statementList, bankList);

            // Act
            decimal result = summary.GetCurrentBalance();

            // Assert
            Assert.Equal(401m, result);
        }

        [Fact]
        public void GetCurrentBalance_PastBankStatementsAndPaidStatements()
        {
            // Arrange
            var statementList = BuildStatementList();
            List<Bank> bankList = BuildBankWithStatements(DateTime.Parse("2018-04-01"));

            var summary = new MonthSummary(statementList, bankList);

            // Act
            decimal result = summary.GetCurrentBalance();

            // Assert
            Assert.Equal(-99.20m, result);
        }

        private IReadOnlyList<Statement> BuildStatementList()
        {
            return new List<Statement>()
            {
                CreateIncome(300),
                CreateIncome(1000.50m),
                CreateOutcome(500.80m),
                CreateOutcome(120.30m),
                CreatePaidOutcome(400),
                CreatePaidOutcome(100.20m)
            };
        }

        private MonthSummary BuildSummary()
        {
            var statementList = BuildStatementList();
            var bankList = new List<Bank>();

            return new MonthSummary(statementList, bankList);
        }

        private Statement CreateIncome(decimal amount)
        {
            var date = DateTime.Parse("2018-04-14");
            var category = Category.Create("income-category");
            category.SetId(1);

            var statement = Statement.CreateOutcome(StatementDescription.Create("Income"), MoneyAmount.Create(amount), date, category);
            statement.SetProperty("Direction", StatementDirection.Income);

            return statement;
        }

        private Statement CreateOutcome(decimal amount)
        {
            var date = DateTime.Parse("2018-04-15");
            var category = Category.Create(IncomeCategoryName);
            category.SetId(1);

            var statement = Statement.CreateOutcome(StatementDescription.Create("Income"), MoneyAmount.Create(amount), date, category);
            
            return statement;
        }

        private Statement CreatePaidOutcome(decimal amount)
        {
            var date = DateTime.Parse("2018-04-16");
            var category = Category.Create(IncomePaidCategoryName);
            category.SetId(1);

            var statement = Statement.CreateOutcome(StatementDescription.Create("Income"), MoneyAmount.Create(amount), date, category);
            statement.MarkAsPaid(date);

            return statement;
        }

        private List<Bank> BuildBankWithStatements(DateTime date)
        {
            decimal balance = 200.50m;

            var bankList = new List<Bank>()
            {
                BuildBank(1, new List<BankStatement> { BuildBankStatement(balance, date) }),
                BuildBank(2, new List<BankStatement> { BuildBankStatement(balance, date) })
            };
            return bankList;
        }

        private Bank BuildBank(int bankId, List<BankStatement> statements)
        {
            var bank = new Bank();
            bank.SetId(bankId);
            bank.SetProperty("BankStatements", statements);

            return bank;
        }

        private BankStatement BuildBankStatement(decimal balance, DateTime date)
        {
            var bankStatement = new BankStatement();
            bankStatement.SetProperty("Amount", MoneyAmount.Create(balance));
            bankStatement.SetProperty("ExecutionDate", date);

            return bankStatement;
        }
    }
}
