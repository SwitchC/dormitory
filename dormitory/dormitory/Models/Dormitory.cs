using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace dormitory
{
    public partial class Dormitory
    {
        public Dormitory()
        {
            Employees = new HashSet<Employee>();
            Floors = new HashSet<Floor>();
        }
        [Required(ErrorMessage ="Поле не повинно бути порожнім")]
        [Display(Name="Назва")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name="Адреса")]
        public string Address { get; set; } = null!;
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name="Номер телефону")]
        public string PhoneNumber { get; set; } = null!;
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name="email")]
        public string Email { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Floor> Floors { get; set; }
    }
}
