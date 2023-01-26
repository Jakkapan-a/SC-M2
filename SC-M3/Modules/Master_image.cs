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
            SQliteDataAccess.Command(sql, parameters);
        }

        public void Update(){
            string sql = "update master_image set path = @path, state = @state, created_at = @created_at, updated_at = @updated_at where id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@path", path);
            parameters.Add("@state", state);
            parameters.Add("@updated_at", SQliteDataAccess.GetDateTimeNow());
            parameters.Add("@id", id);
            SQliteDataAccess.Command(sql, parameters);
        }

        public void Delete()
        {
            string sql = "delete from master_image where id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            SQliteDataAccess.Command(sql, parameters);
        }

        public static List<Master_image> LoadAll()=> SQliteDataAccess.LoadData<Master_image>("select * from master_image");
        public static List<Master_image> LoadAllByState(int state)=> SQliteDataAccess.LoadData<Master_image>("select * from master_image where state = @state order id desc", new Dictionary<string, object> { { "@state", state } });
        public static Master_image LoadLastByState(int state)=> SQliteDataAccess.LoadData<Master_image>("select * from master_image where state = @state order by id desc limit 1 ", new Dictionary<string, object> { { "@state", state } }).FirstOrDefault();
        public static Master_image LoadById(int id)=> SQliteDataAccess.LoadData<Master_image>("select * from master_image where id = @id", new Dictionary<string, object> { { "@id", id } }).FirstOrDefault();
        public static Master_image LoadByPath(string path)=> SQliteDataAccess.LoadData<Master_image>("select * from master_image where path = @path", new Dictionary<string, object> { { "@path", path } }).FirstOrDefault();
    }
}
