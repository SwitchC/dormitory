using System;
using System.Collections.Generic;

namespace dormitory
{
    public partial class Bloсk
    {
        public Bloсk()
        {
            LivingRooms = new HashSet<LivingRoom>();
        }

        public int Number { get; set; }
        public float? Electricity { get; set; }
        public int NumberFloor { get; set; }
        public string NameDormitory { get; set; } = null!;

        public virtual Floor N { get; set; } = null!;
        public virtual ICollection<LivingRoom> LivingRooms { get; set; }
    }
}
