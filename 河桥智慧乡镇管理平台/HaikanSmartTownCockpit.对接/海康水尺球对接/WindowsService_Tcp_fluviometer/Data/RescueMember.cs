namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RescueMember")]
    public partial class RescueMember
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Key]
        public Guid RescueMemberUUID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateTime { get; set; }

        public Guid? RescueTeamUUID { get; set; }

        [StringLength(10)]
        public string MemberName { get; set; }

        [StringLength(2)]
        public string MemberSex { get; set; }

        [StringLength(20)]
        public string MemberPhone { get; set; }

        public int? IsDelete { get; set; }

        public virtual RescueTeam RescueTeam { get; set; }
    }
}
