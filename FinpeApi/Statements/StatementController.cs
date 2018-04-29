using FinpeApi.Banks;
using FinpeApi.Categories;
using FinpeApi.Overviews;
using FinpeApi.Utils;
using FinpeApi.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinpeApi.Statements
{
    public class StatementController
    {
        private StatementRepository statementRepository;
        private CategoryRepository categoryRepository;
        private BankRepository bankRepository;
        private IDateService dateService;

        public StatementController(StatementRepository statementRepository,
            CategoryRepository categoryRepository,
            BankRepository bankRepository,
            IDateService dateService)
        {
            this.statementRepository = statementRepository;
            this.categoryRepository = categoryRepository;
            this.bankRepository = bankRepository;
            this.dateService = dateService;
        }

        public async Task MarkStatementPaid(int id)
        {
            Statement dbStatement = await statementRepository.Get(id);
            dbStatement.MarkAsPaid(dateService.GetCurrentDateTime());
            await statementRepository.Save(dbStatement);
        }

        public async Task UpdateAmount(int id, decimal amount)
        {
            Statement dbStatement = await statementRepository.Get(id);
            dbStatement.UpdateAmount(MoneyAmount.Create(amount));
            await statementRepository.Save(dbStatement);
        }

        public async Task AddStatement(StatementDto statement)
        {
            var category = await categoryRepository.Get(statement.Category);
            if (category == null)
            {
                category = Category.Create(statement.Category);
                await categoryRepository.Save(category);
            }

            var dbStatement = Statement.CreateOutcome(
                statement.Description,
                statement.Amount,
                statement.DueDate,
                category);

            await statementRepository.Save(dbStatement);
        }

        public async Task UpdateBalance(decimal amount)
        {
            var bank = bankRepository.GetList().First();
            var statement = bank.NewBankStatement(amount, dateService.GetCurrentDateTime());

            await bankRepository.SaveStatement(statement);
        }

        public async Task<OverviewDto> BuildCurrentMonth()
        {
            MonthYear monthYear = dateService.GetCurrentMonthYear();

            IReadOnlyList<Statement> statements = await statementRepository.GetList(monthYear);
            IReadOnlyList<Bank> banks = bankRepository.GetList();
            IReadOnlyList<Category> categories = categoryRepository.GetList();

            MonthSummary summary = new MonthSummary(statements, banks);

            return OverviewDto.Create(monthYear, summary, categories);
        }
    }
}
