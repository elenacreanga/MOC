using System;
using HomeworkOne.Helpers;

namespace HomeworkOne.OptimizationFunctions
{
    public class Rastrigin : IOptimizationFunction
    {
        public bool IsFitness { get; set; }

        public double Resolve(double[] x, double[] y = null)
        {
            double tenN = 10 * Constants.NumberOfComponentsInSolution;
            double sum = 0;
            for (var i = 0; i < Constants.NumberOfComponentsInSolution; i++)
            {
                var cosOfTwoPiXi = Math.Cos(2 * Math.PI * x[i]);
                var stepValue = Math.Pow(x[i], 2) - 10 * cosOfTwoPiXi;
                sum += stepValue;
            }
            var result = tenN + sum;
            return IsFitness ? 1 / (0.1 + result) : result;
        }
    }
}
