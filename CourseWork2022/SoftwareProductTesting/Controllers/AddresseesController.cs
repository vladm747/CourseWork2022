using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareProductTesting.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareProductTesting.Controllers
{
    public class AddresseesController : Controller
    {
        private readonly SoftwareProductTestingContext _addrContext;

        public AddresseesController(SoftwareProductTestingContext context)
        {
            _addrContext = context;
        }
        // GET: AddresseesController
        public async Task<IActionResult> Index()
        {
            return View(await _addrContext.Addressees.ToListAsync());
        }

        // GET: AddresseesController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _addrContext.Addressees.Where(x => x.PersonId == id).FirstOrDefaultAsync());
        }

        // GET: AddresseesController/Create
        
        public IActionResult Create()
        {
            return View();
        }

        // POST: AddresseesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Addressee addressee)
        {
           
            if (addressee.FullName == null || addressee.Position == null)
                throw new ArgumentException(nameof(addressee));

            _addrContext.Addressees.Add(addressee);
            await _addrContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
           
        }

        // GET: AddresseesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _addrContext.Addressees.Where(x => x.PersonId == id).FirstOrDefaultAsync());
        }

        // POST: AddresseesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Addressee addressee)
        {
            try
            {
                addressee.PersonId = id;
                var item = _addrContext.Addressees.Attach(addressee);
                
                _addrContext.Entry(addressee).State = EntityState.Modified;
                await _addrContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AddresseesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _addrContext.Addressees.Where(x => x.PersonId == id).FirstOrDefaultAsync());
        }

        // POST: AddresseesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Addressee addressee)
        {
            try
            {
                var item = await _addrContext.Addressees.FindAsync(id);
                var result = item != null;
                if (result)
                {
                    _addrContext.Entry(item).State = EntityState.Deleted;
                    await _addrContext.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
