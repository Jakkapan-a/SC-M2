using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_M3.Modules
{
    internal class Master_image
    {
        public int id { get; set; }
        public string path { get; set; }
        public int state { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        public void Save(){
            string sql = "insert into master_image (path, state, created_at, updated_at) values (@path, @state, @created_at, @updated_at)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@path", path);
            parameters.Add("@state", state);
            parameters.Add("@created_at", SQliteDataAccess.GetDateTimeNow());
            parameters.Add("@updated_at", SQliteDataAccess.GetDateTimeNow());
            SQliteDataAccess.InserInputDB(sql, parameters);
        }

        public void Update(){
            string sql = "update master_image set path = @path, state = @state, created_at = @created_at, updated_at = @updated_at where id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@path", path);
            parameters.Add("@state", state);
            parameters.Add("@updated_at", SQliteDataAccess.GetDateTimeNow());
            parameters.Add("@id", id);
            SQliteDataAccess.InserInputDB(sql, parameters);
        }
    }
}
