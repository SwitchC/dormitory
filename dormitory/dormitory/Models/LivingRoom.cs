using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace dormitory
{
    public partial class LivingRoom
    {
        public LivingRoom()
        {
            Students = new HashSet<Student>();
        }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Номер кімнати")]
        public int NumberRoom { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва гуртожитку")]
        public string NameDormitory { get; set; } = null!;
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Вартість")]
        public decimal Cost { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Номер блоку")]
        public int NumberBlock { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Кількість місць")]
        public int Capacity { get; set; }

        public virtual Bloсk N { get; set; } = null!;
        public virtual Room NNavigation { get; set; } = null!;
        public virtual ICollection<Student> Students { get; set; }
    }
}
