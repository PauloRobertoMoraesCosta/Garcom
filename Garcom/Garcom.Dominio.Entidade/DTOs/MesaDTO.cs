using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garcom.Dominio.Entidade.DTOs
{
    public class MesaDTO
    {
        public string UsuarioLogado { get; set; }
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}
