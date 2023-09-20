﻿// <auto-generated />
using System;
using BiblioBooks.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BiblioBooks.Migrations
{
    [DbContext(typeof(AplicationDbContext))]
    partial class AplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("ImagemLivro")
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
