using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Masroofy.Business.Services;

namespace Masroofy.UI
{
   
    public class DashboardUIController
    {
        private readonly StatisticsDashbourd _dashboardScreen;

        private readonly BudgetService _budgetService;

        public DashboardUIController(BudgetService budgetService, StatisticsDashbourd dashboardScreen )
        {
            _budgetService = budgetService;
            _dashboardScreen = dashboardScreen;
        }

        public async Task OnOpen(int activeCycleId)
        {
            var (remainingBalance, remainingDays) =
                await CalculateRemainingBalance(activeCycleId);

            float safeDailyLimit = CalculateSafeDailyLimit(remainingBalance, remainingDays);

            _dashboardScreen.Refresh();

            var categoryTotals = await _budgetService.GetCategoryBreakdownAsync(activeCycleId);
            _dashboardScreen.Display(safeDailyLimit, (float)remainingBalance, categoryTotals);

            if (remainingDays == 1)
                _dashboardScreen.ShowFinalDayBadge();
        }

        private async Task<(decimal remainingBalance, int remainingDays)>
            CalculateRemainingBalance(int cycleId)
        {
            return await _budgetService.FindAllAndCalculateRemainingAsync(cycleId);
        }

        private float CalculateSafeDailyLimit(decimal remainingBalance, int remainingDays)
        {
            if (remainingDays <= 0) return (float)remainingBalance;
            return (float)Math.Round(remainingBalance / remainingDays, 2);
        }
    }
}