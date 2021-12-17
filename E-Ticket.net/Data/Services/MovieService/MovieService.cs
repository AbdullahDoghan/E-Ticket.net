using E_Ticket.net.Data.Base;
using E_Ticket.net.Data.DbContextConfg;
using E_Ticket.net.Data.Vm;
using E_Ticket.net.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.net.Data.Services.MovieService
{
    public class MovieService: EntityBaseRepository<Moive>, IMovieService
    {
        private readonly AppDbContext _db;
        public MovieService(AppDbContext db):base(db)
        {
            _db = db;
        }

        public async Task AddNewMovie(NewMoiveVm newMoive)
        {
            var newMovie = new Moive()
            {
                Name = newMoive.Name,
                Description = newMoive.Description,
                Price = newMoive.Price,
                ImageURL = newMoive.ImageURL,
                CinemaId = newMoive.CinemaId,
                StartDate = newMoive.StartDate,
                EndDate = newMoive.EndDate,
                MovieCategory = newMoive.MovieCategory,
                ProducerId = newMoive.ProducerId,


            };
            await _db.Movies.AddAsync(newMovie);
            await _db.SaveChangesAsync();

            foreach (var ActorId in newMoive.ActorIds)
            {
                var newActor = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = ActorId,
                };
                await _db.Actor_Movies.AddAsync(newActor);
                
            }
            await _db.SaveChangesAsync();
        }

        public async Task<Moive> GetMovieById(int Id)
        {
            var detalis = await _db.Movies.Include(n => n.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actor_Movies)
                .ThenInclude(a => a.Actor).FirstOrDefaultAsync(x => x.Id == Id);
            return  detalis;

        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownsVM()
            {
                Actors = await _db.Actors.OrderBy(x => x.Name).ToListAsync(),
                Cinemas = await _db.Cinemas.OrderBy(n => n.FullName).ToListAsync(),
                Producers = await _db.producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }

        public async Task UpdateMovieAsync(NewMoiveVm newMoive)
        {
            var dbMovie = await _db.Movies.FirstOrDefaultAsync(n => n.Id == newMoive.Id);

            if (dbMovie != null)
            {
                dbMovie.Name = newMoive.Name;
                dbMovie.Description = newMoive.Description;
                dbMovie.Price = newMoive.Price;
                dbMovie.ImageURL = newMoive.ImageURL;
                dbMovie.CinemaId = newMoive.CinemaId;
                dbMovie.StartDate = newMoive.StartDate;
                dbMovie.EndDate = newMoive.EndDate;
                dbMovie.MovieCategory = newMoive.MovieCategory;
                dbMovie.ProducerId = newMoive.ProducerId;
                await _db.SaveChangesAsync();
            }

            //Remove existing actors
            var existingActorsDb = _db.Actor_Movies.Where(n => n.MovieId == newMoive.Id).ToList();
            _db.Actor_Movies.RemoveRange(existingActorsDb);
            await _db.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in newMoive.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMoive.Id,
                    ActorId = actorId
                };
                await _db.Actor_Movies.AddAsync(newActorMovie);
            }
            await _db.SaveChangesAsync();
        }
    }   
}
