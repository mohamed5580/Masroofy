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
            const string sql = @"
                INSERT INTO BudgetCycles (TotalAllowance, StartDate, EndDate, IsActive)
                VALUES (@TotalAllowance, @StartDate, @EndDate, @IsActive);
                SELECT last_insert_rowid();";

            var paramTotal = DataAccessLayer.CreateParameter("@TotalAllowance", DbType.Decimal, cycle.TotalAllowance);
            var paramStart = DataAccessLayer.CreateParameter("@StartDate", DbType.String, cycle.StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
            var paramEnd = DataAccessLayer.CreateParameter("@EndDate", DbType.String, cycle.EndDate.ToString("yyyy-MM-dd HH:mm:ss"));
            var paramActive = DataAccessLayer.CreateParameter("@IsActive", DbType.Int32, cycle.IsActive ? 1 : 0);

            var result = await DataAccessLayer.ExecuteScalarAsync(sql, CommandType.Text, paramTotal, paramStart, paramEnd, paramActive);
            return Convert.ToInt32(result);
        }

        public async Task<BudgetCycle?> GetActiveCycleAsync()
        {
            const string sql = "SELECT * FROM BudgetCycles WHERE IsActive = 1";
            using var reader = await DataAccessLayer.ExecuteReaderAsync(sql, CommandType.Text);
            if (await reader.ReadAsync())
            {
                return new BudgetCycle
                {
                    Id = reader.GetInt32(0),
                    TotalAllowance = reader.GetDecimal(1),
                    StartDate = DateTime.Parse(reader.GetString(2)),
                    EndDate = DateTime.Parse(reader.GetString(3)),
                    IsActive = reader.GetInt32(4) == 1
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

            var paramTotal = DataAccessLayer.CreateParameter("@TotalAllowance", DbType.Decimal, cycle.TotalAllowance);
            var paramStart = DataAccessLayer.CreateParameter("@StartDate", DbType.String, cycle.StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
            var paramEnd = DataAccessLayer.CreateParameter("@EndDate", DbType.String, cycle.EndDate.ToString("yyyy-MM-dd HH:mm:ss"));
            var paramActive = DataAccessLayer.CreateParameter("@IsActive", DbType.Int32, cycle.IsActive ? 1 : 0);
            var paramId = DataAccessLayer.CreateParameter("@Id", DbType.Int32, cycle.Id);

            await DataAccessLayer.ExecuteNonQueryAsync(sql, CommandType.Text, paramTotal, paramStart, paramEnd, paramActive, paramId);
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
            const string sql = "SELECT Id, TotalAllowance, StartDate, EndDate, IsActive FROM BudgetCycles ORDER BY StartDate DESC";
            var list = new List<BudgetCycle>();
            using var reader = await DataAccessLayer.ExecuteReaderAsync(sql, CommandType.Text);
            while ( await reader.ReadAsync())
            {
                list.Add(new BudgetCycle
                {
                    Id = reader.GetInt32(0),
                    TotalAllowance = reader.GetDecimal(1),
                    StartDate = DateTime.Parse(reader.GetString(2)),
                    EndDate = DateTime.Parse(reader.GetString(3)),
                    IsActive = reader.GetInt32(4) == 1
                });
            }
            return list;
        }

       public async Task<BudgetCycle?> GetByIdAsync(int id)
        {
            const string sql = "SELECT * FROM BudgetCycles WHERE Id = @Id";
            var param = DataAccessLayer.CreateParameter("@Id", DbType.Int32, id);
            using var reader = await DataAccessLayer.ExecuteReaderAsync(sql, CommandType.Text, param);
            if (await reader.ReadAsync())
            {
                return new BudgetCycle
                {
                    Id = reader.GetInt32(0),
                    TotalAllowance = reader.GetDecimal(1),
                    StartDate = DateTime.Parse(reader.GetString(2)),
                    EndDate = DateTime.Parse(reader.GetString(3)),
                    IsActive = reader.GetInt32(4) == 1
                };
            }
            return null;
        }
    }
}
