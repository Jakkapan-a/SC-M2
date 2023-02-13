using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SC_M4.Modules
{
    public class SQliteDataAccess
    {
        public static List<T> GetAll<T>(string table_name)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<T>("select * from " + table_name + " order by id desc limit 100", new DynamicParameters());
                return output.ToList();
            }
        }
        // GetAllNolimit
        public static List<T> GetAllNolimit<T>(string table_name)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<T>("select * from " + table_name + " order by id desc", new DynamicParameters());
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
        public static void InserInputDB(string sql, Dictionary<string, object> parameters)
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

        public static string GetDateTimeNow()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        internal static object LoadData<T>(string sql, Dictionary<string, object> parameters)
        {
            
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<T>(sql, parameters);
                return output.ToList();
            }
        }

        internal static bool IsExist(string sql)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query(sql, new DynamicParameters());
                return output.ToList().Count > 0;
            }
        }
    }
}
