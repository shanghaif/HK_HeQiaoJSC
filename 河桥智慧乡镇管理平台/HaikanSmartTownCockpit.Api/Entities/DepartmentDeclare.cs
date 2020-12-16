using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class DepartmentDeclare
    {
        public Guid DepartmentDeclareUuid { get; set; }
        public string DeclareName { get; set; }
        public string DeclareDepartment { get; set; }
        public string DeclareTime { get; set; }
        public string BonusPoint { get; set; }
        public int? PlusScore { get; set; }
        public string PlusContent { get; set; }
        public string Deduction { get; set; }
        public int? DeductionScore { get; set; }
        public string DeductionContent { get; set; }
        public string Remark { get; set; }
        public string EstablishTime { get; set; }
        public string EstablishName { get; set; }
        public int? IsDeleted { get; set; }
        public Guid? PerformanceDeclareUuid { get; set; }
        public string AuditOpinion { get; set; }
        public string AuditStatus { get; set; }
        public int Id { get; set; }
    }
}
