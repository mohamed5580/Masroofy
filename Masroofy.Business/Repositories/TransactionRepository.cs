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

            var p1 = DataAccessLayer.CreateParameter("@Amount", DbType.Decimal, transaction.Amount);
            var p2 = DataAccessLayer.CreateParameter("@Timestamp", DbType.String, transaction.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
            var p3 = DataAccessLayer.CreateParameter("@CategoryId", DbType.Int32, transaction.CategoryId);
            var p4 = DataAccessLayer.CreateParameter("@BudgetCycleId", DbType.Int32, transaction.BudgetCycleId);

            var result = await DataAccessLayer.ExecuteScalarAsync(sql, CommandType.Text, p1, p2, p3, p4);
            return Convert.ToInt32(result);
        }

        public async Task<List<Transaction>> GetByCycleIdAsync(int cycleId)
        {
            const string sql = "SELECT * FROM Transactions WHERE BudgetCycleId = @Id ORDER BY Timestamp DESC";
            var p = DataAccessLayer.CreateParameter("@Id", DbType.Int32, cycleId);

            var list = new List<Transaction>();

            using var reader = await DataAccessLayer.ExecuteReaderAsync(sql, CommandType.Text, p);

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

        
        public async Task<List<Transaction>> GetHistoryAsync(int cycleId)
        {
            const string sql = @"
                SELECT t.Id, t.Amount, t.Timestamp, t.BudgetCycleId, c.Name
                FROM Transactions t
                JOIN Categories c ON t.CategoryId = c.Id
                ORDER BY t.Timestamp DESC";

            var list = new List<Transaction>();

            
            using var reader = await DataAccessLayer.ExecuteReaderAsync(sql, CommandType.Text);

            while (await reader.ReadAsync())
            {
                list.Add(new Transaction
                {
                    Id = reader.GetInt32(0),
                    Amount = reader.GetDecimal(1),
                    Timestamp = DateTime.Parse(reader.GetString(2)),
                    BudgetCycleId = reader.GetInt32(3),
                    CategoryName = reader.GetString(4)
                });
            }

            return list;
        }

        public async Task<Transaction?> GetByIdAsync(int id) { return null; }
        public async Task UpdateAsync(Transaction t)
        {
            const string sql = "UPDATE Transactions SET Amount = @Amount WHERE Id = @Id";

            var p1 = DataAccessLayer.CreateParameter("@Amount", DbType.Decimal, t.Amount);
            var p2 = DataAccessLayer.CreateParameter("@Id", DbType.Int32, t.Id);

            await DataAccessLayer.ExecuteNonQueryAsync(sql, CommandType.Text, p1, p2);
        }

        public async Task DeleteAsync(int id)
        {
            const string sql = "DELETE FROM Transactions WHERE Id = @Id";
            var p = DataAccessLayer.CreateParameter("@Id", DbType.Int32, id);

            await DataAccessLayer.ExecuteNonQueryAsync(sql, CommandType.Text, p);
        }

    }
}