using E_Ticket.net.Data.DbContextConfg;
using E_Ticket.net.Data.Services.CinemaService;
using E_Ticket.net.Data.Services.ProducerService;
using E_Ticket.net.Data.Static;
using E_Ticket.net.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.net.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CinemaController : Controller
    {
        private readonly AppDbContext _db;

        private readonly ICinemaService services;

        public CinemaController(ICinemaService services)
        {
            this.services = services;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await services.GetAll();
            return View(data);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int Id)
        {
            var cinema = await services.GetById(Id);
            if (cinema == null) return View("NotFound");
            return View(cinema);

        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName, ActorPhoto, Bio")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await services.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var data = await services.GetById(Id);
            if (data == null)
            {
                return View("NotFound");
            }
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int Id, [Bind("Id,FullName, ActorPhoto, Bio")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            if (Id == cinema.Id)
            {
                await services.UpdateAsync(Id, cinema);
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);

        }
        public async Task<IActionResult> Delete(int Id)
        {
            var cinema = await services.GetById(Id);
            if (cinema == null) return View("NotFound");
            return View(cinema);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirme(int Id)
        {
            var cinema = await services.GetById(Id);
            if (cinema == null) return View("NotFound");

            await services.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));

        }
    }
}
