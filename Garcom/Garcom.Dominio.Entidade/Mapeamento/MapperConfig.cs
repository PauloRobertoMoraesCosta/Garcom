using AutoMapper;

namespace Garcom.Dominio.Entidade.Mapeamento
{
    /// <summary>
    /// Classe que faz a inicialização do mapeamento
    /// </summary>
    public class MapperConfig
    {            

        /// <summary>
        /// Objeto que serve para fazer o mapeamento
        /// </summary>
        public static IMapper imap { get; private set; }

        /// <summary>
        /// Método que adicona as classes de profile ao mapper
        /// </summary>
        public static void ConfigurarMapper()
        {
            var mapper = new MapperConfiguration((map) =>
            {
                map.AddProfile<MapperPerfil>();
            });

            imap = mapper.CreateMapper();
        }

    }
}
