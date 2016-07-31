using System;

namespace Upa.Dominio.verifications
{
    public class DominioException : ApplicationException
    {
        public DominioException(string mensagem) : base(mensagem)
        {

        }
    }
}
