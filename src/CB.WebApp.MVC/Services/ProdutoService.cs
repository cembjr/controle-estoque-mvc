using CB.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CB.WebApp.MVC.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        public ProdutoService(HttpClient httpClient) : base(httpClient, new Uri("https://localhost:44305/api/produtos/")) { }

        public async Task<IList<ProdutoViewModel>> ListarAsync()
        {
            var produtos = await GetListAsync<ProdutoViewModel>("listar-produtos");
            return produtos.Result;
        }

        public async Task<bool> Adicionar(AdicionarProdutoViewModel produto)
        {
            var result = await Post(produto, "adicionar-novo-produto");
            return result;
        }

        public async Task<bool> Atualizar(ProdutoViewModel produto)
        {
            var result = await Put(produto, "editar-produto");
            return result;
        }

        public async Task<bool> Deletar(Guid id)
        {
            var result = await Delete($"remover-produto/{id}");
            return result;
        }

        public async Task<ProdutoViewModel> Obter(Guid id)
        {
            var result = await GetAsync<ProdutoViewModel>($"obter-produto/{id}");
            return result.Result;
        }
    }
}
