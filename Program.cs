using System;
using System.Diagnostics;

namespace lab5
{
    class Program
    {
        public static int[] data;
        static Stopwatch sw = new Stopwatch();
        static void Main(string[] args)
        {
            int minNumOfElements = 10000;
            int maxNumOfElements = 10000000;
            if(args.Length == 2) {
                minNumOfElements = Int32.Parse(args[0]);
                maxNumOfElements = Int32.Parse(args[1]);
            }

            run(minNumOfElements, maxNumOfElements);

        }

        static void run(int minNumOfElements, int maxNumOfElements) {
            Algorithm[] algs = new Algorithm[] {new Serial(), new Parallel()};

            for (int i = minNumOfElements; i <= maxNumOfElements; i*=13) {
                Console.WriteLine("Unordered; Num of Elements: " + i);
                data = Algorithm.fillData(i);

                foreach (var alg in algs) {
                    run(alg);
                }
                Console.WriteLine();

                Console.WriteLine("Asc; Num of Elements: " + i);
                data = Algorithm.fillDataAsc(i);

                foreach (var alg in algs) {
                    run(alg);
                }
                Console.WriteLine();

                Console.WriteLine("Desc; Num of Elements: " + i);
                data = Algorithm.fillDataDesc(i);

                foreach (var alg in algs) {
                    run(alg);
                }
                Console.WriteLine("--------------------------------");
                Console.WriteLine();
            }
        }

        static void run(Algorithm alg) {
            sw.Restart();
            alg.perform(data);
            sw.Stop();

            Console.WriteLine(alg.GetType().Name + " algorithm time: " + sw.Elapsed.TotalMilliseconds);
        }
    }
}
