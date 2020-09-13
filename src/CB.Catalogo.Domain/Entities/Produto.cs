using CB.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CB.Catalogo.Domain.Entities
{
    public class Produto : Entity, IAggregateRoot
    {

        public Produto(string nome, string descricao, decimal valor)
        {
            Nome = nome;
            Descricao = descricao;
            Ativo = true;
            Valor = valor;
            DataCadastro = DateTime.Now;
            _Movimentacoes = _Movimentacoes ?? new List<MovimentacaoEstoque>();
        }

        protected Produto()
        {
            _Movimentacoes = _Movimentacoes ?? new List<MovimentacaoEstoque>();
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public decimal QuantidadeEmEstoque => _Movimentacoes.Sum(s => s.Quantidade * (s.TipoMovimento == ETipoMovimento.Entrada ? 1 : -1));

        protected virtual ICollection<MovimentacaoEstoque> _Movimentacoes { get; private set; }
        public IReadOnlyCollection<MovimentacaoEstoque> Movimentacoes => (IReadOnlyCollection<MovimentacaoEstoque>)_Movimentacoes;

        public void EntradaEstoque(DateTime dataMovimento, decimal quantidade)
            => _Movimentacoes.Add(new MovimentacaoEstoque(this.Id, dataMovimento, quantidade, ETipoMovimento.Entrada));

        public void SaidaEstoque(DateTime dataMovimento, decimal quantidade)
            => _Movimentacoes.Add(new MovimentacaoEstoque(this.Id, dataMovimento, quantidade, ETipoMovimento.Saida));

        public void Atualizar(string nome, string descricao, decimal valor)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
        }

        public void Desativar() => Ativo = false;
    }
}
