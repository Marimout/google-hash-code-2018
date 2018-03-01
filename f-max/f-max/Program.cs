using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace f_max
{
    class Program
    {
        public static List<string> inputFiles = new List<string>
        {
            @"..\..\..\..\data\a_example.in",
            @"..\..\..\..\data\b_should_be_easy.in",
            @"..\..\..\..\data\c_no_hurry.in",
            @"..\..\..\..\data\d_metropolis.in",
            @"..\..\..\..\data\e_high_bonus.in"
        };

        public static int R;
        public static int C;
        public static int F;
        public static int N;
        public static int B;
        public static int T;

        public static List<Ride> rides = new List<Ride>();
        public static List<Car> cars = new List<Car>();

        static void Main(string[] args)
        {
            foreach (var inputFile in inputFiles)
            {
                Console.WriteLine(inputFile);
                Compute(inputFile, inputFile.Replace(".in", ".out"));
            }
        }

        private static void Compute(string inputFile, string outputFile)
        {
            using (var writer = new StreamWriter(outputFile))
            {
                using (var reader = new StreamReader(inputFile))
                {
                    var t = Array.ConvertAll(reader.ReadLine().Split(' '), Int32.Parse);
                    R = t[0];
                    C = t[1];
                    F = t[2];
                    N = t[3];
                    B = t[4];
                    T = t[5];

                    for (int i = 0; i < F; i++)
                    {
                        cars.Add(new Car());
                    }

                    for (int i = 0; i < N; i++)
                    {
                        t = Array.ConvertAll(reader.ReadLine().Split(' '), Int32.Parse);

                        rides.Add(new Ride(i, t[0], t[1], t[2], t[3], t[4], t[5]));
                    }

                    for (int i = 0; i < N; i++)
                    {
                        if (i % 50 == 0) Console.WriteLine(i);

                        Ride ride = null;
                        Car maxCar = null;
                        var maxScore = Int32.MinValue;

                        foreach (var c in cars)
                        {
                            ride = rides
                                .Where(x => x.AffectedCar == null)
                                .OrderBy(r => Math.Abs(c.x - r.a) + Math.Abs(c.y - r.b))
                                .First();

                            var score = EstimatedScore(c, ride);
                            if (score > maxScore)
                            {
                                maxScore = score;
                                maxCar = c;
                            }
                        }

                        if (maxScore > 0)
                        {
                            Affect(maxCar, ride);
                            rides.Remove(ride);
                        }
                    }

                    for (int i = 0; i < F; i++)
                    {
                        writer.Write($"{cars[i].Missions.Count} ");

                        foreach (var r in cars[i].Missions)
                        {
                            writer.Write($"{r} ");
                        }
                        writer.WriteLine();
                    }
                }
            }
        }

        public static int EstimatedScore(Car c, Ride r)
        {
            int s = 0;
            int distanceToStart = Math.Abs(c.x - r.a) + Math.Abs(c.y - r.b);

            var tt = c.t + distanceToStart;

            if (tt <= r.s)
            {
                tt = r.s;
                s += B;
            }

            if (tt + r.Length <= r.f)
            {
                s += r.Length;
            }
            else
            {
                s = 0;
            }

            return s;
        }

        public static void Affect(Car c, Ride ride)
        {
            c.Missions.Add(ride.id);

            Ride r = ride;

            c.x = r.x;
            c.y = r.y;

            int distanceToStart = Math.Abs(c.x - r.a) + Math.Abs(c.y - r.b);
            c.t = Math.Max(c.t + distanceToStart, r.s) + r.Length;

            r.AffectedCar = c;
        }
    }

    public class Car
    {
        public int x, y;
        public int t;

        public List<int> Missions;

        public Car()
        {
            Missions = new List<int>();
        }
    }

    public class Ride
    {
        public readonly int id;
        public int a, b;
        public int x, y;
        public int s;
        public int f;

        public Car AffectedCar;

        public Ride(int _id, int _a, int _b, int _x, int _y, int _s, int _f)
        {
            id = _id;
            a = _a;
            b = _b;
            x = _x;
            y = _y;
            s = _s;
            f = _f;

            AffectedCar = null;
        }

        public int Length
        {
            get
            {
                return Math.Abs(a - x) + Math.Abs(b - y);
            }
        }
    }
}

