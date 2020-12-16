using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class TcqType
    {
        public Guid Tcqguid { get; set; }
        public int? Tcqid { get; set; }
        public string TcqName { get; set; }
    }
}
