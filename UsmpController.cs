using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using USMP.Models;

namespace USMP
{
    public class UsmpController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsmpController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Usmp
        public async Task<IActionResult> Index()
        {
              return _context.Usmp != null ? 
                          View(await _context.Usmp.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Usmp'  is null.");
        }

        // GET: Usmp/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Usmp == null)
            {
                return NotFound();
            }

            var usmp = await _context.Usmp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usmp == null)
            {
                return NotFound();
            }

            return View(usmp);
        }

        // GET: Usmp/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usmp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Telefono,Direccion,Observacion")] Usmp usmp)
        {
            if (ModelState.IsValid)
            {
                usmp.Id = Guid.NewGuid();
                _context.Add(usmp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usmp);
        }

        // GET: Usmp/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Usmp == null)
            {
                return NotFound();
            }

            var usmp = await _context.Usmp.FindAsync(id);
            if (usmp == null)
            {
                return NotFound();
            }
            return View(usmp);
        }

        // POST: Usmp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombre,Telefono,Direccion,Observacion")] Usmp usmp)
        {
            if (id != usmp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usmp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsmpExists(usmp.Id))
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
            return View(usmp);
        }

        // GET: Usmp/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Usmp == null)
            {
                return NotFound();
            }

            var usmp = await _context.Usmp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usmp == null)
            {
                return NotFound();
            }

            return View(usmp);
        }

        // POST: Usmp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Usmp == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Usmp'  is null.");
            }
            var usmp = await _context.Usmp.FindAsync(id);
            if (usmp != null)
            {
                _context.Usmp.Remove(usmp);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsmpExists(Guid id)
        {
          return (_context.Usmp?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
