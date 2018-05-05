using FinpeApi.Integration.DatabaseDTOs;
using System;

namespace FinpeApi.Integration
{
    public class TestsUtils
    {
        private DbUtils dbUtils;

        public static int CLOSING_DAY = 15;
        public static int PAYMENT_DAY = 20;
        public static DateTime DEFAULT_DATE = DateTime.Parse("2018-04-15");

        public TestsUtils(DbUtils dbUtils)
        {
            this.dbUtils = dbUtils;
        }

        public int AddSingleCategory()
        {
            CleanAll(); ;

            DbCategoryDto category = new DbCategoryDto()
            {
                Name = "TestCategory"
            };
            dbUtils.Insert(category);

            return category.Id;
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
                DueDate = DEFAULT_DATE,
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

        public DbCreditCardDto AddSingleCreditCard()
        {
            int categoryId = AddSingleCategory();

            DbCreditCardDto creditCard = new DbCreditCardDto()
            {
                Name = "Test bank",
                ClosingDay = CLOSING_DAY,
                CategoryId = categoryId,
                PaymentDay = PAYMENT_DAY,
                EndNumbers = 4321,
                Owner = "teste@teste.com"

            };
            dbUtils.Insert(creditCard);

            return creditCard;
        }

        public void CleanAll()
        {
            dbUtils.DeteleAll<DbStatementDto>();
            dbUtils.DeteleAll<DbBankStatementDto>();
            dbUtils.DeteleAll<DbBankDto>();
            dbUtils.DeteleAll<DbCreditCardStatementDto>();
            dbUtils.DeteleAll<DbCreditCardBillDto>();
            dbUtils.DeteleAll<DbCreditCardDto>();
            dbUtils.DeteleAll<DbCategoryDto>();
        }
    }
}
