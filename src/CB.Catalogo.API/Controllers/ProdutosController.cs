using AutoMapper;
using CB.Catalogo.API.Models;
using CB.Catalogo.Domain.Entities;
using CB.Catalogo.Domain.Repositories;
using CB.Core.Api;
using CB.Core.Identidade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CB.Catalogo.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProdutosController : MainController
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IMapper mapper;

        public ProdutosController(IProdutoRepository produtoRepository, IMapper mapper)
        {
            this.produtoRepository = produtoRepository;
            this.mapper = mapper;
        }

        [HttpPost("adicionar-novo-produto")]
        [ClaimsAuthorize("Produto", "Adicionar")]
        public async Task<IActionResult> Salvar([FromBody] AdicionarProdutoViewModel produtovm)
        {
            if (!ModelState.IsValid) return Response(ModelState);

            var produto = new Produto(produtovm.Nome, produtovm.Descricao, produtovm.Valor);

            produtoRepository.Adicionar(produto);

            var sucesso = await produtoRepository.UnitOfWork.Commit();

            if (!sucesso)
                AdicionarErro("Ocorreu um erro ao salvar");

            return Response();
        }

        [HttpPut("editar-produto")]
        [ClaimsAuthorize("Produto", "Editar")]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarProdutoViewModel produtovm)
        {
            if (!ModelState.IsValid) return Response(ModelState);

            var produto = await produtoRepository.ObterPorId(produtovm.Id);

            produto.Atualizar(produtovm.Nome, produtovm.Descricao, produtovm.Valor);

            produtoRepository.Atualizar(produto);

            var sucesso = await produtoRepository.UnitOfWork.Commit();

            if (!sucesso)
                AdicionarErro("Ocorreu um erro ao atualizar");

            return Response();
        }

        [HttpDelete("remover-produto/{id:guid}")]
        [ClaimsAuthorize("Produto", "Excluir")]
        public async Task<IActionResult> Remover(Guid id)
        {
            if (Guid.Empty == id) return Response("Id Inválido");

            var produto = await produtoRepository.ObterPorId(id);

            produtoRepository.Deletar(produto);

            var sucesso = await produtoRepository.UnitOfWork.Commit();

            if (!sucesso)
                AdicionarErro("Ocorreu um erro ao deletar");

            return Response();
        }

        [HttpPost("adicionar-estoque")]
        [ClaimsAuthorize("Produto", "Movimentar")]
        public async Task<IActionResult> AdicionarEstoque([FromBody] MovimentarEstoqueViewModel moviEstoVm)
        {
            if (!ModelState.IsValid) return Response(ModelState);

            var produto = await produtoRepository.ObterPorId(moviEstoVm.IdProduto);

            produto.EntradaEstoque(moviEstoVm.Data, moviEstoVm.Quantidade);

            produtoRepository.Atualizar(produto);

            var sucesso = await produtoRepository.UnitOfWork.Commit();

            if (!sucesso)
                AdicionarErro("Ocorreu um erro ao salvar");

            return Response();
        }

        [HttpPost("remover-estoque")]
        [ClaimsAuthorize("Produto", "Movimentar")]
        public async Task<IActionResult> RemoverEstoque([FromBody] MovimentarEstoqueViewModel moviEstoVm)
        {
            if (!ModelState.IsValid) return Response(ModelState);

            var produto = await produtoRepository.ObterPorId(moviEstoVm.IdProduto);

            produto.SaidaEstoque(moviEstoVm.Data, moviEstoVm.Quantidade);

            produtoRepository.Atualizar(produto);

            var sucesso = await produtoRepository.UnitOfWork.Commit();

            if (!sucesso)
                AdicionarErro("Ocorreu um erro ao Remover do Estoque");

            return Response();
        }

        [HttpGet("listar-produtos")]       
        public async Task<IActionResult> Listar()
        {
            var lstProd = mapper.Map<IEnumerable<ListarProdutoViewModel>>(await produtoRepository.Listar());
            return Response(lstProd);
        }

        [HttpGet("obter-produto/{id:guid}")]
        public async Task<IActionResult> Obter(Guid id)
        {
            var prod = mapper.Map<ListarProdutoViewModel>(await produtoRepository.ObterPorId(id));
            return Response(prod);
        }


    }
}
