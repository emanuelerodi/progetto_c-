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
using System.Windows.Threading;

namespace PaginaIniziale
{
    /// <summary>
    /// Logica di interazione per Memory.xaml
    /// </summary>
    public partial class Memory : Window
    {

        private DispatcherTimer timer; 
        private int secondi = 0;
        private List<BitmapImage> carte;
        private BitmapImage immagine_coperta;
        private Button primaCarta = null; 
        private Button secondaCarta = null; 
        private int coppieTrovate = 0;
        public Memory()
        {
            InitializeComponent();
            InizializzaTimer();
            InizializzaGioco();
        }

        private void InizializzaGioco()
        {
            carte = new List<BitmapImage>
            {
                new BitmapImage(new Uri("Immagini_memory/alieno.png")),
                new BitmapImage(new Uri("Immagini_memory/alieno.png")),
                
                new BitmapImage(new Uri("Immagini_memory/astronauta.png")),
                new BitmapImage(new Uri("Immagini_memory/astronauta.png")),
                
                new BitmapImage(new Uri("Immagini_memory/luna.png")),
                new BitmapImage(new Uri("Immagini_memory/luna.png")),

                new BitmapImage(new Uri("Immagini_memory/navicella.png")),
                new BitmapImage(new Uri("Immagini_memory/navicella.png")),

                new BitmapImage(new Uri("Immagini_memory/razzo.png")),
                new BitmapImage(new Uri("Immagini_memory/razzo.png")),
                
                new BitmapImage(new Uri("Immagini_memory/satellite.png")),
                new BitmapImage(new Uri("Immagini_memory/satellite.png")),

                new BitmapImage(new Uri("Immagini_memory/sole.png")),
                new BitmapImage(new Uri("Immagini_memory/sole.png")),

                new BitmapImage(new Uri("Immagini_memory/terra.png")),
                new BitmapImage(new Uri("Immagini_memory/terra.png")),
            };
            
            immagine_coperta= new BitmapImage(new Uri("Immagini_memory/coperta.png"));

        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow home = new MainWindow(); 
            home.Show(); 

            this.Close();
        }

        private void Ricomincia_Click(object sender, RoutedEventArgs e)
        {
            secondi = 0; 
            lblTimer.Content = "Tempo: 0 s"; 
            timer.Stop(); 
            timer.Start(); 
            InizializzaGioco();
        }
        private void InizializzaTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            secondi++;
            lblTimer.Content = $"Tempo: {secondi} s";
        }

    }
}
