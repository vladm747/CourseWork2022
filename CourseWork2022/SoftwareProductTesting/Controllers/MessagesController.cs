using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareProductTesting.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareProductTesting.Controllers
{
    public class MessagesController : Controller
    {
        private readonly SoftwareProductTestingContext _messContext;

        public MessagesController(SoftwareProductTestingContext context)
        {
            _messContext = context;
        }
        // GET: AddresseesController
        public async Task<IActionResult> Index()
        {
            return View(await _messContext.Messages
                .Include("Receiver")
                .Include("CommentContent")
                .Include("Sender")
                .ToListAsync());
        }

        // GET: AddresseesController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _messContext.Messages.Where(x => x.MessageId == id)
                .Include("Receiver")
                .Include("CommentContent")
                .Include("Sender")
                .FirstOrDefaultAsync());
        }

        // GET: AddresseesController/Create

        public IActionResult Create()
        {
            return View();
        }

        // POST: AddresseesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Message message)
        {

            _messContext.Messages.Add(message);
            await _messContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: AddresseesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _messContext.Messages.Where(x => x.MessageId == id)
                .Include("Receiver")
                .Include("CommentContent")
                .Include("Sender")
                .FirstOrDefaultAsync());
        }

        // POST: AddresseesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Message message)
        {
            try
            {
                message.MessageId = id;
                var item = _messContext.Messages.Attach(message);

                _messContext.Entry(message).State = EntityState.Modified;
                await _messContext.SaveChangesAsync();
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
            return View(await _messContext.Messages.Where(x => x.MessageId == id)
                .Include("Receiver")
                .Include("CommentContent")
                .Include("Sender")
                .FirstOrDefaultAsync());
        }

        // POST: AddresseesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Message message)
        {
            try
            {
                var item = await _messContext.Messages.FindAsync(id);
                var result = item != null;
                if (result)
                {
                    _messContext.Entry(item).State = EntityState.Deleted;
                    await _messContext.SaveChangesAsync();
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
