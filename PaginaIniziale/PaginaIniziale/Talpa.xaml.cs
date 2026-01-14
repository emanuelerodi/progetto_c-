using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace PaginaIniziale
{
    public partial class Talpa : Window
    {
        private Button[] bottoni;
        private DispatcherTimer timerGioco;
        private DispatcherTimer timerTalpa;

        private int tempo = 30;
        private int punti = 0;
        private Random rnd = new Random();

        private BitmapImage imgTalpa;
        private BitmapImage imgVuoto;

        public Talpa()
        {
            InitializeComponent();
            CaricaImmagini();
            InizializzaGriglia();
            InizializzaTimer();
        }

        // 🔹 CARICAMENTO IMMAGINI CORRETTO (WPF)
        private void CaricaImmagini()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            imgTalpa = new BitmapImage(new Uri(System.IO.Path.Combine(basePath, "Immagini_Talpa", "talpa.png")));
            imgTalpa.Freeze();

            imgVuoto = new BitmapImage(new Uri(System.IO.Path.Combine(basePath, "Immagini_Talpa", "vuoto.png")));
            imgVuoto.Freeze();
        }



        // 🔹 CREAZIONE GRIGLIA
        private void InizializzaGriglia()
        {
            bottoni = new Button[9];
            GrigliaTalpe.Children.Clear();

            for (int i = 0; i < 9; i++)
            {
                Image img = new Image
                {
                    Source = imgVuoto,
                    Stretch = System.Windows.Media.Stretch.Fill
                };

                Button b = new Button
                {
                    Margin = new Thickness(5),
                    Content = img,
                    Tag = "vuoto"
                };

                b.Click += ColpisciTalpa;

                bottoni[i] = b;
                GrigliaTalpe.Children.Add(b);
            }
        }


        // 🔹 TIMER
        private void InizializzaTimer()
        {
            timerGioco = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timerGioco.Tick += TimerGioco_Tick;

            timerTalpa = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(700)
            };
            timerTalpa.Tick += TimerTalpa_Tick;
        }

        // 🔹 START GIOCO
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            tempo = 30;
            punti = 0;

            txtTempo.Text = tempo.ToString() + "s";
            txtPunti.Text = punti.ToString();

            foreach (var b in bottoni)
            {
                ((Image)b.Content).Source = imgVuoto;
                b.Tag = "vuoto";
            }

            timerGioco.Start();
            timerTalpa.Start();
        }

        // 🔹 TIMER TEMPO
        private void TimerGioco_Tick(object sender, EventArgs e)
        {
            tempo--;
            txtTempo.Text = tempo.ToString() + "s";

            if (tempo <= 0)
            {
                timerGioco.Stop();
                timerTalpa.Stop();

                foreach (var b in bottoni)
                {
                    ((Image)b.Content).Source = imgVuoto;
                    b.Tag = "vuoto";
                }

                MessageBox.Show($"Tempo scaduto! Hai fatto {punti} punti.");
            }
        }

        // 🔹 COMPARSA TALPA
        private void TimerTalpa_Tick(object sender, EventArgs e)
        {
            foreach (var b in bottoni)
            {
                ((Image)b.Content).Source = imgVuoto;
                b.Tag = "vuoto";
            }

            int index = rnd.Next(0, bottoni.Length);

            ((Image)bottoni[index].Content).Source = imgTalpa;
            bottoni[index].Tag = "talpa";
        }

        // 🔹 CLICK SULLA TALPA
        private void ColpisciTalpa(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            if (b.Tag?.ToString() == "talpa")
            {
                punti++;
                txtPunti.Text = punti.ToString();

                ((Image)b.Content).Source = imgVuoto;
                b.Tag = "vuoto";
            }
        }
    }
}
