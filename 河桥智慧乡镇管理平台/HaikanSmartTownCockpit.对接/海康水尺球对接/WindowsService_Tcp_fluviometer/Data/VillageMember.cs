namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VillageMember")]
    public partial class VillageMember
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VillageMember()
        {
            ResponseInfo = new HashSet<ResponseInfo>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [Key]
        public Guid VillageMemberUUID { get; set; }

        [StringLength(10)]
        public string MemberName { get; set; }

        [StringLength(2)]
        public string MemberSex { get; set; }

        [StringLength(20)]
        public string MemberPhone { get; set; }

        public int? IsDelete { get; set; }

        public Guid? VillageUUID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResponseInfo> ResponseInfo { get; set; }

        public virtual Village Village { get; set; }
    }
}
