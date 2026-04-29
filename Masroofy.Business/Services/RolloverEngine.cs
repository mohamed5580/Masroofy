using System;
using System.Collections.Generic;
using System.Linq;
using Masroofy.Data.Models;

namespace Masroofy.Business.Services
{
    public class RolloverEngine
    {
        public (decimal, int ) CalculateRemaining(BudgetCycle cycle, List<Transaction> transactions, DateTime currentDate)
        {
            decimal totalSpent = transactions.Sum(t => t.Amount);
            decimal remainingBalance = cycle.TotalAllowance - totalSpent;
            int remainingDays = (int)Math.Ceiling((cycle.EndDate - currentDate).TotalDays);
            if (remainingDays <= 0) remainingDays = 1;
            return (remainingBalance, remainingDays);
        }

        public decimal GetNewDailyLimit(decimal remainingBalance, int remainingDays) => remainingBalance / remainingDays;
    }
}