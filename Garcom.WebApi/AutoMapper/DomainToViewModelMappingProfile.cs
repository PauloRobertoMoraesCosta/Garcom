using AutoMapper;
using Garcom.Dominio.Entidades;
using Garcom.WebApi.ViewModels;

namespace Garcom.WebApi.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get {return "ViewModelToDomain";}
        }

        protected override void Configure()
        {
            Mapper.CreateMap<UsuarioViewModel, Usuario>();
        }
    }
}