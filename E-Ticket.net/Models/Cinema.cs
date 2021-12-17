using E_Ticket.net.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.net.Models
{
    public class Cinema:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Logo")]
        [Required(ErrorMessage = "The Photo is Required!!")]
        public string ActorPhoto { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "The Name is Required!!")]
        public string FullName { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "The Description is Required!!")]
        public string Bio { get; set; }



        public List<Moive> Movies { get; set; }
    }
}
