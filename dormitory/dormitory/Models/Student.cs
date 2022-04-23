using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace dormitory
{
    public partial class Student
    {
        public Student()
        {
            Strikes = new HashSet<Strike>();
        }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Ім'я")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Факультет")]
        public string Faculty { get; set; } = null!;
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Номер телефону")]
        public string PhoneNumber { get; set; } = null!;
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Курс")]
        public int Course { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Баланс")]
        public decimal Balance { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Номер кімнати")]
        public int NumberRoom { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва гуртожитку")]
        public string NameDormitory { get; set; } = null!;
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = null!;

        public virtual LivingRoom N { get; set; } = null!;
        public virtual ICollection<Strike> Strikes { get; set; }
    }
}
