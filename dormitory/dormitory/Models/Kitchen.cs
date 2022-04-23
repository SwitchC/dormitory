using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace dormitory
{
    public partial class Kitchen
    {
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Номер кімнати")]
        public int NumberRoom { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Гуртожиток")]
        public string NameDormitory { get; set; } = null!;
        [Display(Name = "Кількість газових плит")]
        public int? NumberOfGasStoves { get; set; }
        [Display(Name = "Кількість раковин")]
        public int? NumberOfSinks { get; set; }

        public virtual Room N { get; set; } = null!;
    }
}
