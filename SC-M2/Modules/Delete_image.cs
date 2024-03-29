﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_M2.Modules
{
    internal class Delete_image
    {
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string created_at { get; set; }

        public Delete_image()
        {
        }
        public Delete_image(string name, string path)
        {
            this.name = name;
            this.path = path;
            this.created_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static List<Delete_image> GetAll() => SQliteDataAccess.GetAll<Delete_image>("delete_image");
        

        public static List<Delete_image> GetRow(string sql) => SQliteDataAccess.GetRow<Delete_image>(sql);
        
        
        public static List<Delete_image> GetRow(int id) => SQliteDataAccess.GetRow<Delete_image>("select * from delete_image where id = " + id);
        
        public void Save()
        {
            string sql = "INSERT INTO delete_image (name, path, created_at) VALUES (@name, @path, @created_at)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@name", name);
            parameters.Add("@path", path);
            parameters.Add("@created_at", created_at);
            SQliteDataAccess.InserInputDB(sql, parameters);
        }

        public void Update()
        { 
            string sql = "update delete_image set name = @name, path = @path, created_at = @created_at where id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            parameters.Add("@name", name);
            parameters.Add("@path", path);
            parameters.Add("@created_at", created_at);
            SQliteDataAccess.Update(sql, parameters);
        }

        public void Delete()
        {
            
            string sql = "delete from delete_image where id = @id";           
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            SQliteDataAccess.Update(sql, parameters);
        }

        public void Delete(int id)
        {
            string sql = "delete from delete_image where id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            SQliteDataAccess.Update(sql, parameters);
        }
    }
}
