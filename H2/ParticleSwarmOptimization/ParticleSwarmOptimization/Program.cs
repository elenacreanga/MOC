using System;
using System.Collections.Generic;
using ParticleSwarmOptimization.Functions;
using ParticleSwarmOptimization.Helpers;
using ParticleSwarmOptimization.Models;

namespace ParticleSwarmOptimization
{
    public class Program
    {
        static void Main(string[] args)
        {
            var lows = Constants.LOW;
            var highs = Constants.HIGH;

            IList<Interval> intervals = new List<Interval>();

            for (var i = 0; i < Constants.PROBLEM_DIMENSION; i++)
            {
                var interval = new Interval(lows[i], highs[i]);
                intervals.Add(interval);
            }

            var rastriginFunction = new Rastrigin(intervals);
            new PSO().Execute(rastriginFunction);
            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}
