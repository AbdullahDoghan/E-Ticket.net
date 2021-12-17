using E_Ticket.net.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.net.Models
{
    public class Producer: IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Photo")]
        [Required(ErrorMessage = "The Photo is Required!!")]
        public string Photo { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "The Name is Required!!")]
        public string FullName { get; set; }

        [Display(Name = "Bio")]
        [Required(ErrorMessage = "The Description is Required!!")]
        public string Bio { get; set; }


        public List<Moive> Movies { get; set; }
    }

}
