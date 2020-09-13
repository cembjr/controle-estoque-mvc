using CB.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CB.WebApp.MVC.Services
{
    public class AutenticacaoService : BaseService, IAutenticacaoService
    {
        private const string URL_REGISTRAR = "api/identidade/registrar";
        private const string URL_AUTENTICAR = "api/identidade/autenticar";
        public AutenticacaoService(HttpClient httpClient) : base(httpClient, new Uri("https://localhost:44398/"))
        {
        }

        public async Task<ResponseResult<UsuarioResponseLogin>> Login(UsuarioLogin usuarioLogin)
        {
            var response = await Post<UsuarioLogin, UsuarioResponseLogin>(usuarioLogin, URL_AUTENTICAR);
            return response;
        }

        public async Task<ResponseResult<UsuarioResponseLogin>> Registro(UsuarioRegistro usuarioRegistro)
        {
            var response = await Post<UsuarioRegistro, UsuarioResponseLogin>(usuarioRegistro, URL_REGISTRAR);            
            return response;
        }
    }
}
