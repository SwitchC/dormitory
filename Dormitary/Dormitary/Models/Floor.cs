using System;
using System.Collections.Generic;

namespace Dormitary
{
    public partial class Floor
    {
        public Floor()
        {
            Bloсks = new HashSet<Bloсk>();
            Rooms = new HashSet<Room>();
        }

        public int NumberFlor { get; set; }
        public string? Info { get; set; }
        public string NameDormitory { get; set; } = null!;

        public virtual Dormitory NameDormitoryNavigation { get; set; } = null!;
        public virtual ICollection<Bloсk> Bloсks { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
