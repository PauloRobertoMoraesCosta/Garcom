using AutoMapper;
using Garcom.Dominio.Entidades;
using Garcom.WebApi.ViewModels;

namespace Garcom.WebApi.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Usuario, UsuarioViewModel>();
        }
    }
}