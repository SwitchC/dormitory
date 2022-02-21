using System;
using System.Collections.Generic;

namespace Dormitary
{
    public partial class Room
    {
        public int Number { get; set; }
        public float Area { get; set; }
        public int NumberFloor { get; set; }
        public string NameDormitory { get; set; } = null!;
        public string? Info { get; set; }

        public virtual Floor N { get; set; } = null!;
        public virtual Kitchen Kitchen { get; set; } = null!;
        public virtual LaundryRoom LaundryRoom { get; set; } = null!;
        public virtual LivingRoom LivingRoom { get; set; } = null!;
    }
}
