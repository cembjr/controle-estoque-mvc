using CB.WebApp.MVC.Extensions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CB.WebApp.MVC.Models
{
    public class AdicionarProdutoViewModel
    {
        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [Moeda]
        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        public decimal Valor { get; set; }
    }

    public class MovimentarEstoqueViewModel
    {
        [Required(ErrorMessage = "Campo {0} deve ser informado")]
        public Guid IdProduto { get; set; }

        [Required(ErrorMessage = "Campo {0} deve ser informado")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Campo {0} deve ser informado")]
        [Range(1, 9999999, ErrorMessage = "{0} deve estar entre {1} e {2}")]
        public decimal Quantidade { get; set; }
    }

    public class ProdutoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        
        [Moeda]
        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        public decimal Valor { get; set; }
        
        [DisplayName("Em Estoque")]
        public decimal QuantidadeEmEstoque { get; set; }
    }
}
