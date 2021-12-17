using E_Ticket.net.Data.Base;
using E_Ticket.net.Data.DbContextConfg;
using E_Ticket.net.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.net.Data.Services.ActorServices
{
    public class ActorService : EntityBaseRepository<Actor>, IActorService
    {
        public ActorService(AppDbContext db) : base(db) { }
      
    }
}
