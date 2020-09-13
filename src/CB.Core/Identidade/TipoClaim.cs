using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CB.Core.Identidade
{
    public static class TipoClaim
    {
        public const string Adicionar = "Adicionar";
        public const string Editar = "Editar";
        public const string Excluir = "Excluir";
        public const string Movimentar = "Movimentar";

        public static IEnumerable<Claim> PermitirTodasClaims(string tipo)
        {
            return new List<Claim>()
            {
                new Claim(tipo, TipoClaim.Adicionar.ToString()),
                new Claim(tipo, TipoClaim.Editar.ToString()),
                new Claim(tipo, TipoClaim.Excluir.ToString()),
                new Claim(tipo, TipoClaim.Movimentar.ToString())
            };
        }
    }

}
