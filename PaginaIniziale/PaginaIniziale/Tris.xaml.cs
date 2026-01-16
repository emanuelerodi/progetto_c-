using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PaginaIniziale
{
    public partial class Tris : Window
    {
        private bool turnoX = true;

        public Tris()
        {
            InitializeComponent();
            CreaGriglia();
            Disabilita();
        }

        private void CreaGriglia()
        {
            GrigliaTris.Children.Clear();

            for (int i = 0; i < 9; i++)
            {
                Button b = new Button();
                b.FontSize = 36;
                b.FontWeight = FontWeights.Bold;
                b.Background = Brushes.White;
                b.Click += Cella_Click;

                GrigliaTris.Children.Add(b);
            }
        }

        private void Cella_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            if (b.Content != null)
                return;

            if (turnoX)
            {
                b.Content = "X";
                b.Foreground = Brushes.Red;
            }
            else
            {
                b.Content = "O";
                b.Foreground = Brushes.Blue;
            }

            if (ControllaVittoria())
            {
                string vincitore;

                if (turnoX)
                    vincitore = "X";
                else
                    vincitore = "O";

                MessageBox.Show("Ha vinto " + vincitore);
                Disabilita();
                return;
            }

            turnoX = !turnoX;
        }


        private bool ControllaVittoria()
        {
            var c = GrigliaTris.Children;

            string[] v = new string[9];

            for (int i = 0; i < 9; i++)
            {
                Button btn = c[i] as Button;

                if (btn.Content != null)
                    v[i] = btn.Content.ToString();
                else
                    v[i] = null;
            }

            // righe
            if (v[0] == v[1] && v[1] == v[2] && v[0] != null) return true;
            if (v[3] == v[4] && v[4] == v[5] && v[3] != null) return true;
            if (v[6] == v[7] && v[7] == v[8] && v[6] != null) return true;

            // colonne
            if (v[0] == v[3] && v[3] == v[6] && v[0] != null) return true;
            if (v[1] == v[4] && v[4] == v[7] && v[1] != null) return true;
            if (v[2] == v[5] && v[5] == v[8] && v[2] != null) return true;

            // diagonali
            if (v[0] == v[4] && v[4] == v[8] && v[0] != null) return true;
            if (v[2] == v[4] && v[4] == v[6] && v[2] != null) return true;

            return false;
        }


        private void Disabilita()
        {
            foreach (Button b in GrigliaTris.Children)
                b.IsEnabled = false;
        }

        private void Inizia_Click(object sender, RoutedEventArgs e)
        {
            turnoX = true;
            CreaGriglia();
        }

        private void Ricomincia_Click(object sender, RoutedEventArgs e)
        {
            turnoX = true;
            CreaGriglia();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }
    }
}
