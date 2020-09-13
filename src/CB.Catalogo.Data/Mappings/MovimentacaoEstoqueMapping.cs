using CB.Catalogo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CB.Catalogo.Data.Mappings
{
    public class MovimentacaoEstoqueMapping : IEntityTypeConfiguration<MovimentacaoEstoque>
    {
        public void Configure(EntityTypeBuilder<MovimentacaoEstoque> builder)
        {
            builder.ToTable("CBMoviEsto");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .HasColumnName("IdMoviEsto")
                   .IsRequired();

            builder.Property(x => x.IdProduto)
                   .IsRequired()
                   .HasColumnName("IdProd");

            builder.Property(x => x.DataMovimento)
                   .IsRequired()
                   .HasColumnName("DataMovi");

            builder.Property(x => x.TipoMovimento)
                   .IsRequired()
                   .HasColumnName("TipoMovi");

            builder.Property(x => x.Quantidade)
                   .IsRequired()
                   .HasColumnName("QuanMovi");

            builder.HasOne(x => x.Produto)
                   .WithMany(x => x.Movimentacoes)
                   .HasForeignKey(x => x.IdProduto)
                   .HasPrincipalKey(x => x.Id);
        }
    }

}
