using System;
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
        public string from { get; set; }
        public string created_at { get; set; }

        public Delete_image(int id, string name, string from)
        {
            this.id = id;
            this.name = name;
            this.from = from;
            this.created_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public List<Delete_image> GetAll()
        {
            return SQliteDataAccess.GetAll<Delete_image>("delete_image");
        }

        public List<Delete_image> GetRow(string sql)
        {
            return SQliteDataAccess.GetRow<Delete_image>(sql);
        }

        public List<Delete_image> GetRow(int id)
        {
            return SQliteDataAccess.GetRow<Delete_image>("select * from delete_image where id = " + id);
        }

        public void Save()
        {
            string sql = "INSERT INTO delete_image (id, name, from, created_at) VALUES (@id, @name, @from, @created_at)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            parameters.Add("@name", name);
            parameters.Add("@from", from);
            parameters.Add("@created_at", created_at);
            SQliteDataAccess.InserTnputDB(sql, parameters);
        }

        public void Update()
        { 
            string sql = "update delete_image set name = @name, from = @from, created_at = @created_at where id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            parameters.Add("@name", name);
            parameters.Add("@from", from);
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
