using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Emergency
{
    public class DecompViewModel
    {
        public Guid? TaskDecompositionUuid { get; set; }
        public string Commander { get; set; }
        public string Member { get; set; }
        
        
    }
}
