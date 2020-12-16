namespace HaiKanTaskHousekeeperCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Security")]
    public partial class Security
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Security()
        {
            SecurityReport = new HashSet<SecurityReport>();
        }

        [Key]
        public Guid SecurityUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string SecurityName { get; set; }

        [StringLength(255)]
        public string SecurityAddress { get; set; }

        [StringLength(255)]
        public string IdentityCard { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string SecurityTime { get; set; }

        [StringLength(255)]
        public string SecurityStaues { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SecurityReport> SecurityReport { get; set; }
    }
}
