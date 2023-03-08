using System.Configuration;
using System.Data.SqlClient;

namespace PA.StockMarket.Data
{
    internal static class Database
    {
        private static SqlConnection sc = new SqlConnection(ConnectionString);
        public static SqlConnection Connection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }
        public static string ConnectionString
        {
            get
            {
                string con = ConfigurationManager.ConnectionStrings["marketCS"].ConnectionString;
                return con;
            }
        }
    }
}