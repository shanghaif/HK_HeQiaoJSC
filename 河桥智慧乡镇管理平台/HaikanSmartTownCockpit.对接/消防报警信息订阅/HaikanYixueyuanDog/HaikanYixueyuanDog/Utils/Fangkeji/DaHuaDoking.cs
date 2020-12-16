using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaikanYixueyuanDog.Utils.Fangkeji
{
    public class DaHuaDoking
    {
        public void AttendanceData(string json)
        {
            try
            {
                DaHuaModelRoot dm = JsonConvert.DeserializeObject<DaHuaModelRoot>(json);
            }
            catch (Exception e)
            {

            }

        }
    }
}
