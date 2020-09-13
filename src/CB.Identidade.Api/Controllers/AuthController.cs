using CB.Core.Api;
using CB.Core.Identidade;
using CB.Identidade.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static CB.Identidade.Api.Models.UsuarioViewModels;

namespace CB.Identidade.Api.Controllers
{
    [Route("api/identidade")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenService _tokenService;
        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              TokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid) return Response(ModelState);

            var usuarioACadastrar = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(usuarioACadastrar, usuarioRegistro.Senha);

            if (result.Succeeded)
            {
                var usuarioCadastrado = await _userManager.FindByEmailAsync(usuarioACadastrar.Email);
                await _userManager.AddClaimAsync(usuarioCadastrado, new Claim("Produto","Adicionar|Editar|Excluir|Movimentar"));

                var claims = await _userManager.GetClaimsAsync(usuarioCadastrado);
                return Response(_tokenService.GerarToken(usuarioCadastrado, claims));
            }

            result.Errors.ToList().ForEach(error => AdicionarErro(error.Description));
            return Response();
        }

        [HttpPost("autenticar")]
        public async Task<IActionResult> Login(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return Response(ModelState);

            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(usuarioLogin.Email);
                var claims = await _userManager.GetClaimsAsync(user);
                return Response(_tokenService.GerarToken(user, claims));
            }

            if (result.IsLockedOut)
            {
                AdicionarErro("Usuário temporariamente bloqueado por tentativas inválidas");
                return Response();
            }

            AdicionarErro("Usuário ou Senha incorretos");
            return Response();
        }

    }
}
