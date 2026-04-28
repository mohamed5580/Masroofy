using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Masroofy
{
    // ─────────────────────────────────────────────
    //  Enum to identify the active database engine
    // ─────────────────────────────────────────────
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

        // ─────────────────────────────────────────────────────────────
        //  Configure() — call this ONCE at application startup
        //  Examples:
        //    DataAccessLayer.Configure(DatabaseProvider.SqlServer, "...");
        //    DataAccessLayer.Configure(DatabaseProvider.SQLite,    "Data Source=budget.db");
        //    DataAccessLayer.Configure(DatabaseProvider.MySQL,     "Server=...;Database=...;Uid=...;Pwd=...");
        // ─────────────────────────────────────────────────────────────
        public static void Configure(DatabaseProvider provider, string connectionString)
        {
            Provider = provider;
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        // ─────────────────────────────────────────────────────────────
        //  Auto-build connection string from app Settings
        //  (keeps backward compatibility with the original Con() logic)
        // ─────────────────────────────────────────────────────────────
        public static string BuildConnectionString()
        {
            var s = Masroofy.Properties.Settings.Default;

            switch (Provider)
            {
                case DatabaseProvider.SqlServer:
                    string baseCs = s.Mode
                        ? $"Data Source={s.Server};Initial Catalog={s.Database};Integrated Security=true;Connect Timeout=30;"
                        : $"Data Source={s.Server},1433;Initial Catalog={s.Database};User ID={s.Name};Password={s.Pass};Connect Timeout=30;";
                    return baseCs + "Encrypt=true;TrustServerCertificate=true;";

                case DatabaseProvider.SQLite:
                    // Settings.Database = full path or just filename
                    return $"Data Source={s.Database};";

                case DatabaseProvider.MySQL:
                    return $"Server={s.Server};Database={s.Database};Uid={s.Name};Pwd={s.Pass};Connect Timeout=30;";

                default:
                    throw new NotSupportedException($"Provider '{Provider}' is not supported.");
            }
        }

        // ─────────────────────────────────────────────────────────────
        //  Internal factory: creates the correct DbConnection
        // ─────────────────────────────────────────────────────────────
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

        // ─────────────────────────────────────────────────────────────
        //  Internal factory: creates the correct DbCommand
        // ─────────────────────────────────────────────────────────────
        private static DbCommand CreateCommand(string commandText, CommandType commandType, DbConnection connection)
        {
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            return cmd;
        }

        // ─────────────────────────────────────────────────────────────
        //  Helper: add parameters to a DbCommand
        //  Accepts DbParameter[] — use CreateParameter() to build them
        // ─────────────────────────────────────────────────────────────
        private static void AddParameters(DbCommand command, DbParameter[]? parameters)
        {
            if (parameters != null && parameters.Length > 0)
                foreach (var p in parameters)
                    command.Parameters.Add(p);
        }

        // ══════════════════════════════════════════════════════════════
        //  PUBLIC API  (mirrors original DataAccessLayer surface)
        // ══════════════════════════════════════════════════════════════

        // ── ExecuteNonQuery (sync) ──
        public static int ExecuteNonQuery(string query, CommandType commandType, params DbParameter[]? parameters)
        {
            using var connection = CreateConnection();
            using var command = CreateCommand(query, commandType, connection);
            AddParameters(command, parameters);
            connection.Open();
            return command.ExecuteNonQuery();
        }

        // ── ExecuteNonQuery (async) ──
        public static async Task<int> ExecuteNonQueryAsync(string query, CommandType commandType, params DbParameter[]? parameters)
        {
            await using var connection = CreateConnection();
            await using var command = CreateCommand(query, commandType, connection);
            AddParameters(command, parameters);
            await connection.OpenAsync().ConfigureAwait(false);
            return await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        // ── ExecuteScalar (sync) ──
        public static object? ExecuteScalar(string query, CommandType commandType, params DbParameter[]? parameters)
        {
            using var connection = CreateConnection();
            using var command = CreateCommand(query, commandType, connection);
            AddParameters(command, parameters);
            connection.Open();
            return command.ExecuteScalar();
        }

        // ── ExecuteScalar (async) ──
        public static async Task<object?> ExecuteScalarAsync(string query, CommandType commandType, params DbParameter[]? parameters)
        {
            await using var connection = CreateConnection();
            await using var command = CreateCommand(query, commandType, connection);
            AddParameters(command, parameters);
            await connection.OpenAsync().ConfigureAwait(false);
            return await command.ExecuteScalarAsync().ConfigureAwait(false);
        }

        // ── ExecuteReader (sync) — caller must dispose ──
        public static DbDataReader ExecuteReader(string query, CommandType commandType, params DbParameter[]? parameters)
        {
            var connection = CreateConnection();
            var command = CreateCommand(query, commandType, connection);
            AddParameters(command, parameters);
            connection.Open();
            // CommandBehavior.CloseConnection closes the connection when reader is disposed
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        // ── ExecuteReader (async) — caller must dispose ──
        public static async Task<DbDataReader> ExecuteReaderAsync(string query, CommandType commandType, params DbParameter[]? parameters)
        {
            var connection = CreateConnection();
            var command = CreateCommand(query, commandType, connection);
            AddParameters(command, parameters);
            await connection.OpenAsync().ConfigureAwait(false);
            return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection).ConfigureAwait(false);
        }

        // ── ExecuteTable (sync) — returns DataTable ──
        public static DataTable ExecuteTable(string query, CommandType commandType, params DbParameter[]? parameters)
        {
            var dt = new DataTable();
            using var reader = ExecuteReader(query, commandType, parameters);
            dt.Load(reader);
            return dt;
        }

        // ── ExecuteTable (async) — returns DataTable ──
        public static async Task<DataTable> ExecuteTableAsync(string query, CommandType commandType, params DbParameter[]? parameters)
        {
            var dt = new DataTable();
            var reader = await ExecuteReaderAsync(query, commandType, parameters).ConfigureAwait(false);
            await using (reader)
                dt.Load(reader);
            return dt;
        }

        // ── FillDataSet (sync) ──
        public static void FillDataSet(DataSet ds, string tableName, string query, CommandType commandType, params DbParameter[]? parameters)
        {
            if (ds == null) throw new ArgumentNullException(nameof(ds));
            if (string.IsNullOrWhiteSpace(tableName)) throw new ArgumentNullException(nameof(tableName));

            using var reader = ExecuteReader(query, commandType, parameters);
            ds.Tables[tableName]?.Clear();

            var dt = new DataTable(tableName);
            dt.Load(reader);

            if (ds.Tables.Contains(tableName))
                ds.Tables.Remove(tableName);

            ds.Tables.Add(dt);
        }

        // ─────────────────────────────────────────────────────────────
        //  CreateParameter — provider-aware factory
        //  Usage:
        //    var p = DataAccessLayer.CreateParameter("@Name", DbType.String, "Ahmed");
        // ─────────────────────────────────────────────────────────────
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
        //  LogActivity (unchanged)
        // ─────────────────────────────────────────────────────────────
        public static void LogActivity(string user, string action)
        {
            Console.WriteLine($"ACTIVITY LOG: User='{user}', Action='{action}'");
        }
    }
}