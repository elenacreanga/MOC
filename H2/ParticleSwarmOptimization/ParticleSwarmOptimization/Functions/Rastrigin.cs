using System;
using System.Collections.Generic;
using ParticleSwarmOptimization.Helpers;
using ParticleSwarmOptimization.Models;

namespace ParticleSwarmOptimization.Functions
{
    public class Rastrigin : IOptimizationFunction
    {
        public IList<Interval> Intervals { get; set; }

        private readonly double _errTolerance;
        public bool IsFitness { get; set; }

        public double GetErrorTolerance()
        {
            return _errTolerance;
        }

        public Rastrigin(IList<Interval> intervals)
        {
            Intervals = intervals;
            _errTolerance = 1E-20;
        }

        public double Resolve(Position position)
        {
            var size = position.Values.Length;
            double tenN = 10 * size;
            double sum = 0;
            for (var i = 0; i < size; i++)
            {
                var xi = position.Values[i];
                var cosOfTwoPiXi = Math.Cos(2 * Math.PI * xi);
                var stepValue = Math.Pow(xi, 2) - 10 * cosOfTwoPiXi;
                sum += stepValue;
            }
            var result = tenN + sum;
            return IsFitness ? 1 / (0.1 + result) : result;
        }

        public IList<Interval> GetIntervals()
        {
            return Intervals;
        }
    }
}
