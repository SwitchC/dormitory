using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace dormitory
{

    public partial class Bloсk
    {
        public Bloсk()
        {
            LivingRooms = new HashSet<LivingRoom>();
        }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Номер")]
        public int Number { get; set; }
        [Display(Name = "Електроенергія")]
        public float? Electricity { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Поверх")]
        public int NumberFloor { get; set; }
        [Display(Name = "Гуртожиток")]
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        public string NameDormitory { get; set; } = null!;

        public virtual Floor N { get; set; } = null!;
        public virtual ICollection<LivingRoom> LivingRooms { get; set; }
    }
}
