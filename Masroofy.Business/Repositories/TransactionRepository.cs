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
            string sql = DataAccessLayer.Provider switch
            {
                DatabaseProvider.SQLite => @"
                INSERT INTO Transactions (Amount, Timestamp, CategoryId, BudgetCycleId)
                VALUES (@Amount, @Timestamp, @CategoryId, @BudgetCycleId);
                SELECT last_insert_rowid();",

                DatabaseProvider.SqlServer => @"
                INSERT INTO Transactions (Amount, Timestamp, CategoryId, BudgetCycleId)
                VALUES (@Amount, @Timestamp, @CategoryId, @BudgetCycleId);
                SELECT SCOPE_IDENTITY();",

                DatabaseProvider.MySQL => @"
                INSERT INTO Transactions (Amount, Timestamp, CategoryId, BudgetCycleId)
                VALUES (@Amount, @Timestamp, @CategoryId, @BudgetCycleId);
                SELECT LAST_INSERT_ID();",

                _ => throw new NotSupportedException()
            };

            var parameters = new[]
            {
            DataAccessLayer.CreateParameter("@Amount", DbType.Decimal, transaction.Amount),
            DataAccessLayer.CreateParameter("@Timestamp", DbType.DateTime, transaction.Timestamp),
            DataAccessLayer.CreateParameter("@CategoryId", DbType.Int32, transaction.CategoryId),
            DataAccessLayer.CreateParameter("@BudgetCycleId", DbType.Int32, transaction.BudgetCycleId)
        };

            var result = await DataAccessLayer.ExecuteScalarAsync(sql, CommandType.Text, parameters);

            return Convert.ToInt32(result);
        }

        public async Task<List<Transaction>> GetByCycleIdAsync(int cycleId)
        {
            const string sql = @"
            SELECT Id, Amount, Timestamp, CategoryId, BudgetCycleId
            FROM Transactions
            WHERE BudgetCycleId = @Id
            ORDER BY Timestamp DESC";

            var param = DataAccessLayer.CreateParameter("@Id", DbType.Int32, cycleId);

            var list = new List<Transaction>();

            using var reader = await DataAccessLayer.ExecuteReaderAsync(sql, CommandType.Text, param);

            while (await reader.ReadAsync())
            {
                list.Add(new Transaction
                {
                    Id = reader.GetInt32(0),
                    Amount = reader.GetDecimal(1),
                    Timestamp = reader.GetDateTime(2), // ✔ FIX
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

            var param = DataAccessLayer.CreateParameter("@CycleId", DbType.Int32, cycleId);

            var list = new List<Transaction>();

            using var reader = await DataAccessLayer.ExecuteReaderAsync(sql, CommandType.Text, param);

            while (await reader.ReadAsync())
            {
                list.Add(new Transaction
                {
                    Id = reader.GetInt32(0),
                    Amount = reader.GetDecimal(1),
                    Timestamp = reader.GetDateTime(2),
                    BudgetCycleId = reader.GetInt32(3),
                    CategoryName = reader.GetString(4).ToString() // أو null
                });
            }

            return list;
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            const string sql = @"
            SELECT Id, Amount, Timestamp, CategoryId, BudgetCycleId
            FROM Transactions
            WHERE Id = @Id";

            var param = DataAccessLayer.CreateParameter("@Id", DbType.Int32, id);

            using var reader = await DataAccessLayer.ExecuteReaderAsync(sql, CommandType.Text, param);

            if (await reader.ReadAsync())
            {
                return new Transaction
                {
                    Id = reader.GetInt32(0),
                    Amount = reader.GetDecimal(1),
                    Timestamp = reader.GetDateTime(2),
                    CategoryId = reader.GetInt32(3),
                    BudgetCycleId = reader.GetInt32(4)
                };
            }

            return null;
        }

        public async Task UpdateAsync(Transaction t)
        {
            // بنعمل Update للمبلغ (Amount) بناءً على الـ ID[cite: 5]
            const string sql = "UPDATE Transactions SET Amount = @Amount WHERE Id = @Id";

            var p1 = DataAccessLayer.CreateParameter("@Amount", DbType.Decimal, t.Amount);
            var p2 = DataAccessLayer.CreateParameter("@Id", DbType.Int32, t.Id);

            await DataAccessLayer.ExecuteNonQueryAsync(sql, CommandType.Text, p1, p2);
        }
        public async Task DeleteAsync(int id)
        {
            const string sql = "DELETE FROM Transactions WHERE Id = @Id";

            var param = DataAccessLayer.CreateParameter("@Id", DbType.Int32, id);

            await DataAccessLayer.ExecuteNonQueryAsync(sql, CommandType.Text, param);
        }

        public async Task<decimal> GetTotalspendingAsync()
        {
            const string sql = "SELECT COALESCE(SUM(Amount), 0) FROM Transaction";

            using var reader = await DataAccessLayer.ExecuteReaderAsync(sql, CommandType.Text);

            if (await reader.ReadAsync())
                return reader.IsDBNull(0) ? 0 : reader.GetDecimal(0);

            return 0;
        }
    }
}