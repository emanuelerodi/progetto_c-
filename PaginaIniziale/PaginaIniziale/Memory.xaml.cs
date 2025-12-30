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
using System.Windows.Shapes;

namespace PaginaIniziale
{
    /// <summary>
    /// Logica di interazione per Memory.xaml
    /// </summary>
    public partial class Memory : Window
    {
        public Memory()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow home = new MainWindow(); 
            home.Show(); 

            this.Close();
        }
    }
}
