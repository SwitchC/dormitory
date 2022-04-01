using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dormitory
{
    public partial class Floor
    {
        public Floor()
        {
            Bloсks = new HashSet<Bloсk>();
            Rooms = new HashSet<Room>();
        }
        [Required(ErrorMessage ="Поле не повинно бути порожнім")]
        [Display(Name ="Номер поверху")]
        public int NumberFlor { get; set; }
        public string? Info { get; set; }
        [Required(ErrorMessage ="Поле не повинно бути прожнім")]
        [Display(Name ="Назва гуртожитку")]
        public string NameDormitory { get; set; } = null!;

        public virtual Dormitory NameDormitoryNavigation { get; set; } = null!;
        public virtual ICollection<Bloсk> Bloсks { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
