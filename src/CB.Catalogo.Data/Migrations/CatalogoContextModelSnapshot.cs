﻿// <auto-generated />
using System;
using CB.Catalogo.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CB.Catalogo.Data.Migrations
{
    [DbContext(typeof(CatalogoContext))]
    partial class CatalogoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CB.Catalogo.Domain.Entities.MovimentacaoEstoque", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IdMoviEsto")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataMovimento")
                        .HasColumnName("DataMovi")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("IdProduto")
                        .HasColumnName("IdProd")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Quantidade")
                        .HasColumnName("QuanMovi")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("TipoMovimento")
                        .HasColumnName("TipoMovi")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdProduto");

                    b.ToTable("CBMoviEsto");
                });

            modelBuilder.Entity("CB.Catalogo.Domain.Entities.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IdProd")
                        .HasColumnType("char(36)");

                    b.Property<bool>("Ativo")
                        .HasColumnName("IsAtiv")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnName("DataCada")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("DescProd")
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("NomeProd")
                        .HasColumnType("varchar(200)");

                    b.Property<decimal>("Valor")
                        .HasColumnName("ValoProd")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("CBProd");
                });

            modelBuilder.Entity("CB.Catalogo.Domain.Entities.MovimentacaoEstoque", b =>
                {
                    b.HasOne("CB.Catalogo.Domain.Entities.Produto", "Produto")
                        .WithMany("Movimentacoes")
                        .HasForeignKey("IdProduto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
