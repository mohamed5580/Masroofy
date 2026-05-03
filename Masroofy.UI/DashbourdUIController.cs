using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Masroofy.Business.Services;

namespace Masroofy.UI
{
    /// <summary>
    /// US#3 – Dynamic Daily Limit View.
    /// Follows the sequence diagram step-for-step:
    ///
    ///   User → onOpen()
    ///     → calculateRemainingBalance()   → BudgetService.FindAllAndCalculateRemainingAsync()
    ///                                       → TransactionRepository → SQLite → List<Transaction>
    ///                                       ← remainingBalance: float
    ///     → calculateSafeDailyLimit()     ← safeDailyLimit: float
    ///   → DashboardScreen.refresh()
    ///   → DashboardScreen.display(safeDailyLimit, remainingBalance)
    ///   → [opt] if Final Day → DashboardScreen.showFinalDayBadge()
    /// </summary>
    public class DashboardUIController
    {
        private readonly StatisticsDashbourd _dashboardScreen;
        private readonly BudgetService _budgetService;

        public DashboardUIController(BudgetService budgetService, StatisticsDashbourd dashboardScreen)
        {
            _budgetService = budgetService;
            _dashboardScreen = dashboardScreen;
        }

        // ── onOpen() ─────────────────────────────────────────────────────────
        public async Task OnOpen(int activeCycleId)
        {
            // Step 1: calculateRemainingBalance()
            var (remainingBalance, remainingDays) =
                await CalculateRemainingBalance(activeCycleId);

            // Step 2: calculateSafeDailyLimit()
            float safeDailyLimit = CalculateSafeDailyLimit(remainingBalance, remainingDays);

            // Step 3: DashboardScreen.refresh()
            _dashboardScreen.Refresh();

            // Step 4: DashboardScreen.display(safeDailyLimit: float, remainingBalance: float)
            var categoryTotals = await _budgetService.GetCategoryBreakdownAsync(activeCycleId);
            _dashboardScreen.Display(safeDailyLimit, (float)remainingBalance, categoryTotals);

            // Step 5 [opt]: Final Day of Cycle → showFinalDayBadge()
            if (remainingDays == 1)
                _dashboardScreen.ShowFinalDayBadge();
        }

        // ── calculateRemainingBalance() ───────────────────────────────────────
        private async Task<(decimal remainingBalance, int remainingDays)>
            CalculateRemainingBalance(int cycleId)
        {
            // Calls BudgetService.FindAllAndCalculateRemainingAsync()
            // which hits TransactionRepository → SQLite → List<Transaction>
            return await _budgetService.FindAllAndCalculateRemainingAsync(cycleId);
        }

        // ── calculateSafeDailyLimit() ─────────────────────────────────────────
        private float CalculateSafeDailyLimit(decimal remainingBalance, int remainingDays)
        {
            if (remainingDays <= 0) return (float)remainingBalance;
            return (float)Math.Round(remainingBalance / remainingDays, 2);
        }
    }
}