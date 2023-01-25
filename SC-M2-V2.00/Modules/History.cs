using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_M2_V2._00.Modules
{
    public class History
    {
        public int id { get; set; }
        public string name { get; set; }
        public string name_sw { get; set; }
        public string master_sw { get; set; }
        public string name_lb { get; set; }
        public string master_lb { get; set; }
        public string result { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set;}

        public void Save()
        {
            string sql = "insert into history (name, name_sw, master_sw, name_lb, master_lb, result, created_at, updated_at) values (@name, @name_sw, @master_sw, @name_lb, @master_lb, @result, @created_at, @updated_at)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@name", name);
            parameters.Add("@name_sw", name_sw);
            parameters.Add("@master_sw", master_sw);
            parameters.Add("@name_lb", name_lb);
            parameters.Add("@master_lb", master_lb);
            parameters.Add("@result", result);
            parameters.Add("@created_at", SQliteDataAccess.GetDateTimeNow());
            parameters.Add("@updated_at", SQliteDataAccess.GetDateTimeNow());
            SQliteDataAccess.InserInputDB(sql, parameters);            
        }

        public void Update()
        {
            string sql = "update history set name = @name, name_sw = @name_sw, master_sw = @master_sw, name_lb = @name_lb, master_lb = @master_lb, result = @result, updated_at = @updated_at where id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            parameters.Add("@name", name);
            parameters.Add("@name_sw", name_sw);
            parameters.Add("@master_sw", master_sw);
            parameters.Add("@name_lb", name_lb);
            parameters.Add("@master_lb", master_lb);
            parameters.Add("@result", result);
            parameters.Add("@updated_at", SQliteDataAccess.GetDateTimeNow());
            SQliteDataAccess.InserInputDB(sql, parameters);
        }

        public void Delete()
        {
            string sql = "delete from history where id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            SQliteDataAccess.InserInputDB(sql, parameters);
        }

        public static List<History> GetHistory() => SQliteDataAccess.GetRow<History>("select * from history order by id desc limit 50");

        public static List<History> GetHistory(int start, int end) => SQliteDataAccess.GetRow<History>("select * from history order by id desc limit " + start + "," + end);

    }
}
