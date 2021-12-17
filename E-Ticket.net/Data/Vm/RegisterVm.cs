using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.net.Data.Vm
{
    public class RegisterVm
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is requited!!")]
        public string FullName { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage ="Email Address is requited!!")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Confirm Password")]
        [Required (ErrorMessage = "ConfirmPassword is requited!!")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Password don not match")]
        public string ConfirmPassword { get; set; }
    }
}
