using System.Resources;

namespace Garcom.Core
{
    public class GerenciadorMensagensRetorno : IGerenciadorMensagens
    {
        private ResourceManager _manager;

        public GerenciadorMensagensRetorno()
        {
            _manager = new ResourceManager(typeof(MensagensRetorno));
        }

        public string GetMensagem(string name)
        {
            return _manager.GetString(name);
        }
    }
}
