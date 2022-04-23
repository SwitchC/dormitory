using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace dormitory
{
    public partial class LaundryRoom
    {
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Номер кімнати")]
        public int NumberRoom { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва гуртожитку")]
        public string NameDormitory { get; set; } = null!;
        [Display(Name = "Кількість пралок")]
        public int NumberOfWashingMachine { get; set; }
        [Display(Name = "Кількість сушок")]
        public int NumberOfDryer { get; set; }

        public virtual Room N { get; set; } = null!;
    }
}
