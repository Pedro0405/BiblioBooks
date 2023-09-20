﻿// <auto-generated />
using System;
using BiblioBooks.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BiblioBooks.Migrations
{
    [DbContext(typeof(AplicationDbContext))]
    [Migration("20230920143110_Colocando contagem de dias")]
    partial class Colocandocontagemdedias
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("BiblioBooks.Models.EmprestimosModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataDevolucao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataEmprestimo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Fornecedor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LivroEmprestado")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Recebedor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Emprestimos");
                });
#pragma warning restore 612, 618
        }
    }
}
