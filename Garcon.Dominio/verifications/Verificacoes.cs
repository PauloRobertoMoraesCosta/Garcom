using System;

namespace Garcom.Dominio.verifications
{
    public class Verificacoes
    {
        public static string limpaCaracteresEspeciais(string palavraInicial)
        {
            string[] especiais = new String[] { "'", "=", ",", ";", "+", "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°" };
            try
            {
                foreach (var letra in especiais)
                {
                    palavraInicial = palavraInicial.Replace(letra, "");
                }
                return palavraInicial;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao limpar caracteres especiais" + ex.Message);
            }
        }
    }
}
