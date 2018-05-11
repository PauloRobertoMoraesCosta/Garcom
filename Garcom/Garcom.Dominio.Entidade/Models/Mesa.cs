using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garcom.Dominio.Entidade.Models
{
    public class Mesa : ClasseBase
    {

        /// <summary>
        /// Descrição da mesa
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Situação da Mesa
        /// </summary>
        public bool Ativo { get; set; }


        /// <summary>
        /// Validação da classe Mesa
        /// </summary>
        /// <returns></returns>
        public override bool Validacao()
        {
            if (string.IsNullOrWhiteSpace(Descricao))
                throw new Exception("Descrição não foi informada");

            if (Descricao.Length > 100)
                throw new Exception("Descrição não pode conter mais de 100 caracteres");

            return true;

        }

        /// <summary>
        /// Construtor padrão p/ o Dapper
        /// </summary>
        public Mesa()
        {

        }
    }
}
