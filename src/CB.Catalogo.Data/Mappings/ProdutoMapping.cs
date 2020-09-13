using CB.Catalogo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CB.Catalogo.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("CBProd");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .HasColumnName("IdProd")
                   .IsRequired();

            builder.Property(x => x.Nome)
                   .IsRequired()
                   .HasColumnName("NomeProd")
                   .HasColumnType("varchar(200)");

            builder.Property(x => x.Descricao)
                   .IsRequired()
                   .HasColumnName("DescProd")
                   .HasColumnType("varchar(300)");

            builder.Property(x => x.Ativo)
                   .IsRequired()
                   .HasColumnName("IsAtiv");

            builder.Property(x => x.Valor)
                   .IsRequired()
                   .HasColumnName("ValoProd");

            builder.Property(x => x.DataCadastro)
                   .IsRequired()
                   .HasColumnName("DataCada");

            builder.HasMany(x => x.Movimentacoes)
                   .WithOne(x => x.Produto)
                   .HasForeignKey(x => x.IdProduto)
                   .HasPrincipalKey(x => x.Id);
        }
    }
}
