using FinpeApi.Integration.DatabaseDTOs;
using FinpeApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FinpeApi.Integration
{
    public class TestsUtils
    {
        private DbUtils dbUtils;

        public TestsUtils(DbUtils dbUtils)
        {
            this.dbUtils = dbUtils;
        }

        public int AddSingleStatement()
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

        public int AddSingleBank()
        {
            CleanAll();

            DbBankDto bank = new DbBankDto()
            {
                Name = "Test bank"
            };
            dbUtils.Insert(bank);

            return bank.Id;
        }

        public void CleanAll()
        {
            dbUtils.DeteleAll<DbStatementDto>();
            dbUtils.DeteleAll<DbCategoryDto>();
            dbUtils.DeteleAll<DbBankStatementDto>();
            dbUtils.DeteleAll<DbBankDto>();
        }

        public FinpeDbContext GetDbContext()
        {
            DbContextOptionsBuilder contextBuidler = new DbContextOptionsBuilder();
            contextBuidler.UseSqlServer(DbUtils.GetConnectionString());
            FinpeDbContext dbContext = new FinpeDbContext(contextBuidler.Options);
            dbContext.Database.Migrate();

            return dbContext;
        }
    }
}
