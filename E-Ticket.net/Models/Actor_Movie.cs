using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.net.Models
{
    public class Actor_Movie
    {
        public int MovieId { get; set; }
        public Moive Movie { get; set; }

        public int ActorId { get; set; }
        public Actor Actor { get; set; }
    }
}
