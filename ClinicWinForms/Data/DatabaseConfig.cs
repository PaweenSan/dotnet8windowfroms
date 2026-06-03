using System.Data;

namespace ClinicWinForms.Data
{
    public static class DatabaseConfig
    {
        public const string DB_TYPE = "sqlite";

        public static string GetConnectionString(string dbName)
        {
            return DB_TYPE switch
            {
                "sqlite" => $"Data Source={AppDomain.CurrentDomain.BaseDirectory}Databases\\{dbName}.db",
                "sqlserver" => $"Data Source=localhost\\SQLEXPRESS; Database={dbName}; Integrated Security=SSPI",
                "mysql" => $"Server=localhost;Database={dbName};Uid=root;Pwd=password;",
                _ => throw new NotSupportedException($"DB_TYPE '{DB_TYPE}' not supported")
            };
        }
    }
}
