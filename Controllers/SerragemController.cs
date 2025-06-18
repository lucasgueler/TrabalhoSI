using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaMarmoreGranito.Data;
using SistemaMarmoreGranito.Models;

namespace SistemaMarmoreGranito.Controllers
{
    [Authorize]
    public class SerragemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SerragemController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["BlocoId"] = new SelectList(_context.Blocos.Where(b => b.Ativo), "Id", "Codigo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessarSerragem(int blocoId, decimal espessura, int quantidade)
        {
            var bloco = await _context.Blocos.FindAsync(blocoId);
            if (bloco == null)
            {
                return NotFound();
            }

            // Calcula o valor por chapa baseado no valor do bloco e quantidade de chapas
            var valorPorChapa = bloco.ValorCompra / quantidade;

            // Cria as chapas
            for (int i = 0; i < quantidade; i++)
            {
                var chapa = new Chapa
                {
                    BlocoId = blocoId,
                    TipoMaterial = bloco.TipoMaterial,
                    Largura = bloco.MetragemM3 / espessura, // Simplificação do cálculo
                    Altura = bloco.MetragemM3 / espessura, // Simplificação do cálculo
                    Espessura = espessura,
                    Valor = valorPorChapa
                };

                _context.Chapas.Add(chapa);
            }

            // Marca o bloco como inativo
            bloco.Ativo = false;
            _context.Update(bloco);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Chapas");
        }
    }
} 