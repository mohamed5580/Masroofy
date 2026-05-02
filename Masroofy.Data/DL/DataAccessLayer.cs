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
    public enum DatabaseProvider
    {
        SqlServer,
        SQLite,
        MySQL
    }

    public static class DataAccessLayer
    {
        public static DatabaseProvider Provider { get; private set; } = DatabaseProvider.SqlServer;
        private static string _connectionString = string.Empty;

        public static void Configure(DatabaseProvider provider, string connectionString)
        {
            Provider = provider;
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public static string BuildConnectionString()
        {
            var s = Masroofy.Data.Properties.Settings.Default;
            switch (Provider)
            {
                case DatabaseProvider.SqlServer:
                    string baseCs = s.Mode
                        ? $"Data Source={s.Server};Initial Catalog={s.Database};Integrated Security=true;Connect Timeout=30;"
                        : $"Data Source={s.Server},1433;Initial Catalog={s.Database};User ID={s.Name};Password={s.Pass};Connect Timeout=30;";
                    return baseCs + "Encrypt=true;TrustServerCertificate=true;";
                case DatabaseProvider.SQLite:
                    return $"Data Source={s.Database};";
                case DatabaseProvider.MySQL:
                    return $"Server={s.Server};Database={s.Database};Uid={s.Name};Pwd={s.Pass};Connect Timeout=30;";
                default:
                    throw new NotSupportedException($"Provider '{Provider}' is not supported.");
            }
        }

        private static DbConnection CreateConnection()
        {
            string cs = string.IsNullOrWhiteSpace(_connectionString) ? BuildConnectionString() : _connectionString;
            return Provider switch
            {
                DatabaseProvider.SqlServer => new SqlConnection(cs),
                DatabaseProvider.SQLite => new SqliteConnection(cs),
                DatabaseProvider.MySQL => new MySqlConnection(cs),
                _ => throw new NotSupportedException($"Provider '{Provider}' is not supported.")
            };
        }

        private static DbCommand CreateCommand(string commandText, CommandType commandType, DbConnection connection)
        {
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            return cmd;
        }

        private static void AddParameters(DbCommand command, DbParameter[]? parameters)
        {
            if (parameters != null && parameters.Length > 0)
                foreach (var p in parameters)
                    command.Parameters.Add(p);
        }

        // --- PUBLIC API ---

        public static int ExecuteNonQuery(string query, CommandType commandType, params DbParameter[]? parameters)
        {
            using var connection = CreateConnection();
            using var command = CreateCommand(query, commandType, connection);
            AddParameters(command, parameters);
            connection.Open();
            return command.ExecuteNonQuery();
        }

        public static async Task<int> ExecuteNonQueryAsync(string query, CommandType commandType, params DbParameter[]? parameters)
        {
            await using var connection = CreateConnection();
            await using var command = CreateCommand(query, commandType, connection);
            AddParameters(command, parameters);
            await connection.OpenAsync().ConfigureAwait(false);
            return await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public static object? ExecuteScalar(string query, CommandType commandType, params DbParameter[]? parameters)
        {
            using var connection = CreateConnection();
            using var command = CreateCommand(query, commandType, connection);
            AddParameters(command, parameters);
            connection.Open();
            return command.ExecuteScalar();
        }

        public static async Task<object?> ExecuteScalarAsync(string query, CommandType commandType, params DbParameter[]? parameters)
        {
            await using var connection = CreateConnection();
            await using var command = CreateCommand(query, commandType, connection);
            AddParameters(command, parameters);
            await connection.OpenAsync().ConfigureAwait(false);
            return await command.ExecuteScalarAsync().ConfigureAwait(false);
        }

        public static DbDataReader ExecuteReader(string query, CommandType commandType, params DbParameter[]? parameters)
        {
            var connection = CreateConnection();
            var command = CreateCommand(query, commandType, connection);
            AddParameters(command, parameters);
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static async Task<DbDataReader> ExecuteReaderAsync(string query, CommandType commandType, params DbParameter[]? parameters)
        {
            var connection = CreateConnection();
            var command = CreateCommand(query, commandType, connection);
            AddParameters(command, parameters);
            await connection.OpenAsync().ConfigureAwait(false);
            return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection).ConfigureAwait(false);
        }

        public static DbParameter CreateParameter(string name, DbType dbType, object? value)
        {
            DbParameter p = Provider switch
            {
                DatabaseProvider.SqlServer => new SqlParameter(),
                DatabaseProvider.SQLite => new SqliteParameter(),
                DatabaseProvider.MySQL => new MySqlParameter(),
                _ => throw new NotSupportedException()
            };
            p.ParameterName = name;
            p.DbType = dbType;
            p.Value = value ?? DBNull.Value;
            return p;
        }

        // ─────────────────────────────────────────────────────────────
        //  NEW: SeedCategoriesAsync — Ensures IDs 1-5 exist
        // ─────────────────────────────────────────────────────────────
        public static async Task SeedCategoriesAsync()
        {
            // INSERT OR IGNORE works perfectly for SQLite to prevent duplicate errors
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