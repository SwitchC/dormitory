using System;
using System.Collections.Generic;

namespace Dormitary
{
    public partial class LaundryRoom
    {
        public int NumberRoom { get; set; }
        public string NameDormitory { get; set; } = null!;
        public int NumberOfWashingMachine { get; set; }
        public int NumberOfDryer { get; set; }

        public virtual Room N { get; set; } = null!;
    }
}
