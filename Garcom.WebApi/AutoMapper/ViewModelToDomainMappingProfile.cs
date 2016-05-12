﻿using AutoMapper;
using Garcom.Dominio.Entidades;
using Garcom.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garcom.WebApi.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModel"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Usuario, UsuarioViewModel>();
        }
    }
}