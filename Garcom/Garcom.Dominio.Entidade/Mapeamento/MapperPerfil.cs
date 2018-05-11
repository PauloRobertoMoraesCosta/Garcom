using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Entidade.DTOs;

namespace Garcom.Dominio.Entidade.Mapeamento
{
    public class MapperPerfil : Profile
    {
        public MapperPerfil()
        {
            //Usuarios
            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<UsuarioDTO, Usuario>();

            //Perfil
            CreateMap<Perfil, PerfilDTO>();
            CreateMap<PerfilDTO, Perfil>();

            //Ingrediente
            CreateMap<Ingrediente, IngredienteDTO>();
            CreateMap<IngredienteDTO, Ingrediente>();

            //Produto
            CreateMap<Produto, ProdutoDTO>();
            CreateMap<ProdutoDTO, Produto>();

            //Grupo de produto
            CreateMap<GrupoProduto, GrupoProdutoDTO>();
            CreateMap<GrupoProdutoDTO, GrupoProduto>();

            //Tamanho de produto
            CreateMap<TamanhoProduto, TamanhoProdutoDTO>();
            CreateMap<TamanhoProdutoDTO, TamanhoProduto>();

            //Produto Ingrediente
            CreateMap<ProdutoIngrediente, ProdutoIngredienteDTO>();
            CreateMap<ProdutoIngredienteDTO, ProdutoIngrediente>();

            //Produto Ingrediente Tamanho Valor
            CreateMap<ProdutoIngredienteTamanhoValor, ProdutoIngredienteTamanhoValorDTO>();
            CreateMap<ProdutoIngredienteTamanhoValorDTO, ProdutoIngredienteTamanhoValor>();

            CreateMap<ProdutoTamanhoValor, ProdutoTamanhoValorDTO>();
            CreateMap<ProdutoTamanhoValorDTO, ProdutoTamanhoValor>();

            //Mesa
            CreateMap<Mesa, MesaDTO>();
            CreateMap<MesaDTO, Mesa>();
        }
    }
}
