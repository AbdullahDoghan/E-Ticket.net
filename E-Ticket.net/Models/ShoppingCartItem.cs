using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.net.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public Moive Moive { get; set; }

        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }

    }
}
