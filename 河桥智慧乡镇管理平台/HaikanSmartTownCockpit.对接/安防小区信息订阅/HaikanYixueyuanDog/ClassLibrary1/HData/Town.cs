namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Town")]
    public partial class Town
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Town()
        {
            Userinfoty = new HashSet<Userinfoty>();
        }

        [Key]
        public Guid TownUuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? IsDeleted { get; set; }

        public string TownName { get; set; }

        [StringLength(255)]
        public string TownPeople { get; set; }

        [StringLength(255)]
        public string PartyMember { get; set; }

        [StringLength(255)]
        public string Geographical { get; set; }

        [StringLength(255)]
        public string Company { get; set; }

        public Guid? SjTownUuid { get; set; }

        [StringLength(255)]
        public string TownGrade { get; set; }

        [StringLength(255)]
        public string Lon { get; set; }

        [StringLength(255)]
        public string Lat { get; set; }

        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Userinfoty> Userinfoty { get; set; }
    }
}
