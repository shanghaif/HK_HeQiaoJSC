namespace WindowsService_Tcp_fluviometer.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MissionJournal")]
    public partial class MissionJournal
    {
        [Key]
        public Guid MissionJournalUUID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public Guid? MissionUUID { get; set; }

        [StringLength(255)]
        public string Content { get; set; }

        [StringLength(255)]
        public string Accessory { get; set; }

        [StringLength(255)]
        public string EstablishTime { get; set; }

        [StringLength(255)]
        public string EstablishName { get; set; }

        public int? IsDeleted { get; set; }

        [StringLength(255)]
        public string Read { get; set; }

        [StringLength(255)]
        public string Completed { get; set; }

        [StringLength(255)]
        public string Coordination { get; set; }
    }
}
