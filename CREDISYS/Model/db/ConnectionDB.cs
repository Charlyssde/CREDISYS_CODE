using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CREDISYS.Model.db
{
    class ConnectionDB
    {
        private static string SERVER = "localhost"; //por el momento
        private static string PORT = Properties.Settings.Default.portDB;
        private static string DATABASE = Properties.Settings.Default.databaseDB;
        private static string USER = Properties.Settings.Default.usernameBD;
        private static string PASSWORD = Properties.Settings.Default.passwordDB;

        public static SqlConnection getConnection()
        {
            SqlConnection conn = null;
            try
            {
                String urlconn = String.Format("Data Source={0},{1};" + "Network Library=DBMSSOCN;" + "Initial Catalog={2};" +
                    "User ID={3};" + "Password={4};", SERVER, PORT, DATABASE, USER, PASSWORD);
                conn = new SqlConnection(urlconn);
                conn.Open();
                return conn;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return conn;
        }
    }
}
