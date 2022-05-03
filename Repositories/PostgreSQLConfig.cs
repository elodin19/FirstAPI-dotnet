using System;
namespace FirstAPI.Data
{
    public class PostgreSQLConfig
    {
        public PostgreSQLConfig(string connectionString) => ConnectionString = connectionString;

        public string ConnectionString { get; set; }
    }
}
