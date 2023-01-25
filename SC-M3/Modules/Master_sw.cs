using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_M3.Modules
{
    internal class Master_sw
    {
        public int id { get; set; }
        public string sw_ver { get; set; }
        public string serial_no { get; set; }

        public string serial_full { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        public void Save(){
            string sql = "insert into master_sw (sw_ver, serial_no, serial_full, created_at, updated_at) values (@sw_ver, @serial_no, @serial_full, @created_at, @updated_at)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@sw_ver", sw_ver);
            parameters.Add("@serial_no", serial_no);
            parameters.Add("@serial_full", serial_full);
            parameters.Add("@created_at", SQliteDataAccess.GetDateTimeNow());
            parameters.Add("@updated_at", SQliteDataAccess.GetDateTimeNow());
            SQliteDataAccess.InserInputDB(sql, parameters);
        }

        public void Update(){
            string sql = "update master_sw set sw_ver = @sw_ver, serial_no = @serial_no, serial_full = @serial_full, created_at = @created_at, updated_at = @updated_at where id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@sw_ver", sw_ver);
            parameters.Add("@serial_no", serial_no);
            parameters.Add("@serial_full", serial_full);
            parameters.Add("@updated_at", SQliteDataAccess.GetDateTimeNow());
            parameters.Add("@id", id);
            SQliteDataAccess.InserInputDB(sql, parameters);
        }
    }
}
