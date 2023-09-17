﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BiblioBooks.Data;
using BiblioBooks.Models;

namespace BiblioBooks.Controllers
{
    public class EmprestimosModelController : Controller
    {
        private readonly AplicationDbContext _context;

        public EmprestimosModelController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmprestimosModel
        public async Task<IActionResult> Index()
        {
              return _context.Emprestimos != null ? 
                          View(await _context.Emprestimos.ToListAsync()) :
                          Problem("Entity set 'AplicationDbContext.Emprestimos'  is null.");
        }

        // GET: EmprestimosModel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Emprestimos == null)
            {
                return NotFound();
            }

            var emprestimosModel = await _context.Emprestimos
                .FirstOrDefaultAsync(m => m.id == id);
            if (emprestimosModel == null)
            {
                return NotFound();
            }

            return View(emprestimosModel);
        }

        // GET: EmprestimosModel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmprestimosModel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Recebedor,Fornecedor,LivroEmprestado,DataultimaAtualizacao")] EmprestimosModel emprestimosModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emprestimosModel);
                await _context.SaveChangesAsync();
                TempData["MenssagemSucesso"] = "Cadastro realizado com sucesso";
                return RedirectToAction(nameof(Index));
            }
            TempData["MenssagemErro"] = "Erro ao cadastrar emprestimo";
            return View(emprestimosModel);
        }

        // GET: EmprestimosModel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Emprestimos == null)
            {
                return NotFound();
            }

            var emprestimosModel = await _context.Emprestimos.FindAsync(id);
            if (emprestimosModel == null)
            {
                return NotFound();
            }
            return View(emprestimosModel);
        }

        // POST: EmprestimosModel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Recebedor,Fornecedor,LivroEmprestado,DataultimaAtualizacao")] EmprestimosModel emprestimosModel)
        {
            if (id != emprestimosModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emprestimosModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmprestimosModelExists(emprestimosModel.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["MenssagemSucesso"] = "Edição realizada com sucesso";
                return RedirectToAction(nameof(Index));
            }
            TempData["MenssagemErro"] = "Erro ao editar emprestimo";
            return View(emprestimosModel);
        }

        // GET: EmprestimosModel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Emprestimos == null)
            {
                return NotFound();
            }

            var emprestimosModel = await _context.Emprestimos
                .FirstOrDefaultAsync(m => m.id == id);
            if (emprestimosModel == null)
            {
                return NotFound();
            }

            return View(emprestimosModel);
        }

        // POST: EmprestimosModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Emprestimos == null)
            {
                return Problem("Entity set 'AplicationDbContext.Emprestimos'  is null.");
            }
            var emprestimosModel = await _context.Emprestimos.FindAsync(id);
            if (emprestimosModel != null)
            {
                _context.Emprestimos.Remove(emprestimosModel);
            }
            
            await _context.SaveChangesAsync();
            TempData["MenssagemSucesso"] = "Exclusão realizada com sucesso";
            return RedirectToAction(nameof(Index));
        }

        private bool EmprestimosModelExists(int id)
        {
          return (_context.Emprestimos?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}