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

        [DataType(DataType.Date)]
        [Display(Name = "Data do emprestimo")]

        public DateTime DataEmprestimo { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data para devolução")]
        public DateTime DataDevolucao { get; set; }

        [Display(Name = "Dias para devolução")]

        public int DiasParaDevolver
        {
            get
            {
                // Subtrai DataEmprestimo de DataDevolucao e extrai o número de dias.
                TimeSpan diff = DataDevolucao - DataEmprestimo;
                return diff.Days;
            }
        }

    }
}
