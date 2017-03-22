using System;
using System.Collections.Generic;
using HomeworkOne.Helpers;

namespace HomeworkOne.OptimizationFunctions
{
    public class Griewangk : IOptimizationFunction
    {
        public bool IsFitness { get; set; }
        public double Resolve(double[] x, double[] y = null)
        {
            double product = 1;
            double sum = 0;
            for (var i = 0; i < Constants.NumberOfComponentsInSolution; i++)
            {
                sum = ComputeSum(x, i, sum);
                product = ComputeProduct(x, i, product);
            }

            var result = sum - product + 1;

            return IsFitness ? 1 / (0.1 + result) : result;
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
