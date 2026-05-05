using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Masroofy.Data.Models;
using Masroofy.Data.Repositories;

namespace Masroofy.Business.Services
{
    public class BudgetService
    {
        private readonly ITransactionRepository _transactionRepo;
        private readonly IBudgetCycleRepository _cycleRepo;
        private readonly RolloverEngine _rolloverEngine;

        public BudgetService(
            ITransactionRepository transactionRepo,
            IBudgetCycleRepository cycleRepo,
            RolloverEngine rolloverEngine)
        {
            _transactionRepo = transactionRepo;
            _cycleRepo = cycleRepo;
            _rolloverEngine = rolloverEngine;
        }

        public async Task<(decimal remainingBalance, int remainingDays)>
            FindAllAndCalculateRemainingAsync(int cycleId)
        {
            var cycle = await _cycleRepo.GetByIdAsync(cycleId);
            if (cycle == null) return (0, 1);

            var transactions = await _transactionRepo.GetByCycleIdAsync(cycleId);
            return _rolloverEngine.CalculateRemaining(cycle, transactions, DateTime.Today);
        }
        public async Task<List<Category>?> GetByNameAsync(string name)
        {
            return await _cycleRepo.GetByCategoryAsync(name);
        }

        public async Task<(decimal newLimit, decimal totalSpent, decimal percentage)>
            RecalculateAfterExpenseAsync(int cycleId)
        {
            var cycle = await _cycleRepo.GetByIdAsync(cycleId);
            if (cycle == null) return (0, 0, 0);

            var transactions = await _transactionRepo.GetByCycleIdAsync(cycleId);
            decimal totalSpent = transactions.Sum(t => t.Amount);

            var (remainingBalance, remainingDays) =
                _rolloverEngine.CalculateRemaining(cycle, transactions, DateTime.Today);

            // DO NOT write cycle.TotalAllowance back — it is the original budget.
            decimal newLimit = remainingDays > 0
                ? Math.Round(remainingBalance / remainingDays, 2)
                : Math.Round(remainingBalance, 2);

            // Use original cycle.TotalAllowance (unchanged) for correct percentage
            decimal percentage = cycle.TotalAllowance > 0
                ? Math.Round(totalSpent / cycle.TotalAllowance * 100, 1)
                : 0;

            return (newLimit, totalSpent, percentage);
        }

        public async Task<int> CreateCycleAsync(BudgetCycle cycle)
        {
            await _cycleRepo.DeactivateCurrentCycleAsync();
            return await _cycleRepo.CreateAsync(cycle);
        }

        public async Task<decimal> GetSafeDailyLimitAsync(int cycleId)
        {
            var (remainingBalance, remainingDays) =
                await FindAllAndCalculateRemainingAsync(cycleId);
            return remainingDays > 0
                ? Math.Round(remainingBalance / remainingDays, 2)
                : remainingBalance;
        }

        public async Task<decimal> GetTotalSpentAsync(int cycleId)
        {
            var transactions = await _transactionRepo.GetByCycleIdAsync(cycleId);
            return transactions.Sum(t => t.Amount);
        }

        public async Task<Dictionary<string, decimal>> GetCategoryBreakdownAsync(int cycleId)
        {
            var transactions = await _transactionRepo.GetByCycleIdAsync(cycleId);
            return transactions
                .GroupBy(t => t.CategoryId)
                .ToDictionary(
                    g => MapIdToCategoryName(g.Key),
                    g => g.Sum(t => t.Amount)
                );
        }

        public async Task<decimal> GetTotalCycleAsync()
        {
            return await _cycleRepo.GetTotalCycleAsync();
        }

        private string MapIdToCategoryName(int id) => id switch
        {
            1 => "Food",
            2 => "Transport",
            3 => "Entertainment",
            4 => "Utilities",
            _ => "Other"
        };
    }
}