using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ClassLibrary
{
    public sealed class Pudelko : IEquatable<Pudelko>, IEnumerable<double>, IFormattable
    {
        private static readonly Dictionary<UOM, string> dictionary = new()
        {
            {UOM.milimeter, "mm"},
            {UOM.centimeter, "cm"},
            {UOM.meter, "m"}
        };
        private readonly double _a;
        private readonly double _b;
        private readonly double _c;
        private readonly UOM _standardUnit;
        private readonly double gv_kurs;
        private readonly int gv_roundIndex;
        private readonly string gv_formatSpecifier;

        public double A { get => Math.Round(_a * gv_kurs, 3); }
        public double B { get => Math.Round(_b * gv_kurs, 3); }
        public double C { get => Math.Round(_c * gv_kurs, 3); }
        public double Objetosc { get => Math.Round(A * B * C, 9); }
        public double Pole { get => Math.Round((A * B + A * C + B * C) * 2, 6); }
        public double this[int index]
        {
            get
            {
                double[] wymiary = [ this.A, this.B, this.C ];
                return wymiary[index];
            }
        }
    
        public Pudelko(double? a = null, double? b = null, double? c = null, UOM unit = UOM.meter )
        {
            _standardUnit = unit;
            gv_kurs = (ushort)_standardUnit / (double)UOM.meter;

            double defaultValue = _standardUnit switch
            {
                UOM.milimeter => 100,
                UOM.centimeter => 10,
                UOM.meter => 0.1,
                _ => throw new FormatException()
            };

            gv_roundIndex = _standardUnit switch
            {
                UOM.milimeter => 0,
                UOM.centimeter => 1,
                UOM.meter => 3,
                _ => throw new FormatException()
            };

            gv_formatSpecifier = gv_roundIndex switch
            {
                0 => "F0",
                1 => "F1",
                3 => "F3",
                _ => throw new FormatException()
            };

            _a = (double)Math.Round((decimal)(a ?? defaultValue), gv_roundIndex, MidpointRounding.ToNegativeInfinity);
            _b = (double)Math.Round((decimal)(b ?? defaultValue), gv_roundIndex, MidpointRounding.ToNegativeInfinity);
            _c = (double)Math.Round((decimal)(c ?? defaultValue), gv_roundIndex, MidpointRounding.ToNegativeInfinity);

            if (_a <= 0 || _b <= 0 || _c <= 0) throw new ArgumentOutOfRangeException();
            if (A > 10 || B > 10 || C > 10) throw new ArgumentOutOfRangeException();
        }
        IEnumerator<double> IEnumerable<double>.GetEnumerator()
        {
            yield return A;
            yield return B;
            yield return C;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<double>)this).GetEnumerator();
        }
        public override string ToString()
        {
            string lv_unitText = dictionary.First(kvp => kvp.Key == _standardUnit).Value;
            return  String.Format
                 (
                    "{0} {1} × {2} {3} × {4} {5}",
                    (A / gv_kurs).ToString(gv_formatSpecifier), lv_unitText,
                    (B / gv_kurs).ToString(gv_formatSpecifier), lv_unitText, 
                    (C / gv_kurs).ToString(gv_formatSpecifier), lv_unitText
                );
        }
        public string ToString(string? format, IFormatProvider? formatProvider = null)
        {
            if (format == null) format = "m";
            else if (!dictionary.Values.Contains(format)) throw new FormatException();

            UOM lv_unit = dictionary.First(kvp => kvp.Value == format).Key;

            double lv_kurs = gv_kurs * (double)UOM.meter / (int)lv_unit;

            string lv_formatSpecifier = lv_unit switch
            {
                UOM.milimeter => "F0",
                UOM.centimeter => "F1",
                UOM.meter => "F3",
                _ => throw new FormatException()
            };
            return String.Format
                (
                    "{0} {1} × {2} {3} × {4} {5}",
                    (A * lv_kurs).ToString(lv_formatSpecifier), format,
                    (B * lv_kurs).ToString(lv_formatSpecifier), format,
                    (C * lv_kurs).ToString(lv_formatSpecifier), format
                );
        }
        public override bool Equals(object? obj)
        {
            if (obj is Pudelko objCast)
            {
                //return (this as IEquatable<Pudelko>).Equals(objCast as IEquatable<Pudelko>);
                return ((IEquatable<Pudelko>)this).Equals(objCast);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return (A / gv_kurs).GetHashCode() + (B / gv_kurs).GetHashCode() + (C / gv_kurs).GetHashCode();
        }
        bool IEquatable<Pudelko>.Equals(Pudelko? other)
        {
            if (other is null) return false;
            if (object.ReferenceEquals(this, other)) return true;

            foreach (double sideA in this)
            {
                bool ok = false;
                foreach (double sideB in other)
                {
                    if (sideA.Equals(sideB))
                    {
                        ok = true;
                    }
                }
                if (ok is false) return false;
            }

            return true;
        }
        public static bool operator ==(Pudelko one, Pudelko other)
        {
           return one.Equals(other);
        }
        public static bool operator !=(Pudelko one, Pudelko other)
        {
            return !(one==other);
        }
        public static Pudelko operator +(Pudelko one, Pudelko other)
        {
            var dimensionsA = new List<double> { one.A, one.B, one.C };
            var dimensionsB = new List<double> { other.A, other.B, other.C };

            double minVolume = double.MaxValue;
            Pudelko pudelko = null!;

            foreach (var permA in GetPermutations(dimensionsA, 3))
            {
                foreach (var permB in GetPermutations(dimensionsB, 3))
                {
                    try
                    {
                        var newBox = new Pudelko(Math.Max(permA[0], permB[0]),
                        Math.Max(permA[1], permB[1]),
                        permA[2] + permB[2]);

                        double volume = newBox.A * newBox.B * newBox.C;
                        if (volume < minVolume)
                        {
                            minVolume = volume;
                            pudelko = newBox;
                            }
                    }
                    catch (Exception) {;}
                }
            }            

            return pudelko;
            
            IEnumerable<IList<T>> GetPermutations<T>(IList<T> list, int length)
            {
                if (length == 1) return list.Select(t => (IList<T>)[t]);

                return GetPermutations(list, length - 1)
                    .SelectMany(t => list.Where(e => !t.Contains(e)),
                                (t1, t2) => t1.Concat([t2]).ToList());
            }
        }
        public static explicit operator double[](Pudelko one)
        {
            if (one is null) return [0, 0, 0];

            return [ one.A, one.B, one.C ];
        }
        public static implicit operator Pudelko(ValueTuple<int, int, int> tuple)
        {
            return new Pudelko(tuple.Item1, tuple.Item2, tuple.Item3, UOM.milimeter);  
        }
        public static Pudelko Parse(string source)
        {
            string[] it_elements = source.Split(" × ", StringSplitOptions.RemoveEmptyEntries);
            if (it_elements.Length != 3) throw new FormatException("the used format is incorrect!");

            double[] measures = new double[3];
            string? lv_format = null;

            for (int i = it_elements.Length - 1; i >= 0; i--)
            {
                string[] it_temp = it_elements[i].Split(' ');
                if (it_temp.Length != 2) throw new FormatException("the used format is incorrect!");
                
                if (lv_format is not null && lv_format != it_temp[1]) throw new FormatException("the used format is incorrect!");
                lv_format = it_temp[1];

                measures.SetValue(Math.Round(double.Parse(it_temp[0]), 3), i);
            }
            measures.Reverse();

            UOM lv_unit = lv_format switch
            {
                "mm" => UOM.milimeter,
                "cm" => UOM.centimeter,
                "m" => UOM.meter,
                _ => throw new FormatException()
            };

            return new Pudelko(measures[0], measures[1], measures[2], lv_unit);
        }
    }
}