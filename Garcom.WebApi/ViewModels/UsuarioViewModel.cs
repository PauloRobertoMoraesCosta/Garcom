using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garcom.WebApi.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Super { get; set; }
        public string Ativo { get; set; }

    }
}