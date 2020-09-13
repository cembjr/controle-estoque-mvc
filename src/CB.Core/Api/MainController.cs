using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;

namespace CB.Core.Api
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private ICollection<string> _erros;

        protected MainController() => _erros = _erros ?? new List<string>();

        protected void AdicionarErro(string mensagem) => _erros.Add(mensagem);

        protected bool HasErros() => _erros.Any();

        protected IActionResult CustomResponse(ModelStateDictionary modelState)
        {
            modelState.Values.SelectMany(e => e.Errors).ToList().ForEach(erro => AdicionarErro(erro.ErrorMessage));
            return Response();
        }

        protected new IActionResult Response(object objeto = null)
        {
            if (HasErros())
                return BadRequest(new
                {
                    Erros = _erros.ToArray()
                });

            return Ok(objeto);
        }
    }
}
