using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioBooks.Migrations
{
    /// <inheritdoc />
    public partial class imagemparaoslivros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagemLivro",
                table: "Emprestimos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemLivro",
                table: "Emprestimos");
        }
    }
}
