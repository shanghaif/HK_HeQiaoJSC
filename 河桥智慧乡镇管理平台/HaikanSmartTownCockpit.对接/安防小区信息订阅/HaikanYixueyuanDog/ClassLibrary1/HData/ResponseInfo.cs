namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ResponseInfo")]
    public partial class ResponseInfo
    {
        [Key]
        public Guid ResponseInfoUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string Village { get; set; }

        [StringLength(255)]
        public string TongzhiRen { get; set; }

        [StringLength(255)]
        public string XiangyingDj { get; set; }

        [StringLength(255)]
        public string TongzhiQIngk { get; set; }

        [StringLength(255)]
        public string ZaiciTongzhi { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        public Guid? VillageMemberUUID { get; set; }

        public Guid? ResponseInitUUID { get; set; }

        public int? State { get; set; }

        public virtual ResponseInit ResponseInit { get; set; }

        public virtual VillageMember VillageMember { get; set; }
    }
}
