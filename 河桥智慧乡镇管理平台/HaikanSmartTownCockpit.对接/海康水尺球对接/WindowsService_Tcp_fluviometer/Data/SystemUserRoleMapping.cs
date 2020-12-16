namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SystemUserRoleMapping")]
    public partial class SystemUserRoleMapping
    {
        [Key]
        [Column(Order = 0)]
        public Guid SystemUserUUID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid SystemRoleUUID { get; set; }

        [Required]
        [StringLength(255)]
        public string AddTime { get; set; }

        [StringLength(255)]
        public string AddPeople { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
    }
}
