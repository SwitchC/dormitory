using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace dormitory
{
    public partial class Employee
    {
        public Employee()
        {
            Strikes = new HashSet<Strike>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Ім'я")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Номер телефону")]
        public string PhoneNumber { get; set; } = null!;
        [Display(Name = "Гуртожиток")]
        public string? NameDormitory { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "робота")]
        public string Job { get; set; } = null!;
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "пароль")]
        public string Pasword { get; set; } = null!;

        public virtual Dormitory? NameDormitoryNavigation { get; set; }
        public virtual ICollection<Strike> Strikes { get; set; }
    }
}
