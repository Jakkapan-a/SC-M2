using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_M2.Modules
{
    internal class History : IDisposable
    {
        [DisplayName("ID")]
        public int id { get; set; }
        [DisplayName("Name")]
        public string name { get; set; }
        [DisplayName("Model")]
        public string model { get; set; }
        [DisplayName("Qr Code")]
        public string qrcode { get; set; }
        [DisplayName("Judgement")]
        public string judgement { get; set; }
        [DisplayName("DateTime")]
        public string created_at { get; set; }
        public string updated_at { get; set; }
        void IDisposable.Dispose()
        {

        }
        public static List<History> GetAll() 
        {
           return SQliteDataAccess.GetAll<History>("history");
        }

        public List<History> GetRow(string sql) => SQliteDataAccess.GetRow<History>(sql);

        public List<History> GetRow(int id) => SQliteDataAccess.GetRow<History>("select * from history where id = " + id);
        

        public void Get()
        {
            var data = SQliteDataAccess.GetRow<History>("select * from history where id = " + id);
            if (data.Count != 0)
            {
                this.id = data[0].id;
                this.name = data[0].name;
                this.model = data[0].model;
                this.qrcode = data[0].qrcode;
                this.judgement = data[0].judgement;
                this.created_at = data[0].created_at;
                this.updated_at = data[0].updated_at;
            }
        }

        public void Save()
        {
            string sql = "insert into history (name, model, qrcode, judgement, created_at, updated_at) values (@name, @model, @qrcode, @judgement, @created_at, @updated_at)";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@name", this.name);
            param.Add("@model", this.model);
            param.Add("@qrcode", this.qrcode);
            param.Add("@judgement", this.judgement);
            param.Add("@created_at", GetDateTimeNow());
            param.Add("@updated_at", GetDateTimeNow());
            SQliteDataAccess.InserInputDB(sql, param);
        }

        public void Update()
        {
            string sql = "update history set name = @name, model = @model, qrcode = @qrcode, judgement = @judgement, updated_at = @updated_at where id = " + id;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@name", this.name);
            param.Add("@model", this.model);
            param.Add("@qrcode", this.qrcode);
            param.Add("@judgement", this.judgement);
            param.Add("@updated_at", GetDateTimeNow());
            SQliteDataAccess.InserInputDB(sql, param);
        }
        
        public void Delete()
        {
            string sql = "delete from history where id = @id";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@id", this.id);
            SQliteDataAccess.InserInputDB(sql, param);

        }
        private string GetDateTimeNow()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
