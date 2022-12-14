using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace SC_M2.Modules
{
    internal class ImageList
    {
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public int model_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        public ImageList()
        {
            this.created_at = GetDateTimeNow();
            this.updated_at = GetDateTimeNow();
        }
        public ImageList(string name, string path, int model_id)
        {
            this.name = name;
            this.path = path;
            this.model_id = model_id;
            this.created_at = GetDateTimeNow();
            this.updated_at = GetDateTimeNow();
        }

        public static List<ImageList> GetAll() => SQliteDataAccess.GetAll<ImageList>("image");
        
        public static List<ImageList> GetModel(int model_id) => SQliteDataAccess.GetRow<ImageList>("select * from image where model_id = " + model_id);
        
        public static List<ImageList> GetRow(string sql)=>SQliteDataAccess.GetRow<ImageList>(sql);
        
        public static List<ImageList> GetRow(int id) => SQliteDataAccess.GetRow<ImageList>("select * from image where id = " + id);
        
        public void Get()
        {
            var data = SQliteDataAccess.GetRow<ImageList>("select * from image where id = " + id);
            if(data.Count != 0)
            {
                this.id = data[0].id;
                this.name = data[0].name;
                this.path = data[0].path;
                this.model_id = data[0].model_id;
                this.created_at = data[0].created_at;
                this.updated_at = data[0].updated_at;                
            }
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
            SQliteDataAccess.InserInputDB(sql, parameters);
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
            var delete = new Delete_image(name, path);
            delete.Save();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            SQliteDataAccess.Update(sql, parameters);
        }
        public void Delete(int model_id)
        {
            string sql = "delete from image where model_id = @id";

            var all = GetModel(model_id);
           
            foreach (var i in all)
            {
                var delete = new Delete_image(i.name,i.path);
                delete.Save();
            }
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", model_id);
            SQliteDataAccess.Update(sql, parameters);
        }

        private string GetDateTimeNow()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}

