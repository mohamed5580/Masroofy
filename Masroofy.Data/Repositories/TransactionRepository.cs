using Masroofy.Data.Database;
using Masroofy.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Masroofy.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        public async Task<int> AddAsync(Transaction transaction)
        {
            const string sql = @"
                INSERT INTO Transactions (Amount, Timestamp, CategoryId, BudgetCycleId)
                VALUES (@Amount, @Timestamp, @CategoryId, @BudgetCycleId);
                SELECT last_insert_rowid();";
            var paramAmount = DataAccessLayer.CreateParameter("@Amount", DbType.Decimal, transaction.Amount);
            var paramTimestamp = DataAccessLayer.CreateParameter("@Timestamp", DbType.String, transaction.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
            var paramCat = DataAccessLayer.CreateParameter("@CategoryId", DbType.Int32, transaction.CategoryId);
            var paramCycle = DataAccessLayer.CreateParameter("@BudgetCycleId", DbType.Int32, transaction.BudgetCycleId);

            var result = await DataAccessLayer.ExecuteScalarAsync(sql, CommandType.Text, paramAmount, paramTimestamp, paramCat, paramCycle);
            return Convert.ToInt32(result);
        }

        public async Task<List<Transaction>> GetByCycleIdAsync(int cycleId)
        {
            const string sql = "SELECT * FROM Transactions WHERE BudgetCycleId = @CycleId ORDER BY Timestamp DESC";
            var param = DataAccessLayer.CreateParameter("@CycleId", DbType.Int32, cycleId);
            var list = new List<Transaction>();
            using var reader = await DataAccessLayer.ExecuteReaderAsync(sql, CommandType.Text, param);
            while (await reader.ReadAsync())
            {
                list.Add(new Transaction
                {
                    Id = reader.GetInt32(0),
                    Amount = reader.GetDecimal(1),
                    Timestamp = DateTime.Parse(reader.GetString(2)),
                    CategoryId = reader.GetInt32(3),
                    BudgetCycleId = reader.GetInt32(4)
                });
            }
            return list;
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            const string sql = "SELECT * FROM Transactions WHERE Id = @Id";
            var param = DataAccessLayer.CreateParameter("@Id", DbType.Int32, id);
            using var reader = await DataAccessLayer.ExecuteReaderAsync(sql, CommandType.Text, param);
            if (await reader.ReadAsync())
            {
                return new Transaction
                {
                    Id = reader.GetInt32(0),
                    Amount = reader.GetDecimal(1),
                    Timestamp = DateTime.Parse(reader.GetString(2)),
                    CategoryId = reader.GetInt32(3),
                    BudgetCycleId = reader.GetInt32(4)
                };
            }
            return null;
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            const string sql = "UPDATE Transactions SET Amount=@Amount, CategoryId=@CategoryId WHERE Id=@Id";
            var paramAmount = DataAccessLayer.CreateParameter("@Amount", DbType.Decimal, transaction.Amount);
            var paramCat = DataAccessLayer.CreateParameter("@CategoryId", DbType.Int32, transaction.CategoryId);
            var paramId = DataAccessLayer.CreateParameter("@Id", DbType.Int32, transaction.Id);
            await DataAccessLayer.ExecuteNonQueryAsync(sql, CommandType.Text, paramAmount, paramCat, paramId);
        }

        public async Task DeleteAsync(int id)
        {
            const string sql = "DELETE FROM Transactions WHERE Id = @Id";
            var param = DataAccessLayer.CreateParameter("@Id", DbType.Int32, id);
            await DataAccessLayer.ExecuteNonQueryAsync(sql, CommandType.Text, param);
        }
    }
}