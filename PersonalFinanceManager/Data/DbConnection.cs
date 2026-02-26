using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PersonalFinanceManager.Data
{
    public class DbConnection
    {
        private string connectionString;

        public DbConnection()
        {
            // Set your connection string here or load from config
            connectionString = "Server=DESKTOP-1BH7EFE\\MSSQLSERVER01;Database=FinanceDB;Trusted_Connection=True";
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
