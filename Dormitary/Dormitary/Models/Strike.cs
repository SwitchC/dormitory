using System;
using System.Collections.Generic;

namespace Dormitary
{
    public partial class Strike
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string State { get; set; } = null!;
        public int StudentId { get; set; }
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
