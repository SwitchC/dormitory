using System;
using System.Collections.Generic;

namespace Dormitary
{
    public partial class Employee
    {
        public Employee()
        {
            Strikes = new HashSet<Strike>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? NameDormitory { get; set; }
        public string Job { get; set; } = null!;
        public string Pasword { get; set; } = null!;

        public virtual Dormitory? NameDormitoryNavigation { get; set; }
        public virtual ICollection<Strike> Strikes { get; set; }
    }
}
