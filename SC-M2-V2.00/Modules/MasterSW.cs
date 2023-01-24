using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_M2_V2._00.Modules
{
    public class MasterSW
    {
        public int id { get; set; }
        public string name { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set;}

        public void Save()
        {
            string sql = "insert into master_sw (name,created_at,updated_at) values (@name,@created_at,@updated_at)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@name", name);
            parameters.Add("@created_at", SQliteDataAccess.GetDateTimeNow());
            parameters.Add("@updated_at", SQliteDataAccess.GetDateTimeNow());
            SQliteDataAccess.InserInputDB(sql, parameters);
        }

        public void Update()
        {
            string sql = "update master_sw set name = @name, updated_at = @updated_at where id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            parameters.Add("@name", name);
            parameters.Add("@updated_at", SQliteDataAccess.GetDateTimeNow());
            SQliteDataAccess.InserInputDB(sql, parameters);
        }
        public void Delete()
        {
            string sql = "delete from master_sw where id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            SQliteDataAccess.InserInputDB(sql, parameters);
        }
        public static List<MasterSW> GetMasterSW() => SQliteDataAccess.GetRow<MasterSW>("select * from master_sw order by id desc");

        public static List<MasterSW> GetMasterSW(int id) => SQliteDataAccess.GetRow<MasterSW>("select * from master_sw where id = " + id);

        public static List<MasterSW> GetMasterSW(string name) => SQliteDataAccess.GetRow<MasterSW>("select * from master_sw where name = '" + name + "'");

        public static List<MasterSW> GetMasterSW(string name, int id) => SQliteDataAccess.GetRow<MasterSW>("select * from master_sw where name = '" + name + "' and id <> " + id);

        public static List<MasterSW> GetMasterSW(int id, string name) => SQliteDataAccess.GetRow<MasterSW>("select * from master_sw where id = " + id + " and name = '" + name + "'");

        public static bool IsExist(string name) => GetMasterSW(name).Count > 0;

        public static void Delete(int id) => SQliteDataAccess.InserInputDB("delete from master_sw where id = " + id, null);

    }
}
