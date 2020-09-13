using CB.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CB.WebApp.MVC.Services
{
    public interface IAutenticacaoService
    {
        Task<ResponseResult<UsuarioResponseLogin>> Login(UsuarioLogin usuarioLogin);

        Task<ResponseResult<UsuarioResponseLogin>> Registro(UsuarioRegistro usuarioRegistro);
    }
}
