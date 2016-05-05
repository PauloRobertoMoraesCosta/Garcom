using AutoMapper;
using Garcom.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garcom.Apresentacao.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomain"; }
        }

        protected override void Configure()
        {
            //Mapper.CreateMap<UsuarioViewModel, Usuario>();
        }
    }
}