using System.Data;
using Microsoft.Data.Sqlite;

namespace ClinicWinForms.Data
{
    public static class DbConnectionFactory
    {
        public static IDbConnection CreateConnection(string connectionString)
        {
            return DatabaseConfig.DB_TYPE switch
            {
                "sqlite" => new SqliteConnection(connectionString),
                _ => throw new NotSupportedException()
            };
        }
    }
}
