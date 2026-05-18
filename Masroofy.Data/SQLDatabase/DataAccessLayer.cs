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
            _connectionString = connectionString ?? string.Empty;
        }

        public static string GetConnectionString()
        {
            if (!string.IsNullOrWhiteSpace(_connectionString))
                return _connectionString;

            var settings = Masroofy.Data.Properties.Settings.Default;
            string port = "3306";

            return Provider switch
            {
                DatabaseProvider.SQLite =>
                    $"Data Source={settings.Database};",   

                DatabaseProvider.SqlServer =>
                     $"Server={settings.Server};Database={settings.Database};Trusted_Connection=True;TrustServerCertificate=True;",

                DatabaseProvider.MySQL =>
                    $"Server={settings.Server};Port={port};Database={settings.Database};Uid={settings.Name};Pwd={settings.Pass};",

                _ => throw new NotSupportedException()
            };
        }

        private static DbConnection CreateConnection()
        {
            string cs = GetConnectionString();

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

        public static async Task<int> ExecuteNonQueryAsync(
            string query,
            CommandType type,
            params DbParameter[] parameters)
        {
            await using var conn = CreateConnection();
            await using var cmd = CreateCommand(query, type, conn);

            if (parameters?.Length > 0)
                cmd.Parameters.AddRange(parameters);

            await conn.OpenAsync();

            return await cmd.ExecuteNonQueryAsync();
        }

        public static async Task<object?> ExecuteScalarAsync(
            string query,
            CommandType type,
            params DbParameter[] parameters)
        {
            await using var conn = CreateConnection();
            await using var cmd = CreateCommand(query, type, conn);

            if (parameters?.Length > 0)
                cmd.Parameters.AddRange(parameters);

            await conn.OpenAsync();

            return await cmd.ExecuteScalarAsync();
        }

        public static async Task<DbDataReader> ExecuteReaderAsync(
            string query,
            CommandType type,
            params DbParameter[] parameters)
        {
            var conn = CreateConnection();
            var cmd = CreateCommand(query, type, conn);

            if (parameters?.Length > 0)
                cmd.Parameters.AddRange(parameters);

            await conn.OpenAsync();

            return await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }

        public static DbParameter CreateParameter(string name, DbType type, object? value)
        {
            DbParameter parameter = Provider switch
            {
                DatabaseProvider.SQLite => new SqliteParameter(),
                DatabaseProvider.SqlServer => new SqlParameter(),
                DatabaseProvider.MySQL => new MySqlParameter(),
                _ => throw new NotSupportedException()
            };

            parameter.ParameterName = name;
            parameter.DbType = type;
            parameter.Value = value ?? DBNull.Value;

            return parameter;
        }

        public static async Task SeedCategoriesAsync()
        {
            string sql = Provider switch
            {
                DatabaseProvider.SQLite => @"
                    INSERT OR IGNORE INTO Categories (Id, Name) VALUES (1, 'Food');
                    INSERT OR IGNORE INTO Categories (Id, Name) VALUES (2, 'Transport');
                    INSERT OR IGNORE INTO Categories (Id, Name) VALUES (3, 'Entertainment');
                    INSERT OR IGNORE INTO Categories (Id, Name) VALUES (4, 'Utilities');
                    INSERT OR IGNORE INTO Categories (Id, Name) VALUES (5, 'Other');",

                DatabaseProvider.SqlServer => @"
                    IF NOT EXISTS (SELECT 1 FROM Categories WHERE Name = 'Food')
                        INSERT INTO Categories (Name) VALUES ('Food');

                    IF NOT EXISTS (SELECT 1 FROM Categories WHERE Name = 'Transport')
                        INSERT INTO Categories (Name) VALUES ('Transport');

                    IF NOT EXISTS (SELECT 1 FROM Categories WHERE Name = 'Entertainment')
                        INSERT INTO Categories (Name) VALUES ('Entertainment');

                    IF NOT EXISTS (SELECT 1 FROM Categories WHERE Name = 'Utilities')
                        INSERT INTO Categories (Name) VALUES ('Utilities');

                    IF NOT EXISTS (SELECT 1 FROM Categories WHERE Name = 'Other')
                        INSERT INTO Categories (Name) VALUES ('Other');",

                DatabaseProvider.MySQL => @"
                    INSERT IGNORE INTO Categories (Id, Name) VALUES (1, 'Food');
                    INSERT IGNORE INTO Categories (Id, Name) VALUES (2, 'Transport');
                    INSERT IGNORE INTO Categories (Id, Name) VALUES (3, 'Entertainment');
                    INSERT IGNORE INTO Categories (Id, Name) VALUES (4, 'Utilities');
                    INSERT IGNORE INTO Categories (Id, Name) VALUES (5, 'Other');",

                _ => throw new NotSupportedException()
            };

            await ExecuteNonQueryAsync(sql, CommandType.Text);
        }
    }
}