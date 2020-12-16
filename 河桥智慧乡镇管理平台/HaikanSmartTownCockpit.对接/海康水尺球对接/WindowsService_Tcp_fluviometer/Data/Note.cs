namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Note")]
    public partial class Note
    {
        [Key]
        public Guid NoteUUID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        public string Naem { get; set; }

        [StringLength(255)]
        public string Content { get; set; }

        public string ModuleUUID { get; set; }

        [StringLength(255)]
        public string ModuleNaem { get; set; }

        [StringLength(255)]
        public string EstablishTime { get; set; }

        [StringLength(255)]
        public string EstablishName { get; set; }
    }
}
