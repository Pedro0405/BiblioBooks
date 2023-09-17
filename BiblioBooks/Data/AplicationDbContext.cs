using BiblioBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace BiblioBooks.Data
{
    public class AplicationDbContext : DbContext
    {
        public DbSet<EmprestimosModel> Emprestimos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("Data Source = Emprestimos.db");
    }
}
