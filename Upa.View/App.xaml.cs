using System.Windows;
using Ninject;
using Upa.Apresentacao;

namespace Upa.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected MinhaNinject ninject;
        public App()
        {
            ninject = new MinhaNinject();
        }
        private void OnAppStartup(object sender, StartupEventArgs e)
        {
            Login login = ninject.Kernel.Get<Login>();
            login.Show();
            login.inicializar();
        }
    }

}
