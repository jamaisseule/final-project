using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LUMOSBook.Models;
using LUMOSBook.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace LUMOSBook.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly LUMOSBookIdentityDbContext _context;

        public CategoriesController(LUMOSBookIdentityDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        [Authorize(Roles = "StoreOwner, Admin")]
        public async Task<IActionResult> Index()
        {
              return _context.Category != null ? 
                          View(await _context.Category.ToListAsync()) :
                          Problem("Entity set 'LUMOSBookContext.Category'  is null.");
        }

        [Authorize(Roles = "StoreOwner, Admin")]
        public async Task<IActionResult> RequestCategory()
        {
              return View(await _context.Category.ToListAsync());
        }

        // GET: Categories/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        [Authorize(Roles = "StoreOwner, Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "StoreOwner, Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Status")] Category category, string Name)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                if(_context.Category.Where(a => a.Name == Name).ToList().Count != 0){
                     return View(category);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(RequestCategory));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Category == null)
            {
                return Problem("Entity set 'LUMOSBookContext.Category'  is null.");
            }
            var category = await _context.Category.FindAsync(id);
            if (category != null)
            {
                _context.Category.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return (_context.Category?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

















// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;
// using LUMOSBook.Models;
// using LUMOSBook.Areas.Identity.Data;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Authorization;

// namespace LUMOSBook.Controllers
// {
//     [Authorize(Roles = "StoreOwner, Admin")]
//     public class CategoriesController : Controller
//     {
//         private readonly LUMOSBookIdentityDbContext _context;

//         public CategoriesController(LUMOSBookIdentityDbContext context)
//         {
//             _context = context;
//         }

//         // GET: Categories
//         public async Task<IActionResult> Index()
//         {
//               return _context.Category != null ? 
//                           View(await _context.Category.ToListAsync()) :
//                           Problem("Entity set 'LUMOSBookContext.Category'  is null.");
//         }

        
//         // GET: Categories/Details/5
//         public async Task<IActionResult> Details(int? id)
//         {
//             if (id == null || _context.Category == null)
//             {
//                 return NotFound();
//             }

//             var category = await _context.Category
//                 .FirstOrDefaultAsync(m => m.Id == id);
//             if (category == null)
//             {
//                 return NotFound();
//             }

//             return View(category);
//         }

//         // GET: Categories/Create
//         public IActionResult Create()
//         {
//             return View();
//         }

//         // POST: Categories/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create([Bind("Id,Name")] Category category, string Name)
//         {
//             if (ModelState.IsValid)
//             {
//                 _context.Add(category);
//                 if(_context.Category.Where(a => a.Name == Name).ToList().Count != 0){
//                      return View(category);
//                 }
//                 await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(category);
//         }

//         // GET: Categories/Edit/5
//         [Authorize(Roles = "Admin")]
//         public async Task<IActionResult> Edit(int? id)
//         {
//             if (id == null || _context.Category == null)
//             {
//                 return NotFound();
//             }

//             var category = await _context.Category.FindAsync(id);
//             if (category == null)
//             {
//                 return NotFound();
//             }
//             return View(category);
//         }

//         // POST: Categories/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
//         {
//             if (id != category.Id)
//             {
//                 return NotFound();
//             }

//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _context.Update(category);
//                     await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!CategoryExists(category.Id))
//                     {
//                         return NotFound();
//                     }
//                     else
//                     {
//                         throw;
//                     }
//                 }
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(category);
//         }

//         // GET: Categories/Delete/5
//         public async Task<IActionResult> Delete(int? id)
//         {
//             if (id == null || _context.Category == null)
//             {
//                 return NotFound();
//             }

//             var category = await _context.Category
//                 .FirstOrDefaultAsync(m => m.Id == id);
//             if (category == null)
//             {
//                 return NotFound();
//             }

//             return View(category);
//         }

//         // POST: Categories/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(int id)
//         {
//             if (_context.Category == null)
//             {
//                 return Problem("Entity set 'LUMOSBookContext.Category'  is null.");
//             }
//             var category = await _context.Category.FindAsync(id);
//             if (category != null)
//             {
//                 _context.Category.Remove(category);
//             }
            
//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }

//         private bool CategoryExists(int id)
//         {
//           return (_context.Category?.Any(e => e.Id == id)).GetValueOrDefault();
//         }
//     }
// }
