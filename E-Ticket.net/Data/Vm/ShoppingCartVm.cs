using E_Ticket.net.Data.Services.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.net.Data.Vm
{
    public class ShoppingCartVm
    {
        public ShoppingCart shoppingCart { get; set; }

        public double shoppingCartTotal { get; set; }
    }
}
