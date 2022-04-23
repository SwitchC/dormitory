using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace dormitory
{
    public partial class Room
    {
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Номер кімнати")]
        public int Number { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Площа")]
        public float Area { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Поверх")]
        public int NumberFloor { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва гуртожитку")]
        public string NameDormitory { get; set; } = null!;
        [Display(Name = "Інформація")]
        public string? Info { get; set; }

        public virtual Floor N { get; set; } = null!;
        public virtual Kitchen Kitchen { get; set; } = null!;
        public virtual LaundryRoom LaundryRoom { get; set; } = null!;
        public virtual LivingRoom LivingRoom { get; set; } = null!;
    }
}
