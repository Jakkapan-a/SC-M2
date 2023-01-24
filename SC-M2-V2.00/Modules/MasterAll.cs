using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_M2_V2._00.Modules
{
    class MasterAll
    {
        public int id_sw{ get; set; }
        public int id_lb { get; set; }
        public string nameSW { get; set; }
        public string nameModel { get; set; }

        public string created_at { get; set; }
        public string updated_at { get; set; }

        public static List<MasterAll> GetMasterAll(int id) => SQliteDataAccess.GetRow<MasterAll>("select m.id as id_sw,s.id as id_lb, m.name as nameSW, s.name as nameModel, m.created_at as created_at, m.updated_at as updated_at from master_sw m left join master_lb s on m.id = s.master_sw_id where m.id = " + id + " order by m.id desc");
        public static List<MasterAll> GetMasterAll() => SQliteDataAccess.GetRow<MasterAll>("select m.id as id_sw,s.id as id_lb, m.name as nameSW, s.name as nameModel, m.created_at as created_at, m.updated_at as updated_at from master_sw m left join master_lb s on m.id = s.master_sw_id order by m.id desc");
    }
}
