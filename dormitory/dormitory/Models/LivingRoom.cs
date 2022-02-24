using System;
using System.Collections.Generic;

namespace dormitory
{
    public partial class LivingRoom
    {
        public LivingRoom()
        {
            Students = new HashSet<Student>();
        }

        public int NumberRoom { get; set; }
        public string NameDormitory { get; set; } = null!;
        public decimal Cost { get; set; }
        public int NumberBlock { get; set; }
        public int Capacity { get; set; }

        public virtual Bloсk N { get; set; } = null!;
        public virtual Room NNavigation { get; set; } = null!;
        public virtual ICollection<Student> Students { get; set; }
    }
}
