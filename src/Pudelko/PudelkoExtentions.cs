using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class PudelkoExtentions
    {
        public static Pudelko Kompresuj(this Pudelko o) 
        { 
            if (o is null) throw new ArgumentNullException();

            double measure = Math.Round(Math.Pow(o.Objetosc, 1.0/3.0), 3);

            return new Pudelko(measure, measure, measure);
        }
        public static int Comparison(Pudelko p1, Pudelko p2)
        {
            if (p1.Objetosc != p2.Objetosc) return p1.Objetosc.CompareTo(p2.Objetosc);
            if (p1.Pole != p2.Pole) return p1.Pole.CompareTo(p2.Pole);

            return (p1.A + p1.B + p1.C).CompareTo(p2.A + p2.B + p2.C);
        }
    }
}
