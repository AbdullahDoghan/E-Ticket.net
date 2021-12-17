using E_Ticket.net.Data.DbContextConfg;
using E_Ticket.net.Data.Services.ActorServices;
using E_Ticket.net.Data.Static;
using E_Ticket.net.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.net.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ActorsController : Controller
    {
        private readonly IActorService services;

        public ActorsController(IActorService services)
        {
            this.services = services;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var result = await services.GetAll();
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> Create([Bind("Name, Logo, Description")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await services.AddAsync(actor);
            return RedirectToAction(nameof(Index));

        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int Id)
        {
            var actor = await services.GetById(Id);
            if (actor == null) return View("NotFound");
            return View(actor);
        
        }
        public async Task<IActionResult> Edit( int Id)
        {
            var actor = await services.GetById(Id);
            if (actor == null) return View("NotFound");
            return View(actor);
        }


        [HttpPost]

        public async Task<IActionResult> Edit(int Id,[Bind("Id,Name, Logo, Description")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await services.UpdateAsync(Id,actor);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int Id)
        {
            var actor = await services.GetById(Id);
            if (actor == null) return View("NotFound");
            return View(actor);
        }


        [HttpPost, ActionName("Delete")] 
        public async Task<IActionResult> DeleteConfirme(int Id)
        {
            var actor = await services.GetById(Id);
            if (actor == null) return View("NotFound");

            await services.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));

        }
    }
}
