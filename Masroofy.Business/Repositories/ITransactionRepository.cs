using Masroofy.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Masroofy.Data.Repositories
{
    public interface ITransactionRepository
    {
        Task<int> AddAsync(Transaction transaction);
        Task<List<Transaction>> GetByCycleIdAsync(int cycleId);
        Task<Transaction?> GetByIdAsync(int id);
        Task<List<Transaction>> GetHistoryAsync(int cycleId);
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(int id);
        Task<decimal> GetTotalspendingAsync();

    }
}