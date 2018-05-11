using System;

namespace Garcom.Core
{
    public class Funcoes
    {
        public static string TratamentoMessageExcecao(Exception ex)
        {
            if (ex.InnerException != null)
                return ex.InnerException.Message;
            
            return ex.Message;
        }
    }
}
