using E_Ticket.net.Data.DbContextConfg;
using E_Ticket.net.Data.Services.MovieService;
using E_Ticket.net.Data.Static;
using E_Ticket.net.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.net.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {
       
        private readonly IMovieService services;

        public MoviesController(IMovieService services)
        {
            this.services = services;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await services.GetAll(n=> n.Cinema);
            return View(data);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await services.GetAll(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies.Where(x => x.Name.ToLower().Contains(searchString.ToLower()) || x.Description.Contains(searchString.ToLower())).ToList();
                return View("Index", filteredResult);
            }
            return View("Index", allMovies);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var details = await services.GetMovieById(Id);
            return View(details);
        
        }

        public async Task<IActionResult> Create()
        {
            var result =await services.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(result.Cinemas, "Id", "FullName");
            ViewBag.Producers = new SelectList(result.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(result.Actors, "Id", "Name");

            return View();        
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMoiveVm newMoiveVm)
        {
            if (!ModelState.IsValid)
            {
                var result = await services.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(result.Cinemas, "Id", "FullName");
                ViewBag.Producers = new SelectList(result.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(result.Actors, "Id", "Name");
                return View(newMoiveVm);
            }
            await services.AddNewMovie(newMoiveVm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var movie = await services.GetMovieById(id);
            if (movie == null)
            {
                return View("NotFound");
            }

            var respone = new NewMoiveVm()
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                Price = movie.Price,
                ImageURL = movie.ImageURL,
                CinemaId = movie.CinemaId,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                MovieCategory = movie.MovieCategory,
                ActorIds = movie.Actor_Movies.Select(x => x.ActorId).ToList(),
                ProducerId = movie.ProducerId,

            };

            var result = await services.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(result.Cinemas, "Id", "FullName");
            ViewBag.Producers = new SelectList(result.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(result.Actors, "Id", "Name");

            return View(respone);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMoiveVm movie)
        {
            if (id != movie.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await services.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }

            await services.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

    }
}
