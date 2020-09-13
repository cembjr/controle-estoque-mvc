using CB.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CB.Catalogo.Domain.Entities
{
    public class MovimentacaoEstoque : Entity
    {
        public Guid IdProduto { get; private set; }
        public DateTime DataMovimento { get; private set; }
        public decimal Quantidade { get; private set; }
        public ETipoMovimento TipoMovimento { get; private set; }

        public virtual Produto Produto { get; private set; }

        public MovimentacaoEstoque(Guid idProduto, DateTime dataMovimento, decimal quantidade, ETipoMovimento tipoMovimento)
        {
            IdProduto = idProduto;
            DataMovimento = dataMovimento;
            Quantidade = quantidade;
            TipoMovimento = tipoMovimento;
        }
    }

    public enum ETipoMovimento
    {
        Entrada = 1,
        Saida = 2
    }
}
