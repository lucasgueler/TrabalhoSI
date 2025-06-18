using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaMarmoreGranito.Data;
using SistemaMarmoreGranito.Models;

namespace SistemaMarmoreGranito.Controllers
{
    [Authorize]
    public class ChapasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChapasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var chapas = await _context.Chapas
                .Include(c => c.Bloco)
                .Where(c => c.Ativo)
                .ToListAsync();
            return View(chapas);
        }

        public IActionResult Create()
        {
            ViewData["BlocoId"] = new SelectList(_context.Blocos.Where(b => b.Ativo), "Id", "Codigo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Chapa chapa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chapa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlocoId"] = new SelectList(_context.Blocos.Where(b => b.Ativo), "Id", "Codigo", chapa.BlocoId);
            return View(chapa);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chapa = await _context.Chapas.FindAsync(id);
            if (chapa == null)
            {
                return NotFound();
            }
            ViewData["BlocoId"] = new SelectList(_context.Blocos.Where(b => b.Ativo), "Id", "Codigo", chapa.BlocoId);
            return View(chapa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Chapa chapa)
        {
            if (id != chapa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chapa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChapaExists(chapa.Id))
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
            ViewData["BlocoId"] = new SelectList(_context.Blocos.Where(b => b.Ativo), "Id", "Codigo", chapa.BlocoId);
            return View(chapa);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chapa = await _context.Chapas
                .Include(c => c.Bloco)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chapa == null)
            {
                return NotFound();
            }

            return View(chapa);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chapa = await _context.Chapas.FindAsync(id);
            if (chapa != null)
            {
                chapa.Ativo = false;
                _context.Update(chapa);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ChapaExists(int id)
        {
            return _context.Chapas.Any(e => e.Id == id);
        }
    }
} 