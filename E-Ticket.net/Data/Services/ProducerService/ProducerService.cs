using E_Ticket.net.Data.Base;
using E_Ticket.net.Data.DbContextConfg;
using E_Ticket.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.net.Data.Services.ProducerService
{
    public class ProducerService: EntityBaseRepository<Producer>, IProducerService
    {
        public ProducerService(AppDbContext db) : base(db) { }
    }
}
