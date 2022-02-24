using System;
using System.Collections.Generic;

namespace dormitory
{
    public partial class Kitchen
    {
        public int NumberRoom { get; set; }
        public string NameDormitory { get; set; } = null!;
        public int? NumberOfGasStoves { get; set; }
        public int? NumberOfSinks { get; set; }

        public virtual Room N { get; set; } = null!;
    }
}
