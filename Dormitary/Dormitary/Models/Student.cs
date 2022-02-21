using System;
using System.Collections.Generic;

namespace Dormitary
{
    public partial class Student
    {
        public Student()
        {
            Strikes = new HashSet<Strike>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Faculty { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int Course { get; set; }
        public decimal Balance { get; set; }
        public int NumberRoom { get; set; }
        public string NameDormitory { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual LivingRoom N { get; set; } = null!;
        public virtual ICollection<Strike> Strikes { get; set; }
    }
}
