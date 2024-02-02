using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final.Models;
using Final.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace Final.Controllers
{   
    [Authorize(Roles = "Admin")]
    public class LanguageController : Controller
    {      
      
        private readonly LumosTutorIdentityDbContext _context;
        private string Layout = "TutorLayout";
        private string _Layout = "AdminLayout";
        
        public LanguageController(LumosTutorIdentityDbContext context)
        {
            _context = context;
        }

        // GET: Language
        public async Task<IActionResult> Index()
        {   
            ViewBag.Layout = Layout;
            return _context.Language != null ?
                        View(await _context.Language.ToListAsync()) :
                        Problem("Entity set 'LumosTutorIdentityDbContext.Language'  is null.");
        }

        // GET: Language/Details/5
        public async Task<IActionResult> Details(int? id)
        {   
            ViewBag.Layout = Layout;
            if (id == null || _context.Language == null)
            {
                return NotFound();
            }

            var language = await _context.Language
                .FirstOrDefaultAsync(m => m.Id == id);
            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        // GET: Language/Create
        public IActionResult Create()
        {   
            ViewBag.Layout = Layout;
            return View();
        }

        // POST: Language/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status")] Language language)
        {
            ViewBag.Layout = Layout;
            if (ModelState.IsValid)
            {
                _context.Add(language);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(language);
        }

        // GET: Language/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Layout = Layout;
            if (id == null || _context.Language == null)
            {
                return NotFound();
            }

            var language = await _context.Language.FindAsync(id);
            if (language == null)
            {
                return NotFound();
            }


            return View(language);
        }

        // POST: Language/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status")] Language language)
        {   
            ViewBag.Layout = Layout;
            if (id != language.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(language);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanguageExists(language.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(language);
        }

        // GET: Language/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {   
            ViewBag.Layout = Layout;
            if (id == null || _context.Language == null)
            {
                return NotFound();
            }

            var language = await _context.Language
                .FirstOrDefaultAsync(m => m.Id == id);
            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        // POST: Language/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {   
            ViewBag.Layout = Layout;
            if (_context.Language == null)
            {
                return Problem("Entity set 'LumosTutorIdentityDbContext.Language'  is null.");
            }
            var language = await _context.Language.FindAsync(id);
            if (language != null)
            {
                _context.Language.Remove(language);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LanguageExists(int id)
        {   
            ViewBag.Layout = Layout;
            return (_context.Language?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> IndexAdmin()
        {   
            ViewBag.Layout = _Layout;
            return _context.Language != null ?
                        View(await _context.Language.ToListAsync()) :
                        Problem("Entity set 'LumosTutorIdentityDbContext.Language'  is null.");
        }
        public async Task<IActionResult> EditAdmin(int? id)
        {
            ViewBag.Layout = _Layout;
            if (id == null || _context.Language == null)
            {
                return NotFound();
            }

            var language = await _context.Language .FindAsync(id);
            if (language == null)
            {
                return NotFound();
            }
            return View(language);
        }

        // POST: Language/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdmin(int id, [Bind("Id,Name,Status")] Language  language)
        {   
            ViewBag.Layout = _Layout;
            if (id != language.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(language);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanguageExists(language.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexAdmin));
            }
            return View(language);
        }
    }
}
