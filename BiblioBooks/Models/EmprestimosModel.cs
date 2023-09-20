using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BiblioBooks.Models
{
    public class EmprestimosModel 
    {
        public int id { get; set; }
        [Required(ErrorMessage = "O nome do recebor é necessario ")]
        [Display(Name = "Recebedor")]
        public string Recebedor { get; set; }
        

        [Required(ErrorMessage = "O nome do fornecedor é necessario ")]
        [Display(Name = "Fornecedor")]
        public string Fornecedor { get; set; }


        [Required(ErrorMessage = "O nome do livro Emprestado é necessario ")]
        [Display(Name = "Nome do livro")]
        public string LivroEmprestado { get; set; }
        public DateTime DataultimaAtualizacao { get; set; } = DateTime.Now;

    }
}
