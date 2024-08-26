using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading;

namespace PudelkoUnitTests
{

    [TestClass]
    public static class InitializeCulture
    {
        [AssemblyInitialize]
        public static void SetEnglishCultureOnAllUnitTest(TestContext context)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        }
    }

    // ========================================

    [TestClass]
    public class UnitTestsPudelkoConstructors
    {
        private static double defaultSize = 0.1; // w metrach
        private static double accuracy = 0.001; //dokładność 3 miejsca po przecinku

        private void AssertPudelko(Pudelko p, double expectedA, double expectedB, double expectedC)
        {
            Assert.AreEqual(expectedA, p.A, delta: accuracy);
            Assert.AreEqual(expectedB, p.B, delta: accuracy);
            Assert.AreEqual(expectedC, p.C, delta: accuracy);
        }

        #region Constructor tests ================================

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_Default()
        {
            Pudelko p = new Pudelko();

            Assert.AreEqual(defaultSize, p.A, delta: accuracy);
            Assert.AreEqual(defaultSize, p.B, delta: accuracy);
            Assert.AreEqual(defaultSize, p.C, delta: accuracy);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.543, 3.1,
                 1.0, 2.543, 3.1)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1.0, 2.543, 3.1)] // dla metrów liczą się 3 miejsca po przecinku
        public void Constructor_3params_DefaultMeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a, b, c);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.543, 3.1,
                 1.0, 2.543, 3.1)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1.0, 2.543, 3.1)] // dla metrów liczą się 3 miejsca po przecinku
        public void Constructor_3params_InMeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UOM.meter);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(100.0, 25.5, 3.1,
                 1.0, 0.255, 0.031)]
        [DataRow(100.0, 25.58, 3.13,
                 1.0, 0.255, 0.031)] // dla centymertów liczy się tylko 1 miejsce po przecinku
        public void Constructor_3params_InCentimeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a: a, b: b, c: c, unit: UOM.centimeter);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(100, 255, 3,
                 0.1, 0.255, 0.003)]
        [DataRow(100.0, 25.58, 3.13,
                 0.1, 0.025, 0.003)] // dla milimetrów nie liczą się miejsca po przecinku
        public void Constructor_3params_InMilimeters(double a, double b, double c,
                                                     double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(unit: UOM.milimeter, a: a, b: b, c: c);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }


        // ----

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.5, 1.0, 2.5)]
        [DataRow(1.001, 2.599, 1.001, 2.599)]
        [DataRow(1.0019, 2.5999, 1.001, 2.599)]
        public void Constructor_2params_DefaultMeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(a, b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.5, 1.0, 2.5)]
        [DataRow(1.001, 2.599, 1.001, 2.599)]
        [DataRow(1.0019, 2.5999, 1.001, 2.599)]
        public void Constructor_2params_InMeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(a: a, b: b, unit: UOM.meter);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11.0, 2.5, 0.11, 0.025)]
        [DataRow(100.1, 2.599, 1.001, 0.025)]
        [DataRow(2.0019, 0.25999, 0.02, 0.002)]
        public void Constructor_2params_InCentimeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(unit: UOM.centimeter, a: a, b: b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11, 2.0, 0.011, 0.002)]
        [DataRow(100.1, 2599, 0.1, 2.599)]
        [DataRow(200.19, 2.5999, 0.2, 0.002)]
        public void Constructor_2params_InMilimeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(unit: UOM.milimeter, a: a, b: b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        // -------

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(2.5)]
        public void Constructor_1param_DefaultMeters(double a)
        {
            Pudelko p = new Pudelko(a);

            Assert.AreEqual(a, p.A);
            Assert.AreEqual(0.1, p.B);
            Assert.AreEqual(0.1, p.C);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(2.5)]
        public void Constructor_1param_InMeters(double a)
        {
            Pudelko p = new Pudelko(a);

            Assert.AreEqual(a, p.A);
            Assert.AreEqual(0.1, p.B);
            Assert.AreEqual(0.1, p.C);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11.0, 0.11)]
        [DataRow(100.1, 1.001)]
        [DataRow(2.0019, 0.02)]
        public void Constructor_1param_InCentimeters(double a, double expectedA)
        {
            Pudelko p = new Pudelko(unit: UOM.centimeter, a: a);

            AssertPudelko(p, expectedA, expectedB: 0.1, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11, 0.011)]
        [DataRow(100.1, 0.1)]
        [DataRow(200.19, 0.2)]
        public void Constructor_1param_InMilimeters(double a, double expectedA)
        {
            Pudelko p = new Pudelko(unit: UOM.milimeter, a: a);

            AssertPudelko(p, expectedA, expectedB: 0.1, expectedC: 0.1);
        }

        // ---

        public static IEnumerable<object[]> DataSet1Meters_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] {-1.0, 2.5, 3.1},
            new object[] {1.0, -2.5, 3.1},
            new object[] {1.0, 2.5, -3.1},
            new object[] {-1.0, -2.5, 3.1},
            new object[] {-1.0, 2.5, -3.1},
            new object[] {1.0, -2.5, -3.1},
            new object[] {-1.0, -2.5, -3.1},
            new object[] {0, 2.5, 3.1},
            new object[] {1.0, 0, 3.1},
            new object[] {1.0, 2.5, 0},
            new object[] {1.0, 0, 0},
            new object[] {0, 2.5, 0},
            new object[] {0, 0, 3.1},
            new object[] {0, 0, 0},
            new object[] {10.1, 2.5, 3.1},
            new object[] {10, 10.1, 3.1},
            new object[] {10, 10, 10.1},
            new object[] {10.1, 10.1, 3.1},
            new object[] {10.1, 10, 10.1},
            new object[] {10, 10.1, 10.1},
            new object[] {10.1, 10.1, 10.1}
        };

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet1Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_DefaultMeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet1Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InMeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UOM.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1, 1)]
        [DataRow(1, -1, 1)]
        [DataRow(1, 1, -1)]
        [DataRow(-1, -1, 1)]
        [DataRow(-1, 1, -1)]
        [DataRow(1, -1, -1)]
        [DataRow(-1, -1, -1)]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(0, 1, 0)]
        [DataRow(1, 0, 0)]
        [DataRow(0, 0, 0)]
        [DataRow(0.01, 0.1, 1)]
        [DataRow(0.1, 0.01, 1)]
        [DataRow(0.1, 0.1, 0.01)]
        [DataRow(1001, 1, 1)]
        [DataRow(1, 1001, 1)]
        [DataRow(1, 1, 1001)]
        [DataRow(1001, 1, 1001)]
        [DataRow(1, 1001, 1001)]
        [DataRow(1001, 1001, 1)]
        [DataRow(1001, 1001, 1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InCentimeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UOM.centimeter);
        }


        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1, 1)]
        [DataRow(1, -1, 1)]
        [DataRow(1, 1, -1)]
        [DataRow(-1, -1, 1)]
        [DataRow(-1, 1, -1)]
        [DataRow(1, -1, -1)]
        [DataRow(-1, -1, -1)]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(0, 1, 0)]
        [DataRow(1, 0, 0)]
        [DataRow(0, 0, 0)]
        [DataRow(0.1, 1, 1)]
        [DataRow(1, 0.1, 1)]
        [DataRow(1, 1, 0.1)]
        [DataRow(10001, 1, 1)]
        [DataRow(1, 10001, 1)]
        [DataRow(1, 1, 10001)]
        [DataRow(10001, 10001, 1)]
        [DataRow(10001, 1, 10001)]
        [DataRow(1, 10001, 10001)]
        [DataRow(10001, 10001, 10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InMiliimeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UOM.milimeter);
        }


        public static IEnumerable<object[]> DataSet2Meters_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] {-1.0, 2.5},
            new object[] {1.0, -2.5},
            new object[] {-1.0, -2.5},
            new object[] {0, 2.5},
            new object[] {1.0, 0},
            new object[] {0, 0},
            new object[] {10.1, 10},
            new object[] {10, 10.1},
            new object[] {10.1, 10.1}
        };

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet2Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_DefaultMeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet2Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InMeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UOM.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1)]
        [DataRow(1, -1)]
        [DataRow(-1, -1)]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(0, 0)]
        [DataRow(0.01, 1)]
        [DataRow(1, 0.01)]
        [DataRow(0.01, 0.01)]
        [DataRow(1001, 1)]
        [DataRow(1, 1001)]
        [DataRow(1001, 1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InCentimeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UOM.centimeter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1)]
        [DataRow(1, -1)]
        [DataRow(-1, -1)]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(0, 0)]
        [DataRow(0.1, 1)]
        [DataRow(1, 0.1)]
        [DataRow(0.1, 0.1)]
        [DataRow(10001, 1)]
        [DataRow(1, 10001)]
        [DataRow(10001, 10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InMilimeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UOM.milimeter);
        }




        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(10.1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_DefaultMeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(10.1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InMeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a, unit: UOM.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(0.01)]
        [DataRow(1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InCentimeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a, unit: UOM.centimeter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(0.1)]
        [DataRow(10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InMilimeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new(a, unit: UOM.milimeter);
        }

        #endregion


        #region ToString tests ===================================

        [TestMethod, TestCategory("String representation")]
        [DataRow(2.5, 9.321, 0.1, UOM.meter, "2.500 m × 9.321 m × 0.100 m")]
        [DataRow(250.0, 93.21, 10.0, UOM.centimeter, "250.0 cm × 93.2 cm × 10.0 cm")]
        [DataRow(2500, 9321, 100, UOM.milimeter, "2500 mm × 9321 mm × 100 mm")]
        public void ToString_Default_Culture_EN(double a, double b, double c, UOM uom, string expectedString)
        {
            var p = new Pudelko(a, b, c, uom);

            Assert.AreEqual(expectedString, p.ToString());
        }

        [DataTestMethod, TestCategory("String representation")]
        [DataRow(null, 2.5, 9.321, 0.1, "2.500 m × 9.321 m × 0.100 m")]
        [DataRow("m", 2.5, 9.321, 0.1, "2.500 m × 9.321 m × 0.100 m")]
        [DataRow("cm", 2.5, 9.321, 0.1, "250.0 cm × 932.1 cm × 10.0 cm")]
        [DataRow("mm", 2.5, 9.321, 0.1, "2500 mm × 9321 mm × 100 mm")]
        public void ToString_Formattable_Culture_EN(string format, double a, double b, double c, string expectedStringRepresentation)
        {
            var p = new Pudelko(a, b, c, unit: UOM.meter);
            Assert.AreEqual(expectedStringRepresentation, p.ToString(format));
        }

        [TestMethod, TestCategory("String representation")]
        [ExpectedException(typeof(FormatException))]
        public void ToString_Formattable_WrongFormat_FormatException()
        {
            var p = new Pudelko(1);
            var stringformatedrepreentation = p.ToString("wrong code");
        }

        #endregion


        #region Pole, Objętość ===================================

        [DataTestMethod, TestCategory("Area")]
        [DataRow(1.0, 2.543, 3.1, UOM.meter, 27.0526)]
        [DataRow(1.0001, 2.54387, 3.1005, UOM.meter, 27.0526)]
        [DataRow(100, 255, 3, UOM.centimeter, 5.313)]
        [DataRow(0.1, 0.255, 0.003, UOM.meter, 0.05313)]
        [DataRow(0.1, 0.025, 0.003, UOM.meter, 0.00575)]
        [DataRow(1.0, 2.5, 1.0, UOM.meter, 12.0)]
        [DataRow(1.001, 2.599, 1.001, UOM.meter, 12.410398)]
        [DataRow(1.0019, 2.5999, 1.001, UOM.meter, 12.410398)]
        [DataRow(10, 2.0, 0.011, UOM.meter, 40.264)]
        [DataRow(100.1, 599, 0.1, UOM.centimeter, 12.005962)]
        [DataRow(200.19, 2.5999, 1.2, UOM.milimeter, 0.001204)]
        [DataRow(10000, 1, 1, UOM.milimeter, 0.040002)]
        [DataRow(1, 10000, 1, UOM.milimeter, 0.040002)]
        [DataRow(1, 1, 10000, UOM.milimeter, 0.040002)]
        [DataRow(10000, 10000, 1, UOM.milimeter, 200.04)]
        [DataRow(10000, 1, 10000, UOM.milimeter, 200.04)]
        [DataRow(1, 10000, 10000, UOM.milimeter, 200.04)]
        [DataRow(10000, 10000, 10000, UOM.milimeter, 600)]
        public void AreaCalculation(double a, double b, double c, UOM uom, double area)
        {
            var p = new Pudelko(a , b, c, uom);

            Assert.AreEqual(p.Pole, area);
        }

        [DataTestMethod, TestCategory("Volume")]
        [DataRow(1.0, 2.543, 3.1, UOM.meter, 7.8833)]
        [DataRow(1.0001, 2.54387, 3.1005, UOM.meter, 7.8833)]
        [DataRow(100, 255, 3, UOM.centimeter, 0.0765)]
        [DataRow(0.1, 0.255, 0.003, UOM.meter, 0.0000765)]
        [DataRow(0.1, 0.025, 0.003, UOM.meter, 0.0000075)]
        [DataRow(1.0, 2.5, 1.0, UOM.meter, 2.5)]
        [DataRow(1.001, 2.599, 1.001, UOM.meter, 2.604200599)]
        [DataRow(1.0019, 2.5999, 1.001, UOM.meter, 2.604200599)]
        [DataRow(10, 2.0, 0.011, UOM.meter, 0.22)]
        [DataRow(100.1, 599, 0.1, UOM.centimeter, 0.00599599)]
        [DataRow(200.19, 2.5999, 1.2, UOM.milimeter, 0.0000004)]
        [DataRow(10000, 1, 1, UOM.milimeter, 0.00001)]
        [DataRow(1, 10000, 1, UOM.milimeter, 0.00001)]
        [DataRow(1, 1, 10000, UOM.milimeter, 0.00001)]
        [DataRow(10000, 10000, 1, UOM.milimeter, 0.1)]
        [DataRow(10000, 1, 10000, UOM.milimeter, 0.1)]
        [DataRow(1, 10000, 10000, UOM.milimeter, 0.1)]
        [DataRow(10000, 10000, 10000, UOM.milimeter, 1000)]
        public void VolumeCalculation(double a, double b, double c, UOM uom, double volume)
        {
            var p = new Pudelko(a, b, c, uom);

            Assert.AreEqual(p.Objetosc, volume);
        }

        #endregion

        #region Equals ===========================================
        [DataTestMethod, TestCategory("Equality")]
        [DataRow(1, 3.05, 2.1, UOM.meter)]
        [DataRow(2.1, 1, 3.05, UOM.meter)]
        [DataRow(2100, 1000, 3050, UOM.milimeter)]
        public void Equality(double a, double b, double c, UOM uom)
        {
            Pudelko p = new(1, 2.1, 3.05);
            Pudelko check = new Pudelko(a, b, c, uom);
            Assert.IsTrue(p.Equals(check));
        }

        #endregion

        #region Operators overloading ===========================

        [DataTestMethod, TestCategory("OperatorPlus")]
        [DataRow(2.5, 3.0, 4.0, 1.5, 2.0, 2.5, 41.25)]
        [DataRow(3.0, 2.5, 4.0, 1.5, 2.0, 2.5, 41.25)]
        [DataRow(4.0, 3.0, 2.5, 1.5, 2.0, 2.5, 41.25)]
        [DataRow(3.0, 4.0, 2.5, 1.5, 2.0, 2.5, 41.25)]
        [DataRow(2.5, 4.0, 3.0, 1.5, 2.0, 2.5, 41.25)]
        [DataRow(4.0, 2.5, 3.0, 1.5, 2.0, 2.5, 41.25)]
        [DataRow(2.5, 3.0, 4.0, 2.0, 1.5, 2.5, 41.25)]
        [DataRow(3.0, 2.5, 4.0, 2.0, 1.5, 2.5, 41.25)]
        [DataRow(4.0, 3.0, 2.5, 2.0, 1.5, 2.5, 41.25)]
        [DataRow(3.0, 4.0, 2.5, 2.0, 1.5, 2.5, 41.25)]
        [DataRow(2.5, 4.0, 3.0, 2.0, 1.5, 2.5, 41.25)]
        [DataRow(4.0, 2.5, 3.0, 2.0, 1.5, 2.5, 41.25)]
        [DataRow(2.5, 3.0, 4.0, 2.5, 2.0, 1.5, 41.25)]
        [DataRow(3.0, 2.5, 4.0, 2.5, 2.0, 1.5, 41.25)]
        [DataRow(4.0, 3.0, 2.5, 2.5, 2.0, 1.5, 41.25)]
        [DataRow(3.0, 4.0, 2.5, 2.5, 2.0, 1.5, 41.25)]
        [DataRow(2.5, 4.0, 3.0, 2.5, 2.0, 1.5, 41.25)]
        [DataRow(4.0, 2.5, 3.0, 2.5, 2.0, 1.5, 41.25)]
        [DataRow(2.5, 3.0, 4.0, 1.5, 2.5, 2.0, 41.25)]
        [DataRow(3.0, 2.5, 4.0, 1.5, 2.5, 2.0, 41.25)]
        [DataRow(4.0, 3.0, 2.5, 1.5, 2.5, 2.0, 41.25)]
        [DataRow(3.0, 4.0, 2.5, 1.5, 2.5, 2.0, 41.25)]
        [DataRow(2.5, 4.0, 3.0, 1.5, 2.5, 2.0, 41.25)]
        [DataRow(4.0, 2.5, 3.0, 1.5, 2.5, 2.0, 41.25)]
        [DataRow(2.5, 3.0, 4.0, 2.0, 2.5, 1.5, 41.25)]
        [DataRow(3.0, 2.5, 4.0, 2.0, 2.5, 1.5, 41.25)]
        [DataRow(4.0, 3.0, 2.5, 2.0, 2.5, 1.5, 41.25)]
        [DataRow(3.0, 4.0, 2.5, 2.0, 2.5, 1.5, 41.25)]
        [DataRow(2.5, 4.0, 3.0, 2.0, 2.5, 1.5, 41.25)]
        [DataRow(4.0, 2.5, 3.0, 2.0, 2.5, 1.5, 41.25)]
        [DataRow(2.5, 3.0, 4.0, 2.5, 1.5, 2.0, 41.25)]
        [DataRow(3.0, 2.5, 4.0, 2.5, 1.5, 2.0, 41.25)]
        [DataRow(4.0, 3.0, 2.5, 2.5, 1.5, 2.0, 41.25)]
        [DataRow(3.0, 4.0, 2.5, 2.5, 1.5, 2.0, 41.25)]
        [DataRow(2.5, 4.0, 3.0, 2.5, 1.5, 2.0, 41.25)]
        [DataRow(4.0, 2.5, 3.0, 2.5, 1.5, 2.0, 41.25)]

        public void PlusOperatorOverload(double a1, double b1, double c1, double a2, double b2, double c2, double volume)
        {
            Pudelko box1 = new(a1, b1, c1);
            Pudelko box2 = new(a2, b2, c2);
            Pudelko result = box1 + box2;

            Assert.AreEqual(result.Objetosc, volume);
        }

        [DataTestMethod, TestCategory("OperatorEquals")]
        [DataRow(1, 3.05, 2.1, UOM.meter)]
        [DataRow(2.1, 1, 3.05, UOM.meter)]
        [DataRow(2100, 1000, 3050, UOM.milimeter)]
        public void EqualityOperatorOverload(double a, double b, double c, UOM uom)
        {
            Pudelko p = new(1, 2.1, 3.05);
            Pudelko check = new Pudelko(a, b, c, uom);
            Assert.IsTrue(p == check);
        }

        #endregion

        #region Conversions =====================================
        [TestMethod]
        public void ExplicitConversion_ToDoubleArray_AsMeters()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            double[] tab = (double[])p;
            Assert.AreEqual(3, tab.Length);
            Assert.AreEqual(p.A, tab[0]);
            Assert.AreEqual(p.B, tab[1]);
            Assert.AreEqual(p.C, tab[2]);
        }

        [TestMethod]
        public void ImplicitConversion_FromAalueTuple_As_Pudelko_InMilimeters()
        {
            var (a, b, c) = (2500, 9321, 100); // in milimeters, ValueTuple
            Pudelko p = (a, b, c);
            Assert.AreEqual((int)(p.A * 1000), a);
            Assert.AreEqual((int)(p.B * 1000), b);
            Assert.AreEqual((int)(p.C * 1000), c);
        }

        #endregion

        #region Indexer, enumeration ============================
        [TestMethod]
        public void Indexer_ReadFrom()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            Assert.AreEqual(p.A, p[0]);
            Assert.AreEqual(p.B, p[1]);
            Assert.AreEqual(p.C, p[2]);
        }

        [TestMethod]
        public void ForEach_Test()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            var tab = new[] { p.A, p.B, p.C };
            int i = 0;
            foreach (double x in p)
            {
                Assert.AreEqual(x, tab[i]);
                i++;
            }
        }

        #endregion

        #region Parsing =========================================

        [TestMethod]
        public void Parsing_CorrectString_Meters()
        {
            string src = "2.500 m × 9.321 m × 0.100 m";
            Pudelko p = Pudelko.Parse(src);
            Assert.AreEqual(p.A, 2.500);
            Assert.AreEqual(p.B, 9.321);
            Assert.AreEqual(p.C, 0.100);
        }
        
        [TestMethod]
        public void Parsing_CorrectString_Centimeters()
        {
            string src = "250 cm × 932.1 cm × 10 cm";
            Pudelko p = Pudelko.Parse(src);
            Assert.AreEqual(p.A, 2.500);
            Assert.AreEqual(p.B, 9.321);
            Assert.AreEqual(p.C, 0.100);
        }

        [TestMethod]
        public void Parsing_CorrectString_Milimeters()
        {
            string src = "2500 mm × 9321 mm × 100 mm";
            Pudelko p = Pudelko.Parse(src);
            Assert.AreEqual(p.A, 2.500);
            Assert.AreEqual(p.B, 9.321);
            Assert.AreEqual(p.C, 0.100);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parsing_IncorrectString_Separators()
        {
            string src = "2500 mm x 9321 mm × 100 mm";
            Pudelko p = Pudelko.Parse(src);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parsing_IncorrectString_WhiteSpaces()
        {
            string src = "2500 mm x9321 mm × 100 mm";
            Pudelko p = Pudelko.Parse(src);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parsing_IncorrectString_Units()
        {
            string src = "2500 m × 9321 cm × 100 mm";
            Pudelko p = Pudelko.Parse(src);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parsing_IncorrectString_IncompleteString()
        {
            string src = "2500 m x 9321 m";
            Pudelko p = Pudelko.Parse(src);
        }

        #endregion

    }
}