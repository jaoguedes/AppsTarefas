﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppTarefas.Data;
using AppTarefas.Models;

namespace AppTarefas.Controllers
{
    public class TarefasController : Controller
    {
        private readonly AppTarefasDbContext _context;

        public TarefasController(AppTarefasDbContext context)
        {
            _context = context;
        }

        // GET: Tarefas

        public IActionResult Index()
        {
            var categorias = _context.Categorias.ToList();
            ViewBag.Categorias = categorias;

            var tarefas = _context.Tarefas
                .Include(t => t.Categoria)
                .Include(t => t.Status)
                .ToList();

            return View(tarefas);

        }

        [HttpGet]
        public IActionResult FiltrarPorCategoriaCor(string categoriaCor)
        {
            var categorias = _context.Categorias.ToList();
            ViewBag.Categorias = categorias;

            List<Tarefa> tarefasFiltradas = _context.Tarefas
                .Include(t => t.Categoria)
                .Include(t => t.Status)
                .Where(t => string.IsNullOrEmpty(categoriaCor) || t.Categoria.CategoriaCor == categoriaCor)
                .ToList();

            return PartialView("_TarefasTable", tarefasFiltradas);

        }





        // GET: Tarefas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Tarefas == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas
                .Include(t => t.Categoria)
                .Include(t => t.Status)
                .FirstOrDefaultAsync(m => m.TarefaId == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // GET: Tarefas/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome");
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusNome");
            return View();
        }

        // POST: Tarefas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TarefaId,TarefaNome,DataInicio,DataFim,CategoriaId,StatusId")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                tarefa.TarefaId = Guid.NewGuid();
                tarefa.StatusId = _context.Status.FirstOrDefault(t => t.StatusNome == "A Fazer").StatusId;
                _context.Add(tarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", tarefa.CategoriaId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusNome", tarefa.StatusId);
            return View(tarefa);
        }

        // GET: Tarefas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Tarefas == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", tarefa.CategoriaId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusNome", tarefa.StatusId);
            return View(tarefa);
        }

        // POST: Tarefas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TarefaId,TarefaNome,DataInicio,DataFim,CategoriaId,StatusId")] Tarefa tarefa)
        {
            if (id != tarefa.TarefaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarefa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarefaExists(tarefa.TarefaId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", tarefa.CategoriaId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusNome", tarefa.StatusId);
            return View(tarefa);
        }



        // GET: Tarefas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Tarefas == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas
                .Include(t => t.Categoria)
                .Include(t => t.Status)
                .FirstOrDefaultAsync(m => m.TarefaId == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // POST: Tarefas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Tarefas == null)
            {
                return Problem("Entity set 'AppTarefasDbContext.Tarefas'  is null.");
            }
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarefaExists(Guid id)
        {
          return (_context.Tarefas?.Any(e => e.TarefaId == id)).GetValueOrDefault();
        }
    }
}
