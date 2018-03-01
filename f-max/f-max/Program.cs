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
        public static string inputFile = @"..\..\..\..\data\test.in";

        static void Main(string[] args)
        {
            using (var reader = new StreamReader(inputFile))
            {
                Console.WriteLine(reader.ReadLine());
            }
        }
    }
}
