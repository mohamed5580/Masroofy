using Masroofy.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Masroofy.Data.Database;
namespace Masroofy.Data.Repositories
{
    public class BudgetCycleRepository : IBudgetCycleRepository
    {


        public async Task<int> CreateAsync(BudgetCycle cycle)
        {
            string sql = DataAccessLayer.Provider switch
            {
                DatabaseProvider.SQLite => @"
            INSERT INTO BudgetCycles (TotalAllowance, StartDate, EndDate, IsActive)
            VALUES (@TotalAllowance, @StartDate, @EndDate, @IsActive);
            SELECT last_insert_rowid();",

                DatabaseProvider.SqlServer => @"
            INSERT INTO BudgetCycles (TotalAllowance, StartDate, EndDate, IsActive)
            VALUES (@TotalAllowance, @StartDate, @EndDate, @IsActive);
            SELECT SCOPE_IDENTITY();",

                DatabaseProvider.MySQL => @"
            INSERT INTO BudgetCycles (TotalAllowance, StartDate, EndDate, IsActive)
            VALUES (@TotalAllowance, @StartDate, @EndDate, @IsActive);
            SELECT LAST_INSERT_ID();",

                _ => throw new NotSupportedException()
            };

            var parameters = new[]
            {
        DataAccessLayer.CreateParameter("@TotalAllowance", DbType.Decimal, cycle.TotalAllowance),
        DataAccessLayer.CreateParameter("@StartDate", DbType.DateTime, cycle.StartDate),
        DataAccessLayer.CreateParameter("@EndDate", DbType.DateTime, cycle.EndDate),
        DataAccessLayer.CreateParameter("@IsActive", DbType.Boolean, cycle.IsActive)
    };

            var result = await DataAccessLayer.ExecuteScalarAsync(sql, CommandType.Text, parameters);

            return Convert.ToInt32(result);
        }

        public async Task<BudgetCycle?> GetActiveCycleAsync()
        {
            const string sql = @"
        SELECT Id, TotalAllowance, StartDate, EndDate, IsActive 
        FROM BudgetCycles 
        WHERE IsActive = 1";

            using var reader = await DataAccessLayer.ExecuteReaderAsync(sql, CommandType.Text);

            if (await reader.ReadAsync())
            {
                return new BudgetCycle
                {
                    Id = reader.GetInt32(0),
                    TotalAllowance = reader.GetDecimal(1),
                    StartDate = reader.GetDateTime(2),
                    EndDate = reader.GetDateTime(3),
                    IsActive = reader.GetBoolean(4)
                };
            }

            return null;
        }

        public async Task<decimal> GetTotalCycleAsync()
        {

            const string sql = "SELECT COALESCE(SUM(TotalAllowance), 0) FROM BudgetCycles";

            using var reader = await DataAccessLayer.ExecuteReaderAsync(sql, CommandType.Text);

            if (await reader.ReadAsync())
                return reader.IsDBNull(0) ? 0 : reader.GetDecimal(0);

            return 0;
        }


        public async Task<List<Category>> GetByCategoryAsync(string search)
        {
            const string sql =
         "SELECT Id, Name FROM Categories WHERE Name LIKE @Search ORDER BY Name";

            var param = DataAccessLayer.CreateParameter(
                "@Search",
                DbType.String,
                $"%{search}%");

            using var reader = await DataAccessLayer.ExecuteReaderAsync(
                sql,
                CommandType.Text,
                param);

            var categories = new List<Category>();

            while (await reader.ReadAsync())
            {
                categories.Add(new Category
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }

            return categories;
        }

        public async Task UpdateAsync(BudgetCycle cycle)
        {
            const string sql = @"
            UPDATE BudgetCycles 
            SET TotalAllowance = @TotalAllowance,
                StartDate = @StartDate,
                EndDate = @EndDate,
                IsActive = @IsActive
            WHERE Id = @Id";

                var parameters = new[]
                {
            DataAccessLayer.CreateParameter("@TotalAllowance", DbType.Decimal, cycle.TotalAllowance),
            DataAccessLayer.CreateParameter("@StartDate", DbType.DateTime, cycle.StartDate),
            DataAccessLayer.CreateParameter("@EndDate", DbType.DateTime, cycle.EndDate),
            DataAccessLayer.CreateParameter("@IsActive", DbType.Boolean, cycle.IsActive),
            DataAccessLayer.CreateParameter("@Id", DbType.Int32, cycle.Id)
        };

                await DataAccessLayer.ExecuteNonQueryAsync(sql, CommandType.Text, parameters);
            }
        public async Task DeleteAsync(int id)
        {
            const string sql = "DELETE FROM BudgetCycles WHERE Id = @Id";

            var param = DataAccessLayer.CreateParameter("@Id", DbType.Int32, id);

            await DataAccessLayer.ExecuteNonQueryAsync(sql, CommandType.Text, param);
        }
        public async Task DeactivateCurrentCycleAsync()
        {
            const string sql = "UPDATE BudgetCycles SET IsActive = 0 WHERE IsActive = 1";
            await DataAccessLayer.ExecuteNonQueryAsync(sql, CommandType.Text);
        }

        public async Task<List<BudgetCycle>> GetAllCyclesAsync()
        {
            const string sql = @"
        SELECT Id, TotalAllowance, StartDate, EndDate, IsActive 
        FROM BudgetCycles 
        ORDER BY StartDate DESC";

            var list = new List<BudgetCycle>();

            using var reader = await DataAccessLayer.ExecuteReaderAsync(sql, CommandType.Text);

            while (await reader.ReadAsync())
            {
                list.Add(new BudgetCycle
                {
                    Id = reader.GetInt32(0),
                    TotalAllowance = reader.GetDecimal(1),
                    StartDate = reader.GetDateTime(2),
                    EndDate = reader.GetDateTime(3),
                    IsActive = reader.GetBoolean(4)
                });
            }

            return list;
        }

        public async Task<BudgetCycle?> GetByIdAsync(int id)
        {
            const string sql = @"
        SELECT Id, TotalAllowance, StartDate, EndDate, IsActive 
        FROM BudgetCycles 
        WHERE Id = @Id";

            var param = DataAccessLayer.CreateParameter("@Id", DbType.Int32, id);

            using var reader = await DataAccessLayer.ExecuteReaderAsync(sql, CommandType.Text, param);

            if (await reader.ReadAsync())
            {
                return new BudgetCycle
                {
                    Id = reader.GetInt32(0),
                    TotalAllowance = reader.GetDecimal(1),
                    StartDate = reader.GetDateTime(2),   // ✔ FIX
                    EndDate = reader.GetDateTime(3),     // ✔ FIX
                    IsActive = reader.GetBoolean(4)      // ✔ FIX (أفضل من Int32)
                };
            }

            return null;
        }
    }
}
