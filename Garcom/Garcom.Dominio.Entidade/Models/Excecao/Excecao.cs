using System;

namespace Garcom.Dominio.Entidade.Models.Excecao
{
    public class Excecao : ClasseBase
    {
        public string Rotina { get; set; }
        public string Mensagem { get; set; }
        public string Parametros { get; set; }

        public override bool Validacao()
        {
            if (string.IsNullOrEmpty(Rotina))
                throw new Exception("Rotina não pode ser nulo ou vazio");

            if (string.IsNullOrEmpty(Mensagem))
                throw new Exception("Mensagem não pode ser nulo ou vazio");

            return true;
        }
    }
}
