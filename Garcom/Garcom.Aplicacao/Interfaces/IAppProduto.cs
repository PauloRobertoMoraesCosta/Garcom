using Garcom.Dominio.Entidade.DTOs;

namespace Garcom.Aplicacao.Interfaces
{
    public interface IAppProduto : IAppBase<ProdutoDTO>
    {
        ProdutoDTO SelecionaProdutoPorCodigoRapido(string codigoRapido);
        void RemoverProduto(ProdutoDTO produtoDTO);
        void Desfazer(ProdutoDTO produtoDTO);
        void Inativar(ProdutoDTO produtoDTO);
        bool JaUtilizado(int id);
    }
}
