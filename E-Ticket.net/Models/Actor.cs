using E_Ticket.net.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.net.Models
{
    public class Actor: IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Logo")]
        [Required(ErrorMessage ="The Photo is Required!!")]
        public string Logo { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "The Name is Required!!")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "The Description is Required!!")]
        public string Description { get; set; }


        public List<Actor_Movie> Actor_Movies { get; set; }
    }
}
