using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class ResponseInit
    {
        public ResponseInit()
        {
            ResponseInfo = new HashSet<ResponseInfo>();
        }

        public int Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Addpeoople { get; set; }
        public Guid ResponseInitUuid { get; set; }
        public string Villages { get; set; }
        public string Level { get; set; }
        public string Situation { get; set; }
        public int? IsDelete { get; set; }
        public int? ReleaseState { get; set; }

        public virtual ICollection<ResponseInfo> ResponseInfo { get; set; }
    }
}
