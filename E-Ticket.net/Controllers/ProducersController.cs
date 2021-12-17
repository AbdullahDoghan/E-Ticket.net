using E_Ticket.net.Data.DbContextConfg;
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
    [Authorize (Roles =UserRoles.Admin)]
    public class ProducersController : Controller
    {
        private readonly IProducerService services;

        public ProducersController(IProducerService services)
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
            var Producer = await services.GetById(Id);
            if (Producer == null) return View("NotFound");
            return View(Producer);

        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName, Photo, Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
           await services.AddAsync(producer);
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
        public async Task<IActionResult> Edit(int Id,[Bind("Id,FullName, Photo, Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            if (Id == producer.Id)
            {
                await services.UpdateAsync(Id, producer);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
           
        }
        public async Task<IActionResult> Delete(int Id)
        {
            var Producer = await services.GetById(Id);
            if (Producer == null) return View("NotFound");
            return View(Producer);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirme(int Id)
        {
            var Producer = await services.GetById(Id);
            if (Producer == null) return View("NotFound");

            await services.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));

        }



    }
}
