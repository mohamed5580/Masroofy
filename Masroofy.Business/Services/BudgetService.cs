using System;
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


        public BudgetService(ITransactionRepository transactionRepo, IBudgetCycleRepository cycleRepo, RolloverEngine rolloverEngine)
        {
            _transactionRepo = transactionRepo;
            _cycleRepo = cycleRepo;
            _rolloverEngine = rolloverEngine;
        }
        public async Task<int> CreateCycleAsync(BudgetCycle cycle)
        {
            await _cycleRepo.DeactivateCurrentCycleAsync();
            return await _cycleRepo.CreateAsync(cycle);
        }
        public async Task<decimal> GetSafeDailyLimitAsync(int cycleId)
        {
            var cycle = await _cycleRepo.GetByIdAsync(cycleId);
            if (cycle == null) return 0;
            var transactions = await _transactionRepo.GetByCycleIdAsync(cycleId);
            var (remainingBalance, remainingDays) = _rolloverEngine.CalculateRemaining(cycle, transactions, DateTime.Today);
            if (remainingDays <= 0) return remainingBalance;
            return Math.Round(remainingBalance / remainingDays, 2);
        }

        public async Task<(decimal newLimit, decimal totalSpent, decimal percentage)> RecalculateAfterExpenseAsync(int cycleId)
        {
            var cycle = await _cycleRepo.GetByIdAsync(cycleId);
            if (cycle == null) return (0, 0, 0);
            var transactions = await _transactionRepo.GetByCycleIdAsync(cycleId);
            decimal totalSpent = transactions.Sum(t => t.Amount);
            var (remainingBalance, remainingDays) = _rolloverEngine.CalculateRemaining(cycle, transactions, DateTime.Today);
            decimal newLimit = remainingDays > 0 ? remainingBalance / remainingDays : remainingBalance;
            decimal percentage = totalSpent / cycle.TotalAllowance * 100;
            return (Math.Round(newLimit, 2), totalSpent, percentage);
        }

        public async Task<decimal> GetTotalSpentAsync(int cycleId)
        {
            var transactions = await _transactionRepo.GetByCycleIdAsync(cycleId);
            return transactions.Sum(t => t.Amount);
        }
    }
}