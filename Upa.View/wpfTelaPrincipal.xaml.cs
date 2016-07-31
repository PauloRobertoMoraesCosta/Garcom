using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Upa.View
{
    /// <summary>
    /// Interaction logic for wpfTelaPrincipal.xaml
    /// </summary>
    public partial class wpfTelaPrincipal : Window
    {
        public wpfTelaPrincipal()
        {
            InitializeComponent();
            this.lblDataHora.Content = GetDateNow();
            UCMovimentacoesLencoes ucMovimentacoesLencoes = new UCMovimentacoesLencoes();
            TabItem tabItemMovimentacoesLencoes = new TabItem();
            tabItemMovimentacoesLencoes.Header = "Movimentações Lenções";
            tabItemMovimentacoesLencoes.Content = ucMovimentacoesLencoes;
            TbcPrincipal.Items.Add(tabItemMovimentacoesLencoes);
        }

        public string GetDateNow()
        {
            return DateTime.Now.Date.ToString().Substring(0, 10);
        }
    }
}
