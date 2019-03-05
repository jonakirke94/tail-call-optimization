using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace tail_recursion
{
    public class Timer
    {
        private readonly Stopwatch StopWatch = null;

        public Timer()
        {
            StopWatch = Stopwatch.StartNew();
        }

        public double CheckNanoSeconds()
        {
            return StopWatch.Elapsed.TotalMilliseconds * 1000000;
        }
    }
}

