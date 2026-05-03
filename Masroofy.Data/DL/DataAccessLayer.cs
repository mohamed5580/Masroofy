using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Masroofy.Data.Database
{
    public enum DatabaseProvider { SqlServer, SQLite, MySQL }

    public static class DataAccessLayer
    {
        public static DatabaseProvider Provider { get; private set; } = DatabaseProvider.SQLite;
        private static string _connectionString = string.Empty;

        public static void Configure(DatabaseProvider provider, string connectionString)
        {
            Provider = provider;
            _connectionString = connectionString;
        }

        private static DbConnection CreateConnection()
        {
            string cs = string.IsNullOrWhiteSpace(_connectionString) ?
                $"Data Source={Masroofy.Data.Properties.Settings.Default.Database};" : _connectionString;

            return Provider switch
            {
                DatabaseProvider.SQLite => new SqliteConnection(cs),
                DatabaseProvider.SqlServer => new SqlConnection(cs),
                DatabaseProvider.MySQL => new MySqlConnection(cs),
                _ => throw new NotSupportedException()
            };
        }

        private static DbCommand CreateCommand(string sql, CommandType type, DbConnection conn)
        {
            DbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = type;
            return cmd;
        }

        public static async Task<int> ExecuteNonQueryAsync(string query, CommandType type, params DbParameter[] parameters)
        {
            await using var conn = CreateConnection();
            await using var cmd = CreateCommand(query, type, conn);
            if (parameters != null) cmd.Parameters.AddRange(parameters);
            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync();
        }

        public static async Task<object?> ExecuteScalarAsync(string query, CommandType type, params DbParameter[] parameters)
        {
            await using var conn = CreateConnection();
            await using var cmd = CreateCommand(query, type, conn);
            if (parameters != null) cmd.Parameters.AddRange(parameters);
            await conn.OpenAsync();
            return await cmd.ExecuteScalarAsync();
        }

        public static async Task<DbDataReader> ExecuteReaderAsync(string query, CommandType type, params DbParameter[] parameters)
        {
            var conn = CreateConnection();
            var cmd = CreateCommand(query, type, conn);
            if (parameters != null) cmd.Parameters.AddRange(parameters);
            await conn.OpenAsync();
            return await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }

        public static DbParameter CreateParameter(string name, DbType type, object value)
        {
            DbParameter p = Provider switch
            {
                DatabaseProvider.SQLite => new SqliteParameter(),
                DatabaseProvider.SqlServer => new SqlParameter(),
                DatabaseProvider.MySQL => new MySqlParameter(),
                _ => throw new NotSupportedException()
            };
            p.ParameterName = name;
            p.DbType = type;
            p.Value = value ?? DBNull.Value;
            return p;
        }

        // FIX: Added this method to solve the 'Program.cs' error
        public static async Task SeedCategoriesAsync()
        {
            string query = @"
                INSERT OR IGNORE INTO Categories (Id, Name) VALUES (1, 'Food');
                INSERT OR IGNORE INTO Categories (Id, Name) VALUES (2, 'Transport');
                INSERT OR IGNORE INTO Categories (Id, Name) VALUES (3, 'Entertainment');
                INSERT OR IGNORE INTO Categories (Id, Name) VALUES (4, 'Utilities');
                INSERT OR IGNORE INTO Categories (Id, Name) VALUES (5, 'Other');";
            try
            {
                await ExecuteNonQueryAsync(query, CommandType.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Seed Error: " + ex.Message);
            }
        }
    }
}