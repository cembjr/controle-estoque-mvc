using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CB.Catalogo.API.Models
{
    public class AdicionarProdutoViewModel
    {
        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        public string Descricao { get; set; }

        [Range(1, 999999, ErrorMessage = "{0} deve estar entre {1} e {2}")]
        public decimal Valor { get; set; }
    }
    public class AtualizarProdutoViewModel : AdicionarProdutoViewModel
    {
        public Guid Id { get; set; }
    }

    public class MovimentarEstoqueViewModel
    {
        [Required(ErrorMessage = "Campo {0} deve ser informado")]
        public Guid IdProduto { get; set; }

        [Required(ErrorMessage = "Campo {0} deve ser informado")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Campo {0} deve ser informado")]
        [Range(1, 999999, ErrorMessage = "{0} deve estar entre {1} e {2}")]
        public decimal Quantidade { get; set; }
    }

    public class ListarProdutoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public decimal Valor { get; set; }
        public decimal QuantidadeEmEstoque { get; set; }
    }
}
