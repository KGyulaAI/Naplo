﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace WpfOsztalyzas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string fajlNev = "naplo.txt";
        //Így minden metódus fogja tudni használni.
        ObservableCollection<Osztalyzat> jegyek = new ObservableCollection<Osztalyzat>();

        public MainWindow()
        {
            InitializeComponent();
            // Fájlok kitallózásával tegye lehetővé a naplófájl kiválasztását!
            // Ha nem választ ki semmit, akkor "naplo.csv" legyen az állomány neve. A későbbiekben ebbe fog rögzíteni a program.
            // Configure open file dialog box
            var dialog = new OpenFileDialog();
            dialog.FileName = "naplo.csv"; // Default file name
            dialog.DefaultExt = ".csv"; // Default file extension
            dialog.Filter = "Text documents (.csv)|*.csv"; // Filter files by extension
            bool? result = dialog.ShowDialog();

            // A kiválasztott naplót egyből töltse be és a tartalmát jelenítse meg a datagrid-ben!
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
            lblUtvonal.Content = dialog.FileName;
            lblJegyekSzamaSzam.Content = dgJegyek.Items.Count;
            datDatum.SelectedDate = DateTime.Now;
        }

        private void btnRogzit_Click(object sender, RoutedEventArgs e)
        {
            // Ne lehessen rögzíteni, ha a következők valamelyike nem teljesül!
            // a) - A név legalább két szóból álljon és szavanként minimum 3 karakterből!
            //      Szó = A szöközökkel határolt karaktersorozat.
            // b) - A beírt dátum újabb, mint a mai dátum

            // A rögzítés mindig az aktuálisan megnyitott naplófájlba történjen!
            string teljesNev = txtNev.Text;
            string[] nevSplit = teljesNev.Split(" ");
            int nevekSzama = nevSplit.Count();
            bool nevKevesebbMintHaromKarakter = false;
            DateTime currentDateTime = DateTime.Now;
            DateTime selectedDateTime = datDatum.SelectedDate.Value;
            foreach (string nevResz in nevSplit)
            {
                if (nevResz.Count() < 3)
                {
                    nevKevesebbMintHaromKarakter = true;
                    break;
                }
            }

            if (nevekSzama >= 2)
            {
                if (nevKevesebbMintHaromKarakter == false)
                {
                    if (selectedDateTime < currentDateTime)
                    {
                        //A CSV szerkezetű fájlba kerülő sor előállítása
                        string csvSor = $"{txtNev.Text};{datDatum.Text};{cboTantargy.Text};{sliJegy.Value}";
                        //Megnyitás hozzáfűzéses írása (APPEND)
                        StreamWriter sw = new StreamWriter(fajlNev, append: true);
                        sw.WriteLine(csvSor);
                        sw.Close();
                    }
                    else
                    {
                        MessageBox.Show("Nem lehet jövőbeli időpontot beállítani!");
                    }
                }
                else
                {
                    MessageBox.Show("A nevek legalább 3 betűből kell álljanak!");
                }
            }
            else
            {
                MessageBox.Show("Ez nem egy teljes név!");
            }
            //todo Az újonnan felvitt jegy is jelenjen meg a datagrid-ben!
            dgJegyek.ItemsSource = jegyek;
            dgJegyek.Items.Refresh();
        }

        private void btnBetolt_Click(object sender, RoutedEventArgs e)
        {
            jegyek.Clear();  //A lista előző tartalmát töröljük
            StreamReader sr = new StreamReader(fajlNev); //olvasásra nyitja az állományt
            while (!sr.EndOfStream) //amíg nem ér a fájl végére
            {
                string[] mezok = sr.ReadLine().Split(";"); //A beolvasott sort feltördeli mezőkre
                                                           //A mezők értékeit felhasználva létrehoz egy objektumot
                Osztalyzat ujJegy = new Osztalyzat(mezok[0], mezok[1], mezok[2], int.Parse(mezok[3]));
                jegyek.Add(ujJegy); //Az objektumot a lista végére helyezi
            }
            sr.Close(); //állomány lezárása

            //A Datagrid adatforrása a jegyek nevű lista lesz.
            //A lista objektumokat tartalmaz. Az objektumok lesznek a rács sorai.
            //Az objektum nyilvános tulajdonságai kerülnek be az oszlopokba.
            dgJegyek.ItemsSource = jegyek;
        }

        private void sliJegy_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblJegy.Content = sliJegy.Value; //Több alternatíva van e helyett! Legjobb a Data Binding!
        }

        //todo Felület bővítése: Az XAML átszerkesztésével biztosítsa, hogy láthatóak legyenek a következők!
        // - A naplófájl neve
        // - A naplóban lévő jegyek száma
        // - Az átlag
        
        //todo Új elemek frissítése: Figyeljen rá, ha új jegyet rögzít, akkor frissítse a jegyek számát és az átlagot is!

        //todo Helyezzen el alkalmas helyre 2 rádiónyomógombot!
        //Feliratok: [■] Vezetéknév->Keresztnév [O] Keresztnév->Vezetéknév
        //A táblázatban a név azserint szerepeljen, amit a rádiónyomógomb mutat!
        //A feladat megoldásához használja fel a ForditottNev metódust!
        //Módosíthatja az osztályban a Nev property hozzáférhetőségét!
        //Megjegyzés: Felételezzük, hogy csak 2 tagú nevek vannak
    }
}

