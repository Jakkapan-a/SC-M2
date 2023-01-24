using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_M2_V2._00.Modules
{
    public class MasterLB
    {
        public int id { get; set;}
        public int master_sw_id { get; set;}
        public string name { get; set;}
        public string created_at { get; set;}
        public string updated_at { get; set;}

        public void Save()
        {
            string sql = "insert into master_lb (master_sw_id,name,created_at,updated_at) values (@master_sw_id,@name,@created_at,@updated_at)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@master_sw_id", master_sw_id);
            parameters.Add("@name", name);
            parameters.Add("@created_at", SQliteDataAccess.GetDateTimeNow());
            parameters.Add("@updated_at", SQliteDataAccess.GetDateTimeNow());
            SQliteDataAccess.InserInputDB(sql, parameters);
        }

        public void Update()
        {
            string sql = "update master_lb set master_sw_id = @master_sw_id, name = @name, updated_at = @updated_at where id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            parameters.Add("@master_sw_id", master_sw_id);
            parameters.Add("@name", name);
            parameters.Add("@updated_at", SQliteDataAccess.GetDateTimeNow());
            SQliteDataAccess.InserInputDB(sql, parameters);
        }
        public void Delete()
        {
            string sql = "delete from master_lb where id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            SQliteDataAccess.InserInputDB(sql, parameters);
        }

        public static List<MasterLB> GetMasterLB() => SQliteDataAccess.GetRow<MasterLB>("select * from master_lb order by id desc");

        public static List<MasterLB> GetMasterLB(int id) => SQliteDataAccess.GetRow<MasterLB>("select * from master_lb where id = " + id);

        public static List<MasterLB> GetMasterLB(string name) => SQliteDataAccess.GetRow<MasterLB>("select * from master_lb where name = '" + name + "'");

        public static List<MasterLB> GetMasterLB(string name, int id) => SQliteDataAccess.GetRow<MasterLB>("select * from master_lb where name = '" + name + "' and id <> " + id);

        public static List<MasterLB> GetMasterLB(int master_sw_id, string name) => SQliteDataAccess.GetRow<MasterLB>("select * from master_lb where master_sw_id = " + master_sw_id + " and name = '" + name + "'");

        public static List<MasterLB> GetMasterLBBySW(int master_sw_id) => SQliteDataAccess.GetRow<MasterLB>("select * from master_lb where master_sw_id = " + master_sw_id + " ");

        public static bool IsExist(string name) => GetMasterLB(name).Count > 0;

        public static void Delete(int id_lb) => SQliteDataAccess.InserInputDB("delete from master_lb where id = " + id_lb, null);
    }
}
