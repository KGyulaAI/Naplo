using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfOsztalyzas
{
    public class Osztalyzat
    {
        String nev;
        String datum;
        String tantargy;
        int jegy;

        public Osztalyzat(string nev, string datum, string tantargy, int jegy)
        {
            this.nev = nev;
            this.datum = datum;
            this.tantargy = tantargy;
            this.jegy = jegy;
        }

        public string Nev { get => nev; }
        public string Datum { get => datum; }
        public string Tantargy { get => tantargy; }
        public int Jegy { get => jegy; }
        public string CsaladiNev { get => nev.Split(" ")[0]; }

        public string ForditottNev(string nev)
        {
            Osztalyzat osztalyzat = new Osztalyzat(nev, DateTime.Now.ToString(), tantargy, jegy);
            string vezetekNev = osztalyzat.Nev.Split(" ")[0];
            string keresztNev = osztalyzat.Nev.Split(" ")[1];
            string forditottNev = $"{keresztNev} {vezetekNev}";
            return forditottNev;
        }
    }
        //Bővítse az osztályt! Készítsen CsaladiNev néven property-t, ami a névből a családi nevet adja vissza. Feltételezve, hogy a névnek csak az első tagja az.

        //Készítsen metódust ForditottNev néven, ami a két tagból álló nevek esetén megfordítja a névtagokat. Pld. Kiss Ádám => Ádám Kiss
}
