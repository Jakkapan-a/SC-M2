using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_M2.Modules
{
    internal class ImageList
    {
        public string id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string model_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        public ImageList(string id, string name, string path, string model_id)
        {
            this.id = id;
            this.name = name;
            this.path = path;
            this.model_id = model_id;
            this.created_at = GetDateTimeNow();
            this.updated_at = GetDateTimeNow();
        }

        public List<ImageList> GetAll()
        {
            return SQliteDataAccess.GetAll<ImageList>("image");
        }

        public List<ImageList> GetRow(string sql)
        {
            return SQliteDataAccess.GetRow<ImageList>(sql);
        }

        public List<ImageList> GetRow(int id)
        {
            return SQliteDataAccess.GetRow<ImageList>("select * from image where id = " + id);
        }

        public void Save()
        {
            string sql = "INSERT INTO image (name, path, model_id, created_at, updated_at) VALUES (@name, @path, @model_id, @created_at, @updated_at)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@name", name);
            parameters.Add("@path", path);
            parameters.Add("@model_id", model_id);
            parameters.Add("@created_at", GetDateTimeNow());
            parameters.Add("@updated_at", GetDateTimeNow());
            SQliteDataAccess.InserTnputDB(sql, parameters);
        }
        
        public void Update()
        {
            string sql = "update image set name = @name, path = @path, model_id = @model_id, updated_at = @updated_at where id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            parameters.Add("@name", name);
            parameters.Add("@path", path);
            parameters.Add("@model_id", model_id);
            parameters.Add("@updated_at", GetDateTimeNow());
            SQliteDataAccess.Update(sql, parameters);
        }
        
        public void Delete()
        {
            string sql = "delete from image where id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            SQliteDataAccess.Update(sql, parameters);
        }
        private string GetDateTimeNow()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}

