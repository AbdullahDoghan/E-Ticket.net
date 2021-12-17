using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.net.Data.Vm
{
    public class LoginVm
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage ="Email Address is requited!!")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
