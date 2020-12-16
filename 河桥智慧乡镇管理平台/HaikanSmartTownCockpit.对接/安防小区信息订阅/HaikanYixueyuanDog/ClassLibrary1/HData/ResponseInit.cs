namespace ClassLibrary1.HData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ResponseInit")]
    public partial class ResponseInit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ResponseInit()
        {
            ResponseInfo = new HashSet<ResponseInfo>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateTime { get; set; }

        [StringLength(255)]
        public string Addpeoople { get; set; }

        [Key]
        public Guid ResponseInitUUID { get; set; }

        public string Villages { get; set; }

        [StringLength(255)]
        public string Level { get; set; }

        [StringLength(255)]
        public string Situation { get; set; }

        public int? IsDelete { get; set; }

        public int? ReleaseState { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResponseInfo> ResponseInfo { get; set; }
    }
}
