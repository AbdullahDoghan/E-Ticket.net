using E_Ticket.net.Data.Base;
using E_Ticket.net.Data.DbContextConfg;
using E_Ticket.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.net.Data.Services.CinemaService
{
    public class CinemaService: EntityBaseRepository<Cinema>, ICinemaService
    {
        public CinemaService(AppDbContext db) : base(db) { }
    }
}
