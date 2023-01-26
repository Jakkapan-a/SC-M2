using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_M3.Modules
{
    internal class History
    {
        public int id { get; set; }
        public string employee { get; set; }
        public string serial_no { get; set; }
        public string serial_full { get; set; }
        public string sw_ver { get; set; }  
        public string sw_ver_full { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        public void Save(){
            string sql = "insert into history (employee, serial_no, serial_full, sw_ver, sw_ver_full, created_at, updated_at) values (@employee, @serial_no, @serial_full, @sw_ver, @sw_ver_full, @created_at, @updated_at)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@employee", employee);
            parameters.Add("@serial_no", serial_no);
            parameters.Add("@serial_full", serial_full);
            parameters.Add("@sw_ver", sw_ver);
            parameters.Add("@sw_ver_full", sw_ver_full);
            parameters.Add("@created_at", SQliteDataAccess.GetDateTimeNow());
            parameters.Add("@updated_at", SQliteDataAccess.GetDateTimeNow());
            SQliteDataAccess.Command(sql, parameters);
        }

        public void Update(){
            string sql = "update history set employee = @employee, serial_no = @serial_no, serial_full = @serial_full, sw_ver = @sw_ver, sw_ver_full = @sw_ver_full, created_at = @created_at, updated_at = @updated_at where id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@employee", employee);
            parameters.Add("@serial_no", serial_no);
            parameters.Add("@serial_full", serial_full);
            parameters.Add("@sw_ver", sw_ver);
            parameters.Add("@sw_ver_full", sw_ver_full);
            parameters.Add("@updated_at", SQliteDataAccess.GetDateTimeNow());
            parameters.Add("@id", id);
            SQliteDataAccess.Command(sql, parameters);
        }

        public void Delete()
        {
            string sql = "delete from history where id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            SQliteDataAccess.Command(sql, parameters);
        }

        public static List<History> LoadHistory()=> SQliteDataAccess.LoadData<History>("select * from history order by id desc limit 100");

        public static List<History> LoadHistory(string serial_no)=> SQliteDataAccess.LoadData<History>("select * from history where serial_no = @serial_no order by id desc limit 100", new Dictionary<string, object> { { "@serial_no", serial_no } });

        public static List<History> LoadHistory(string serial_no, string sw_ver)=> SQliteDataAccess.LoadData<History>("select * from history where serial_no = @serial_no and sw_ver = @sw_ver order by id desc limit 100", new Dictionary<string, object> { { "@serial_no", serial_no }, { "@sw_ver", sw_ver } });

        public static List<History> LoadHistory(int start, int end)=> SQliteDataAccess.LoadData<History>("select * from history order by id desc limit @start, @end", new Dictionary<string, object> { { "@start", start }, { "@end", end } });
    }
}
