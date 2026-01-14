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

        private void CaricaImmagini()
        {
            imgTalpa = new BitmapImage(new Uri("Immagini_Talpa/talpa.png", UriKind.Relative));
            imgVuoto = new BitmapImage(new Uri("Immagini_Talpa/vuoto.png", UriKind.Relative));
        }

        private void InizializzaGriglia()
        {
            bottoni = new Button[9];
            GrigliaTalpe.Children.Clear();

            for (int i = 0; i < 9; i++)
            {
                Button b = new Button();
                b.Margin = new Thickness(5);
                b.Click += ColpisciTalpa;

                b.Content = new Image { Source = imgVuoto };
                b.Tag = "vuoto";

                bottoni[i] = b;
                GrigliaTalpe.Children.Add(b);
            }
        }

        private void InizializzaTimer()
        {
            timerGioco = new DispatcherTimer();
            timerGioco.Interval = TimeSpan.FromSeconds(1);
            timerGioco.Tick += TimerGioco_Tick;

            timerTalpa = new DispatcherTimer();
            timerTalpa.Interval = TimeSpan.FromMilliseconds(700);
            timerTalpa.Tick += TimerTalpa_Tick;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            tempo = 30;
            punti = 0;
            txtTempo.Text = tempo.ToString();
            txtPunti.Text = punti.ToString();

            foreach (var b in bottoni)
            {
                b.Content = new Image { Source = imgVuoto };
                b.Tag = "vuoto";
            }

            timerGioco.Start();
            timerTalpa.Start();
        }

        private void TimerGioco_Tick(object sender, EventArgs e)
        {
            tempo--;
            txtTempo.Text = tempo.ToString();

            if (tempo <= 0)
            {
                timerGioco.Stop();
                timerTalpa.Stop();

                foreach (var b in bottoni)
                {
                    b.Content = new Image { Source = imgVuoto };
                    b.Tag = "vuoto";
                }

                MessageBox.Show($"Tempo scaduto! Hai fatto {punti} punti.");
            }
        }

        private void TimerTalpa_Tick(object sender, EventArgs e)
        {
            foreach (var b in bottoni)
            {
                b.Content = new Image { Source = imgVuoto };
                b.Tag = "vuoto";
            }

            int index = rnd.Next(0, 9);

            bottoni[index].Content = new Image { Source = imgTalpa };
            bottoni[index].Tag = "talpa";
        }

        private void ColpisciTalpa(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            if (b.Tag != null && b.Tag.ToString() == "talpa")
            {
                punti++;
                txtPunti.Text = punti.ToString();

                b.Content = new Image { Source = imgVuoto };
                b.Tag = "vuoto";
            }
        }
    }
}
