using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garcom.API.Helper
{
    /// <summary>
    /// Classe responsável por listar os Papeis de cada perfil na autenticação
    /// </summary>
    public static class RegraPerfil
    {
        public const string Administrador = "1";
        public const string Caixa = "2";
        public const string Cozinha = "3";
        public const string Garcom = "4";
    }
}