using ClassLibrary;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            Pudelko box1 = new(2.5, 3.0, 4.0);
            Pudelko box2 = new(1.5, 2.0, 2.5);

            Pudelko result = box1 + box2;

            Console.WriteLine(result);
            Console.WriteLine(result.Objetosc);

            result = result.Kompresuj();

            Console.WriteLine(result);
            Console.WriteLine(result.Objetosc);


            List<Pudelko> boxes = new()
            {
                box1,
                box2,
                result,
                new Pudelko(1.0, 2.543, 3.1, UOM.meter),
                new Pudelko(1.0001, 2.54387, 3.1005, UOM.meter),
                new Pudelko(0.003, 0.025, 0.1, UOM.meter),
                new Pudelko(0.1, 0.025, 0.003, UOM.meter),
                new Pudelko(1.0, 2.5, 1.0, UOM.meter),
                new Pudelko(1.001, 2.599, 1.001, UOM.meter),
                new Pudelko(1.0019, 2.5999, 1.001, UOM.meter),
                new Pudelko(10, 2.0, 0.011, UOM.meter),
                new Pudelko(100.1, 599, 0.1, UOM.centimeter),
                new Pudelko(200.19, 2.5999, 1.2, UOM.milimeter),
                new Pudelko(10000, 1, 1, UOM.milimeter),
                new Pudelko(1, 10000, 1, UOM.milimeter),
                new Pudelko(1, 1, 10000, UOM.milimeter),
                new Pudelko(10000, 10000, 1, UOM.milimeter),
                new Pudelko(10000, 1, 10000, UOM.milimeter),
                new Pudelko(1, 10000, 10000, UOM.milimeter),
                new Pudelko(10000, 10000, 10000, UOM.milimeter),
                new Pudelko(2.5, 3.0, 4.0),
                new Pudelko(3.0, 2.5, 4.0),
                new Pudelko(4.0, 3.0, 2.5),
                new Pudelko(3.0, 4.0, 2.5),
                new Pudelko(2.5, 4.0, 3.0),
                new Pudelko(4.0, 2.5, 3.0),
                new Pudelko(2.5, 3.0, 4.0),
                new Pudelko(3.0, 2.5, 4.0),
                new Pudelko(4.0, 3.0, 2.5),
                new Pudelko(3.0, 4.0, 2.5),
                new Pudelko(2.5, 4.0, 3.0),
                new Pudelko(4.0, 2.5, 3.0),
                new Pudelko(2.5, 3.0, 4.0),
                new Pudelko(3.0, 2.5, 4.0),
                new Pudelko(4.0, 3.0, 2.5),
                new Pudelko(3.0, 4.0, 2.5),
                new Pudelko(2.5, 4.0, 3.0),
                new Pudelko(4.0, 2.5, 3.0),
                new Pudelko(2.5, 3.0, 4.0),
                new Pudelko(3.0, 2.5, 4.0),
                new Pudelko(4.0, 3.0, 2.5),
                new Pudelko(3.0, 4.0, 2.5),
                new Pudelko(2.5, 4.0, 3.0),
                new Pudelko(4.0, 2.5, 3.0),
                new Pudelko(2.5, 3.0, 4.0),
                new Pudelko(3.0, 2.5, 4.0),
                new Pudelko(4.0, 3.0, 2.5),
                new Pudelko(3.0, 4.0, 2.5),
                new Pudelko(2.5, 4.0, 3.0),
                new Pudelko(4.0, 2.5, 3.0),
                new Pudelko(2.5, 3.0, 4.0),
                new Pudelko(3.0, 2.5, 4.0),
                new Pudelko(4.0, 3.0, 2.5),
                new Pudelko(3.0, 4.0, 2.5),
                new Pudelko(2.5, 4.0, 3.0),
                new Pudelko(4.0, 2.5, 3.0)
            };

            boxes.Sort(PudelkoExtentions.Comparison);

            foreach (var box in boxes)
            {
                Console.WriteLine(box + " ; Objetosc: " + box.Objetosc + " ; Pole: " + box.Pole + " ; Suma krawędzi: " + (box.A + box.B + box.C));
            }
        }
    }
}
