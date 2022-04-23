using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace dormitory
{
    public partial class Strike
    {
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Тип")]
        public string Type { get; set; } = null!;
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Стан")]
        public string State { get; set; } = null!;
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "StudentId")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "EmployeeId")]
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
