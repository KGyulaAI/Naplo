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
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string fajlNev = "naplo.txt";
        List<Osztalyzat> jegyek = new List<Osztalyzat>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OsztalyzatokBetoltese(string fajlNev)
        {
            jegyek.Clear();
            StreamReader sr = new StreamReader(fajlNev);
            while (!sr.EndOfStream)
            {
                string[] mezok = sr.ReadLine().Split(";");
                Osztalyzat ujJegy = new Osztalyzat(mezok[0], mezok[1], mezok[2], int.Parse(mezok[3]));
                jegyek.Add(ujJegy);
            }
            sr.Close();
            dgJegyek.ItemsSource = jegyek;
            MessageBox.Show("Az állomány beolvasása befejeződött!");
        }

        private void btnRogzit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBetoltes_Click(object sender, RoutedEventArgs e)
        {
            OsztalyzatokBetoltese(fajlNev);
        }
    }
}
