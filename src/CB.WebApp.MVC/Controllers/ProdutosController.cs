using CB.WebApp.MVC.Models;
using CB.WebApp.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace CB.WebApp.MVC.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        private readonly IProdutoService produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            this.produtoService = produtoService;
        }

        public async Task<IActionResult> Index() => View(await produtoService.ListarAsync());

        public async Task<IActionResult> Details(Guid id)
        {
            var produtoViewModel = await produtoService.Obter(id);
            if (produtoViewModel == null)
                return NotFound();

            return View(produtoViewModel);
        }

        // GET: Produtos/Create
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel produto)
        {
            if (!ModelState.IsValid) return View(produto);

            var produtoAdicionar = new AdicionarProdutoViewModel()
            {
                Descricao = produto.Descricao,
                Nome = produto.Nome,
                Valor = produto.Valor
            };

            var result = await produtoService.Adicionar(produtoAdicionar);

            if (result)
                return RedirectToAction(nameof(Index));
            else
                return View(produto);

        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var produtoViewModel = await produtoService.Obter(id);
            if (produtoViewModel == null)
                return NotFound();

            return View(produtoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id)
                return NotFound();

            if (!ModelState.IsValid) return View(produtoViewModel);

            var ret = await produtoService.Atualizar(produtoViewModel);

            if (ret)
                return RedirectToAction(nameof(Index));
            else
                return View(produtoViewModel);

        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var produtoViewModel = await produtoService.Obter(id);

            if (produtoViewModel == null)
                return NotFound();

            return View(produtoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produtoViewModel = await produtoService.Obter(id);
            if (produtoViewModel == null) return BadRequest();

            var result = await produtoService.Deletar(id);

            if (result)
                return RedirectToAction(nameof(Index));
            else return BadRequest();
        }

    }
}
