﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_M2.Modules
{
    internal class Model
    {
        public int id { get; set; }
        public string name { get; set; }
        public string fullname { get; set; }
        public string percent { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        public List<Model> GetAll()
        {
            return SQliteDataAccess.GetAll<Model>("model");
        }

        public List<Model> GetRow(string sql)
        {
            return SQliteDataAccess.GetRow<Model>(sql);
        }

        public List<Model> GetRow(int id)
        {
            return SQliteDataAccess.GetRow<Model>("select * from model where id = " + id);
        }

        public void Save()
        {
            string sql = "INSERT INTO model (name, fullname, percent, created_at, updated_at) VALUES (@name, @fullname, @percent, @created_at, @updated_at)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@name", name);
            parameters.Add("@fullname", fullname);
            parameters.Add("@percent", percent);
            parameters.Add("@created_at", GetDateTimeNow());
            parameters.Add("@updated_at", GetDateTimeNow());
            SQliteDataAccess.InserTnputDB(sql, parameters);
        }

        public void Update()
        {
            string sql = "update model set name = @name, fullname = @fullname, percent = @percent, updated_at = @updated_at where id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            parameters.Add("@name", name);
            parameters.Add("@fullname", fullname);
            parameters.Add("@percent", percent);
            parameters.Add("@updated_at", GetDateTimeNow());
            SQliteDataAccess.Update(sql, parameters);
        }

        public void Delete()
        {
            string sql = "delete from model where id = @id";
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
