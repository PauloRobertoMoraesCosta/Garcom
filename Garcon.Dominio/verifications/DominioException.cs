using System;

namespace Garcom.Dominio.verifications
{
    public class DominioException : ApplicationException
    {
        public DominioException(string mensagem) : base(mensagem)
        {

        }
    }
}
