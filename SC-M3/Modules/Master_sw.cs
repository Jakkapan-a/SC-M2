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

        public void Delete()
        {
            string sql = "delete from master_sw where id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            SQliteDataAccess.InserInputDB(sql, parameters);
        }

        public static List<Master_sw> LoadAll()=> SQliteDataAccess.LoadData<Master_sw>("select * from master_sw");
        public static Master_sw LoadLast()=> SQliteDataAccess.LoadData<Master_sw>("select * from master_sw order by id desc limit 1 ").FirstOrDefault();

        public static Master_sw LoadById(int id)=> SQliteDataAccess.LoadData<Master_sw>("select * from master_sw where id = @id", new Dictionary<string, object> { { "@id", id } }).FirstOrDefault();

        public static Master_sw LoadBySerial(string serial)=> SQliteDataAccess.LoadData<Master_sw>("select * from master_sw where serial_no = @serial_no", new Dictionary<string, object> { { "@serial_no", serial } }).FirstOrDefault();

        public static Master_sw LoadBySerialFull(string serial)=> SQliteDataAccess.LoadData<Master_sw>("select * from master_sw where serial_full = @serial_full", new Dictionary<string, object> { { "@serial_full", serial } }).FirstOrDefault();

        public static Master_sw LoadBySwVer(string sw_ver)=> SQliteDataAccess.LoadData<Master_sw>("select * from master_sw where sw_ver = @sw_ver", new Dictionary<string, object> { { "@sw_ver", sw_ver } }).FirstOrDefault();

        public static Master_sw LoadBySwVerAndSerial(string sw_ver, string serial)=> SQliteDataAccess.LoadData<Master_sw>("select * from master_sw where sw_ver = @sw_ver and serial_no = @serial_no", new Dictionary<string, object> { { "@sw_ver", sw_ver }, { "@serial_no", serial } }).FirstOrDefault();

        public static Master_sw LoadBySwVerAndSerialFull(string sw_ver, string serial)=> SQliteDataAccess.LoadData<Master_sw>("select * from master_sw where sw_ver = @sw_ver and serial_full = @serial_full", new Dictionary<string, object> { { "@sw_ver", sw_ver }, { "@serial_full", serial } }).FirstOrDefault();

        public static Master_sw LoadBySwVerAndSerialAndSerialFull(string sw_ver, string serial, string serial_full)=> SQliteDataAccess.LoadData<Master_sw>("select * from master_sw where sw_ver = @sw_ver and serial_no = @serial_no and serial_full = @serial_full", new Dictionary<string, object> { { "@sw_ver", sw_ver }, { "@serial_no", serial }, { "@serial_full", serial_full } }).FirstOrDefault();

        public static Master_sw LoadBySwVerAndSerialAndSerialFullAndId(string sw_ver, string serial, string serial_full, int id)=> SQliteDataAccess.LoadData<Master_sw>("select * from master_sw where sw_ver = @sw_ver and serial_no = @serial_no and serial_full = @serial_full and id = @id", new Dictionary<string, object> { { "@sw_ver", sw_ver }, { "@serial_no", serial }, { "@serial_full", serial_full }, { "@id", id } }).FirstOrDefault();

    }
}
