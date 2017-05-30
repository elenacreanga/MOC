using System;
using System.Collections.Generic;
using ParticleSwarmOptimization.Helpers;
using ParticleSwarmOptimization.Models;

namespace ParticleSwarmOptimization.Functions
{
    public class Griewangk : IOptimizationFunction
    {
        private readonly double _errTolerance;
        public IList<Interval> Intervals { get; set; }
        public bool IsFitness { get; set; }
        public double GetErrorTolerance()
        {
            return _errTolerance;
        }

        public Griewangk(IList<Interval> intervals)
        {
            _errTolerance = 1E-20;
            Intervals = intervals;
        }

        public double Resolve(Position position)
        {
            double product = 1;
            double sum = 0;
            for (var i = 0; i < position.Values.Length; i++)
            {
                sum = ComputeSum(position.Values, i, sum);
                product = ComputeProduct(position.Values, i, product);
            }

            var result = sum - product + 1;

            return IsFitness ? 1 / (0.1 + result) : result;
        }

        public IList<Interval> GetIntervals()
        {
            return Intervals;
        }

        private double ComputeProduct(IReadOnlyList<double> chromosomes, int i, double product)
        {
            var valueStepForProduct = Math.Cos(chromosomes[i] / Math.Sqrt(i + 1));
            product *= valueStepForProduct;
            return product;
        }

        private double ComputeSum(IReadOnlyList<double> chromosomes, int i, double sum)
        {
            var valueStepForSum = Math.Pow(chromosomes[i], 2) / 4000;
            sum += valueStepForSum;
            return sum;
        }
    }
}
