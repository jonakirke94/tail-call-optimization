using System;
using System.Linq;

namespace tail_recursion
{
    public class Program
    {
        private static readonly int n = 100;
        private static readonly int[] a = Enumerable.Range(0, n).ToArray();


        static void Main(string[] args)
        {
            Mark("Marking regular recursion", SumRec);
            Mark("Marking tail recursion", SumRecTail);
            Console.ReadLine();

            // NOTE:

            // The huge benefit of using tail calls is that since they're the LAST action within the method, the stack can reuse it
            // for the next recursive call (i.e. you don't have the overhead of preserving any state)

            // C# does NOT support tail call optimization however there's a well known workaround called "Trampolining"

        }

        public static double Mark(string description, Func<int, double> method)
        {
            int noOfRuns = 30;
            int count = 100_000;
            double st = 0.0, sst = 0.0, dummy = 0.0;
            Console.WriteLine(description);
            for (int j = 0; j < noOfRuns; j++)
            {
                Timer t = new Timer();
                for (int i = 0; i < count; i++)
                {
                    dummy += method(n);
                }
                double time = t.CheckNanoSeconds() / count;
                st += time;
                sst += time * time;
            }
            double mean = st / noOfRuns, sdev = Math.Sqrt((sst - mean * mean * noOfRuns) / (noOfRuns - 1));
            Console.WriteLine("avg {0} ns, {1} sdev", Math.Round(mean, 5), Math.Round(sdev, 5));
            return dummy / noOfRuns;
        }

        public static double Mark(string description, Func<int, int, double> method)
        {
            int noOfRuns = 30;
            int count = 100_000;
            double st = 0.0, sst = 0.0, dummy = 0.0;
            Console.WriteLine(description);
            for (int j = 0; j < noOfRuns; j++)
            {
                Timer t = new Timer();
                for (int i = 0; i < count; i++)
                {
                    dummy += method(n, 0);
                }
                double time = t.CheckNanoSeconds() / count;
                st += time;
                sst += time * time;
            }
            double mean = st / noOfRuns, sdev = Math.Sqrt((sst - mean * mean * noOfRuns) / (noOfRuns - 1));
            Console.WriteLine("avg {0} ns, {1} sdev", Math.Round(mean, 5), Math.Round(sdev, 5));
            return dummy / noOfRuns;
        }

        private static double SumRecTail(int start, int sum = 0)
        {
            return start == 0 ? sum : SumRecTail(start - 1, sum + start);
        }

        private static double SumRec(int end)
        {
            return end > 0 ? SumRec(end - 1) + end : 0;
        }
    }
}
