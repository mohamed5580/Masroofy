using System;
using System.Threading.Tasks;
using Masroofy.Business.Services;
using Masroofy.Data.Repositories;
using Masroofy.Data.Models;

namespace Masroofy.UI
{
    public class LoggingUIController
    {
        private readonly ValidationService _validationService;
        private readonly ITransactionRepository _repository;
        private readonly BudgetService _budgetService;

        // FIX: holds the live singleton dashboard reference so we can
        // trigger a full sequence-diagram refresh after every save.
        private readonly StatisticsDashbourd? _dashboard;

        public LoggingUIController(
            ValidationService vs,
            ITransactionRepository repo,
            BudgetService bs,
            StatisticsDashbourd? dashboard = null)
        {
            _validationService = vs;
            _repository = repo;
            _budgetService = bs;
            _dashboard = dashboard;
        }

        public async Task<bool> OnSaveTapped(string amountText, int categoryId, int budgetCycleId)
        {
            if (!decimal.TryParse(amountText, out decimal amount))
                return false;

            if (!_validationService.IsValidAmount(amount))
                return false;

            // 1. Persist transaction to SQLite
            await _repository.AddAsync(new Transaction
            {
                Amount = amount,
                Timestamp = DateTime.Now,
                CategoryId = categoryId,
                BudgetCycleId = budgetCycleId
            });

            // 2. Re-run the full US#3 sequence diagram flow on the live dashboard:
            //    calculateRemainingBalance → calculateSafeDailyLimit
            //    → refresh() → display() → [opt] showFinalDayBadge()
            _dashboard?.RefreshDashboardData();

            return true;
        }
    }
}