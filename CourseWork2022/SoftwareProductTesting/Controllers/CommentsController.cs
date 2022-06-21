using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareProductTesting.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareProductTesting.Controllers
{
    public class CommentsController : Controller
    {
        private readonly SoftwareProductTestingContext _comContext;

        public CommentsController(SoftwareProductTestingContext context)
        {
            _comContext = context;
        }
        // GET: AddresseesController
        public async Task<IActionResult> Index()
        {
            return View(await _comContext.Comments.ToListAsync());
        }

        // GET: AddresseesController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _comContext.Comments.Where(x => x.CommentId == id).Include(x => x.Messages).FirstOrDefaultAsync());
        }

        // GET: AddresseesController/Create

        public IActionResult Create()
        {
            return View();
        }

        // POST: AddresseesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Comment comment)
        {

            if (comment.Content == null )
                throw new ArgumentException(nameof(comment));

            _comContext.Comments.Add(comment);
            await _comContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: AddresseesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _comContext.Comments.Where(x => x.CommentId == id).FirstOrDefaultAsync());
        }

        // POST: AddresseesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Comment comment)
        {
            try
            {
                comment.CommentId = id;
                var item = _comContext.Comments.Attach(comment);

                _comContext.Entry(comment).State = EntityState.Modified;
                await _comContext.SaveChangesAsync();
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
            return View(await _comContext.Comments.Where(x => x.CommentId == id).FirstOrDefaultAsync());
        }

        // POST: AddresseesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Comment comment)
        {
            try
            {
                var item = await _comContext.Comments.FindAsync(id);
                var result = item != null;
                if (result)
                {
                    _comContext.Entry(item).State = EntityState.Deleted;
                    await _comContext.SaveChangesAsync();
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
