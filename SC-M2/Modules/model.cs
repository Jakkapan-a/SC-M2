using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_M2.Modules
{
    internal class Model
    {
        [DisplayName("ID")]
        public int id { get; set; }
        [DisplayName("Model")]
        public string name { get; set; }
        [DisplayName("Full Name")]
        public string fullname { get; set; }
        [DisplayName("Accept")]
        public int percent { get; set; }
        [DisplayName("Date")]
        public string created_at { get; set; }
        [DisplayName("Update")]
        public string updated_at { get; set; }
        public Model()
        {
            
        }
        public Model(int id)
        {
            var data = SQliteDataAccess.GetRow<Model>("select * from model where id = " + id);
            this.id = data[0].id;
            this.name = data[0].name;
            this.fullname = data[0].fullname;
            this.percent = data[0].percent;
            this.created_at = data[0].created_at;
            this.updated_at = data[0].updated_at;

        }
        public static List<Model> GetAll() => SQliteDataAccess.GetAllNolimit<Model>("model");
        

        public static List<Model> GetRow(string sql) => SQliteDataAccess.GetRow<Model>(sql);
        
        public void GetRow()
        {
            var data = SQliteDataAccess.GetRow<Model>("select * from model where id = " + id);
            if(data.Count > 0)
            {
                this.name = data[0].name;
                this.fullname = data[0].fullname;
                this.percent = data[0].percent;
                this.created_at= data[0].created_at;
                this.updated_at= data[0].updated_at;
            }
        }
        public static List<Model> GetRow(int id) => SQliteDataAccess.GetRow<Model>("select * from model where id = " + id);
        
        public static List<Model> GetByName(string model_name) => SQliteDataAccess.GetRow<Model>("select * from model where name = '" + model_name+"'");

        public bool isName() => (SQliteDataAccess.GetRow<Model>("select * from model where name = '" + name + "'").Count > 0) ? true : false;

        public bool isnNotName() => (SQliteDataAccess.GetRow<Model>("select * from model where name = '" + name + "'").Count > 1) ? true : false;

        public void Save()
        {
            string sql = "INSERT INTO model (name, fullname, percent, created_at, updated_at) VALUES (@name, @fullname, @percent, @created_at, @updated_at)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@name", name);
            parameters.Add("@fullname", fullname);
            parameters.Add("@percent", percent);
            parameters.Add("@created_at", GetDateTimeNow());
            parameters.Add("@updated_at", GetDateTimeNow());
            SQliteDataAccess.InserInputDB(sql, parameters);
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
