using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnSales.Common.Entities;
using OnSales.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSales.Web.Controllers
{
    public class CountriesController : Controller
    {
        private readonly DataContext _contex;

        public CountriesController(DataContext context)
        {
            _contex = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _contex.Countries.OrderByDescending(countrie => countrie.Name).ToListAsync());
        }

        public async Task<IActionResult>Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var countrie = await _contex.Countries.FirstOrDefaultAsync(c => c.Id == id);

            if(countrie == null)
            {
                return NotFound();
            }

            return View(countrie);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AddOrEdit(Country country)
        {
            if (ModelState.IsValid)
            {
                _contex.Add(country);
                await _contex.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(country);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var countrie = await _contex.Countries.FirstOrDefaultAsync(c => c.Id == id);
            _contex.Countries.Remove(countrie);
            await _contex.SaveChangesAsync();
            return Ok(countrie);
        }
    }
}
