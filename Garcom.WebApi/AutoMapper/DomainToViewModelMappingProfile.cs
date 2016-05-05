using AutoMapper;
//using Garcom.Apresentacao.ViewModels;
using Garcom.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garcom.Apresentacao.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get {return "DomainToViewModel";}
        }

        protected override void Configure()
        {
            //Mapper.CreateMap<Usuario, UsuarioViewModel>();
        }
    }
}