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

        public async Task<IActionResult>Create(Country country)
        {
            if (ModelState.IsValid)
            {
                _contex.Add(country);
                await _contex.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(country);
        }
        public async Task<IActionResult> Edit(int? id)
        {
           if(id == null)
           {
                return NotFound();
           }

            var countrie = await _contex.Countries.FindAsync(id);

            if(countrie == null)
            {
                return NotFound();
            }

            return View(countrie);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult>Edit(int id, Country country)
        {
            if(id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _contex.Update(country.Id);
                await _contex.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(country);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countrie = await _contex.Countries.FirstOrDefaultAsync(c => c.Id == id);

            if(countrie != null)
            {
                _contex.Countries.Remove(countrie);
                await _contex.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
