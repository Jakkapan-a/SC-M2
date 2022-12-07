using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Dapper;
using System.Data;
using System.Configuration;

namespace SC_M2.Modules
{
    public class SQliteDataAccess
    {
        public static List<T> GetAll<T>(string table_name)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<T>("select * from " + table_name, new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<T> GetRow<T>(string sql)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<T>(sql, new DynamicParameters());
                return output.ToList();
            }
        }


        // Insert to Db
        public static void InserTnputDB(string sql, Dictionary<string, object> parameters)
        {
            using (IDbConnection con = new SQLiteConnection(LoadConnectionString()))
            {
                con.Execute(sql, parameters);
            }
        }

        // Update to Db
        public static void Update(string sql, Dictionary<string, object> parameters)
        {
            using (IDbConnection con = new SQLiteConnection(LoadConnectionString()))
            {
                con.Execute(sql, parameters);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return "Data Source=" + System.IO.Directory.GetCurrentDirectory() + "\\" + ConfigurationManager.ConnectionStrings[id];
        }
    }
}
