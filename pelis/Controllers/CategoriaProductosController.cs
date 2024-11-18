using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pelis.Data;
using pelis.Models;

namespace pelis.Controllers
{
    [Authorize(Roles = "Administrador,Supervisor")]
    public class CategoriaProductosController : Controller
    {
        private readonly pelisContext _context;

        public CategoriaProductosController(pelisContext context)
        {
            _context = context;
        }

        // GET: CategoriaProductos
        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoriaProductos.ToListAsync());
        }

        // GET: CategoriaProductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaProductos = await _context.CategoriaProductos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaProductos == null)
            {
                return NotFound();
            }

            return View(categoriaProductos);
        }

        // GET: CategoriaProductos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriaProductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Descrip,Estado")] CategoriaProductos categoriaProductos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriaProductos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaProductos);
        }

        // GET: CategoriaProductos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaProductos = await _context.CategoriaProductos.FindAsync(id);
            if (categoriaProductos == null)
            {
                return NotFound();
            }
            return View(categoriaProductos);
        }

        // POST: CategoriaProductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Descrip,Estado")] CategoriaProductos categoriaProductos)
        {
            if (id != categoriaProductos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaProductos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaProductosExists(categoriaProductos.Id))
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
            return View(categoriaProductos);
        }

        // GET: CategoriaProductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaProductos = await _context.CategoriaProductos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaProductos == null)
            {
                return NotFound();
            }

            return View(categoriaProductos);
        }

        // POST: CategoriaProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriaProductos = await _context.CategoriaProductos.FindAsync(id);
            if (categoriaProductos != null)
            {
                _context.CategoriaProductos.Remove(categoriaProductos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaProductosExists(int id)
        {
            return _context.CategoriaProductos.Any(e => e.Id == id);
        }
    }
}
