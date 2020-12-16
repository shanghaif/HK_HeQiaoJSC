using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class Note
    {
        public Guid NoteUuid { get; set; }
        public int Id { get; set; }
        public string Naem { get; set; }
        public string Content { get; set; }
        public string ModuleUuid { get; set; }
        public string ModuleNaem { get; set; }
        public string EstablishTime { get; set; }
        public string EstablishName { get; set; }
    }
}
