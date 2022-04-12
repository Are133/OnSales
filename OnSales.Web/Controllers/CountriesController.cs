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
            return View(await _contex.Countries.ToListAsync());
        }


        public IActionResult AddOrEdit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(Country country)
        {
            
                if(country.Id == 0)
                {
                    _contex.Add(country);
                }
                else
                {
                    _contex.Update(country);
                }

                await _contex.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
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
