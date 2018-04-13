﻿using System;
using System.Collections.Generic;
using System.Linq;
using FinpeApi.Categories;
using FinpeApi.Statements;
using FinpeApi.Utils;

namespace FinpeApi.Overviews
{
    public class OverviewDto
    {
        private MonthYear monthYear;
        private MonthSummary summary;
        private IReadOnlyList<Category> categories;

        public decimal TotalIncome { get; private set; }
        public IReadOnlyList<ExpenseDto> Expenses { get; private set; }
        public IReadOnlyList<StatementDto> PendingStatements { get; private set; }
        public IReadOnlyList<string> Categories { get; private set; }
        public decimal BankAmount { get; private set; }
        public string MonthName { get; private set; }
        public int Year { get; private set; }

        public static OverviewDto Create(MonthYear monthYear, MonthSummary summary, IReadOnlyList<Category>  categories) 
            => new OverviewDto(monthYear, summary, categories);

        private OverviewDto(MonthYear monthYear, MonthSummary summary, IReadOnlyList<Category> categories)
        {
            this.monthYear = monthYear ?? throw new ArgumentNullException("monthYear");
            this.summary = summary ?? throw new ArgumentNullException("summary");
            this.categories = categories ?? throw new ArgumentNullException("categories");

            BuildOverview();
        }

        private void BuildOverview()
        {
            MonthName = monthYear.GetMonthName();
            Year = monthYear.Year;
            TotalIncome = summary.GetTotalIncome();                
            Expenses = FormatExpenses();
            PendingStatements = FormatPendingStatements();
            BankAmount = summary.GetCurrentBalance();
            Categories = GetCategories();
        }

        private IReadOnlyList<StatementDto> FormatPendingStatements() => summary
            .GetPendingStatements()
            .Select(x => new StatementDto(EntityId.Create(x.Id), x.DueDate, x.Description, x.Amount, x.Category))
            .ToList();

        private IReadOnlyList<ExpenseDto> FormatExpenses() => summary
            .GetExpenses()
            .GroupBy(x => x.Category)
            .Select(x => new ExpenseDto(x.Key, x.Sum(values => values.Amount.Value)))
            .ToList();

        private IReadOnlyList<string> GetCategories() => categories
            .Select(x => x.Name)
            .ToList();
    }
}
