using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanSmartTownCockpit.Api.ViewModels.Organization
{
    public class OrgainImportViewModel
    {
        public IFormFile excelfile { get; set; }
        public Guid guid { get; set; }
    }
}
