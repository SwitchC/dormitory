using System;
using System.Collections.Generic;

namespace dormitory
{
    public partial class Dormitory
    {
        public Dormitory()
        {
            Employees = new HashSet<Employee>();
            Floors = new HashSet<Floor>();
        }

        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Floor> Floors { get; set; }
    }
}
