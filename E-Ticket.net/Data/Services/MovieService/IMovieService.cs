using E_Ticket.net.Data.Base;
using E_Ticket.net.Data.Vm;
using E_Ticket.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.net.Data.Services.MovieService
{
    public interface IMovieService: IEntityBaseRepository<Moive>
    {
        Task<Moive> GetMovieById(int Id);
        Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues();

        Task AddNewMovie(NewMoiveVm newMoive);

        Task UpdateMovieAsync(NewMoiveVm newMoive);
    }
}
