using System;
using HomeworkOne.Helpers;

namespace HomeworkOne.OptimizationFunctions
{
    public class Rosenbrock : IOptimizationFunction
    {
        public bool IsFitness { get; set; } 
        public double Resolve(double[] x, double[] y = null)
        {
            double sum = 0;
            for (var i = 0; i < Constants.NumberOfComponentsInSolution - 1; i++)
            {
                var xiPow = Math.Pow(x[i], 2);
                var firstParanthesis = x[i + 1] - xiPow;
                var secondParanthesis = 1 - x[i];
                var stepValue = 100 * Math.Pow(firstParanthesis, 2) + Math.Pow(secondParanthesis, 2);
                sum += stepValue;
            }

            var result = sum;

            return IsFitness ? 1 / (0.1 + result) : result;
        }
    }
}
