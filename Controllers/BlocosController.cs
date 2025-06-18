using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMarmoreGranito.Data;
using SistemaMarmoreGranito.Models;

namespace SistemaMarmoreGranito.Controllers
{
    [Authorize]
    public class BlocosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlocosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Blocos.Where(b => b.Ativo).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bloco bloco)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bloco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bloco);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloco = await _context.Blocos.FindAsync(id);
            if (bloco == null)
            {
                return NotFound();
            }
            return View(bloco);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bloco bloco)
        {
            if (id != bloco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bloco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlocoExists(bloco.Id))
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
            return View(bloco);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloco = await _context.Blocos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bloco == null)
            {
                return NotFound();
            }

            return View(bloco);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bloco = await _context.Blocos.FindAsync(id);
            if (bloco != null)
            {
                bloco.Ativo = false;
                _context.Update(bloco);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BlocoExists(int id)
        {
            return _context.Blocos.Any(e => e.Id == id);
        }
    }
} 