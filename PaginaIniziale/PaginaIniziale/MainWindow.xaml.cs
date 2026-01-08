using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PaginaIniziale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Memory_Click(object sender, RoutedEventArgs e)
        {
            Memory finestraMemory = new Memory();
            finestraMemory.Show();
            this.Close();
        }

        private void btnImpiccato_Click(object sender, RoutedEventArgs e)
        {
            Impiccato finestraImpiccato = new Impiccato();
            finestraImpiccato.Show();
            this.Close();
        }

        private void btnNumero_Click(object sender, RoutedEventArgs e)
        {
            IndovinaNumero indovinaNumero = new IndovinaNumero();
            indovinaNumero.Show();

            this.Close();
        }
    }
}
