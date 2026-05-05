using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Masroofy.Data.Models;

namespace Masroofy.Data.Repositories
{
    public interface IBudgetCycleRepository
    {
        Task<int> CreateAsync(BudgetCycle cycle);
        Task<BudgetCycle?> GetActiveCycleAsync();
        Task<List<BudgetCycle>> GetAllCyclesAsync();
        Task<decimal> GetTotalCycleAsync();
        Task<BudgetCycle?> GetByIdAsync(int id);
        Task<List<Category?>> GetByCategoryAsync(string name);
        Task UpdateAsync(BudgetCycle cycle);
        Task DeactivateCurrentCycleAsync();
        Task DeleteAsync(int id);
    }
}