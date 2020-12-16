namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Village")]
    public partial class Village
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Village()
        {
            VillageMember = new HashSet<VillageMember>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateTime { get; set; }

        [Key]
        public Guid VillageUUID { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [StringLength(20)]
        public string VillageName { get; set; }

        public int? OrderBy { get; set; }

        public int? IsDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VillageMember> VillageMember { get; set; }
    }
}
