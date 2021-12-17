using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.net.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Display(Name ="FuSll Name")]
        public string FullName { get; set; }


    }
}
