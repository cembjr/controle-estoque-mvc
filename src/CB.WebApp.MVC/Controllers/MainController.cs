using CB.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CB.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool HasResponseErros(IResponseErros response)
        {
            var erros = response.ErrosResponse?.Erros ?? new ErrosResponse().Erros;
            if (erros == null || (erros != null && erros.Count == 0)) return false;

            foreach (var mensagem in erros)
                ModelState.AddModelError(string.Empty, mensagem);
            
            return true;
        }
    }
}
