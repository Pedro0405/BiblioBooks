using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BiblioBooks.Data;
using BiblioBooks.Models;
using System.Security.Cryptography.X509Certificates;
using ClosedXML.Excel;
using System.Data;

namespace BiblioBooks.Controllers
{
    public class EmprestimosModelController : Controller
    {
        private readonly AplicationDbContext _context;
        private string caminhoServidor;
        public EmprestimosModelController(AplicationDbContext context, IWebHostEnvironment sistmea)
        {
            _context = context;
            caminhoServidor = sistmea.WebRootPath;
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
        public async Task<IActionResult> Create([Bind("id,ImagemLivro,Recebedor,Fornecedor,LivroEmprestado,DataEmprestimo, DataDevolucao, DiasParaDevolver")] EmprestimosModel emprestimosModel, IFormFile ImagemLivro)
        {
            if (ModelState.IsValid)
            {

                string caminhoParaSalvar = caminhoServidor + "\\Images\\";
                string novoNomeParaImagen = Guid.NewGuid().ToString() + "_" + ImagemLivro.FileName;
                if (!Directory.Exists(caminhoParaSalvar))
                {
                    Directory.CreateDirectory(caminhoParaSalvar);
                }
                using (var stream = System.IO.File.Create(caminhoParaSalvar + novoNomeParaImagen))
                {
                    ImagemLivro.CopyToAsync(stream);
                    emprestimosModel.ImagemLivro = novoNomeParaImagen;
                }
                emprestimosModel.DataEmprestimo = DateTime.Now.ToLocalTime();
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
        public async Task<IActionResult> Edit(int id, [Bind("id,Recebedor, ImagemLivro, Fornecedor,LivroEmprestado,DataEmprestimo,DataDevolucao,DiasParaDevolver")] EmprestimosModel emprestimosModel)
        {
            if (id != emprestimosModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                EmprestimosModel ORIGINALEMPRESTIMO = _context.Emprestimos.AsNoTracking().FirstOrDefault(x => x.id == emprestimosModel.id);
                
                try
                {
                    emprestimosModel.ImagemLivro = ORIGINALEMPRESTIMO.ImagemLivro;
                    emprestimosModel.DataEmprestimo = ORIGINALEMPRESTIMO.DataEmprestimo;
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
        public IActionResult Exportar()
        {
            var dados = GetDados();

            using (XLWorkbook workBook = new XLWorkbook())
            {
                workBook.AddWorksheet(dados, "Dados Empréstimos");
                using (MemoryStream ms = new MemoryStream())
                {
                    workBook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Emprestimo.xls");
                }


            } 
        }

        private DataTable GetDados()
        {
            DataTable dataTable = new DataTable();

            dataTable.TableName = "Dados emprestimos";


            dataTable.Columns.Add("Recebedor", typeof(string));
            dataTable.Columns.Add("Fornecedor", typeof(string));
            dataTable.Columns.Add("Livro", typeof(string));
            dataTable.Columns.Add("Data Emprestimos", typeof(DateTime));
            dataTable.Columns.Add("Data para devolução", typeof(DateTime));
            dataTable.Columns.Add("Dias para devolução", typeof(int));

            var dados = _context.Emprestimos.ToList();

            if (dados.Count > 0)

            {
                dados.ForEach(emprestimo =>
                {
                    dataTable.Rows.Add(emprestimo.Recebedor, emprestimo.Fornecedor, emprestimo.LivroEmprestado, emprestimo.DataEmprestimo, emprestimo.DataDevolucao, emprestimo.DiasParaDevolver);
                });
            }

            return dataTable;


        }
    } 
}
