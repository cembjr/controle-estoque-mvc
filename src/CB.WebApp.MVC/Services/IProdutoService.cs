using CB.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CB.WebApp.MVC.Services
{
    public interface IProdutoService
    {
        Task<IList<ProdutoViewModel>> ListarAsync();
        Task<ProdutoViewModel> Obter(Guid id);
        Task<bool> Adicionar(AdicionarProdutoViewModel produto);
        Task<bool> Atualizar(ProdutoViewModel produto);
        Task<bool> Deletar(Guid id);
    }
}
