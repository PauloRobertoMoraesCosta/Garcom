using Ninject;
using Upa.Apresentacao.InterfacesApp;
using Upa.Apresentacao.ServiceApp;
using Upa.Dados.Repositorios;
using Upa.Dominio.Interfaces.Repositorios;
using Upa.Dominio.Interfaces.Servicos;
using Upa.Dominio.Servicos;

namespace Upa.Apresentacao
{
    public class MinhaNinject
    {
        private IKernel kernel;

        public IKernel Kernel
        {
            get { return kernel; }
            set { kernel = value; }
        }

        public MinhaNinject()
        {
            this.kernel = new StandardKernel();
            kernel.Bind(typeof(IAppServiceBase<>)).To(typeof(AppServiceBase<>)).InTransientScope();
            kernel.Bind<IAppServiceUsuario>().To<AppServiceUsuario>().InTransientScope();

            kernel.Bind(typeof(IServicoBase<>)).To(typeof(ServicoBase<>)).InTransientScope();
            kernel.Bind<IServicoUsuario>().To<ServicoUsuario>().InTransientScope();

            kernel.Bind(typeof(IRepositorioBase<>)).To(typeof(RepositorioBase<>)).InTransientScope();
            kernel.Bind<IRepositorioUsuario>().To<RepositorioUsuario>().InTransientScope();

        }
    }
}
