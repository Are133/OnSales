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
            return View(await _contex.Countries.Include(estate => estate.Estates).ToListAsync());
        }

        #region Countries

        public async Task<IActionResult> AddOrEdit(int? id)
        {
            if(id == null)
            {
                return View();
            }

            var country = await _contex.Countries.FindAsync(id);

            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(Country country, int id)
        {
            if(country.Id == 0)
            {
                try
                {
                    _contex.Add(country);
                    await _contex.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Este pais ya esta registrado.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            if(id > 0)
            {
                try
                {
                    _contex.Update(country);
                    await _contex.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Este pais ya esta registrado.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            
            return View();
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _contex.Countries
                .Include(estate => estate.Estates)
                .ThenInclude(municipipality => municipipality.Municipalities)
                .FirstOrDefaultAsync(me => me.Id == id);

            if (country == null)
            {
                return NotFound();
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

        #endregion

        #region Estates
        public async Task<IActionResult> AddOrEditEstates(int? id)
        {

            var country = await _contex.Countries.FindAsync(id);


            var model = new Estate { Id = country.Id };


            return View(model);
        }

        public async Task<IActionResult> AddOrEditEstatesEdit(int? id)
        {

            var country = await _contex.Estates.FindAsync(id);

            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditEstates(Estate estate)
        {
            if (ModelState.IsValid)
            {
                var country = await _contex.Countries
                    .Include(c => c.Estates)
                    .FirstOrDefaultAsync(c => c.Id == estate.IdCountry);
                if (country == null)
                {
                    return NotFound();
                }

                try
                {
                    string url = "https://localhost:44310/Countries/Details";
                    string cadenaMoreId = $"{url}/{country.Id}";
                    estate.Id = 0;
                    country.Estates.Add(estate);
                    _contex.Update(country);
                    await _contex.SaveChangesAsync();
                    return Redirect(cadenaMoreId);


                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            if (ModelState.IsValid)
            {
                var country = await _contex.Countries
                    .Include(c => c.Estates)
                    .FirstOrDefaultAsync(c => c.Id == estate.IdCountry);
                if (country == null)
                {
                    return NotFound();
                }

                try
                {
                    string url = "https://localhost:44310/Countries/Details";
                    string cadenaMoreId = $"{url}/{country.Id}";
                    estate.Id = 0;
                    country.Estates.Add(estate);
                    _contex.Update(country);
                    await _contex.SaveChangesAsync();
                    return Redirect(cadenaMoreId);


                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(estate);
        }



        public async Task<IActionResult> DetailsOfEstates(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estates = await _contex.Estates
                .Include(d => d.Municipalities)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estates == null)
            {
                return NotFound();
            }

            var country = await _contex.Estates.FirstOrDefaultAsync(estate => estate.Municipalities.FirstOrDefault(d => d.Id == estates.Id) != null);
            if(estates.Municipalities.Count() > 0)
            {
                estates.IdCountry = country.Id;
            }
           
            return View(estates);
        }
        #endregion






    }
}
