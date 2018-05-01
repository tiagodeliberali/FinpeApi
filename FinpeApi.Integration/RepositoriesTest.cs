﻿using FinpeApi.Banks;
using FinpeApi.Integration.DatabaseDTOs;
using FinpeApi.Models;
using FinpeApi.ValueObjects;
using System;
using System.Linq;
using Xunit;

namespace FinpeApi.Integration
{
    public class RepositoriesTest
    {
        private DbUtils dbUtils;
        private TestsUtils testsUtils;
        private FinpeDbContext db;

        public RepositoriesTest()
        {
            dbUtils = new DbUtils();
            testsUtils = new TestsUtils(dbUtils);
            SetDbContext();
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

            var sut = new BankRepository(db);

            // Act
            var statementList = sut.GetList(MonthYear.Create(2018, 4));

            // Assert
            Assert.Equal(1, statementList.Count);
            Assert.Equal(1, statementList.First().BankStatements.Count);
            Assert.Equal(200m, statementList.First().BankStatements.First().Amount.Value);
        }

        private void SetDbContext()
        {
            if (db == null)
            {
                db = testsUtils.GetDbContext();
            }
        }
    }
}