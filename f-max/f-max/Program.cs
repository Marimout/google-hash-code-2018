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
        public static string inputFile = @"..\..\..\..\data\a_example.in";

        public static int R;
        public static int C;
        public static int F;
        public static int N;
        public static int B;
        public static int T;

        public static List<Ride> rides = new List<Ride>();

        static void Main(string[] args)
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

                for (int i = 0; i < N; i++)
                {
                    t = Array.ConvertAll(reader.ReadLine().Split(' '), Int32.Parse);

                    rides.Add(new Ride(t[0], t[1], t[2], t[3], t[4], t[5]));
                }
            }
        }
    }

    public class Ride
    {
        public int a, b;
        public int x, y;
        public int s;
        public int f;

        public Ride(int _a, int _b, int _x, int _y, int _s, int _f)
        {
            a = _a;
            b = _b;
            x = _x;
            y = _y;
            s = _s;
            f = _f;
        }
    }
}
