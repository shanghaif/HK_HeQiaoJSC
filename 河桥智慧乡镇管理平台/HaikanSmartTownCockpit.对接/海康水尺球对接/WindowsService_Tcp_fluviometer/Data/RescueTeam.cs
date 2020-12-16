namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RescueTeam")]
    public partial class RescueTeam
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RescueTeam()
        {
            RescueMember = new HashSet<RescueMember>();
        }

        [Key]
        public Guid RescueTeamUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string TeamName { get; set; }

        [StringLength(255)]
        public string TeamCaptain { get; set; }

        [StringLength(255)]
        public string TeamRenshu { get; set; }

        [StringLength(255)]
        public string TeamPhone { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RescueMember> RescueMember { get; set; }
    }
}
