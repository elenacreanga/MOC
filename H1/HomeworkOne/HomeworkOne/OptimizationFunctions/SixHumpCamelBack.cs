using System;
using HomeworkOne.Helpers;

namespace HomeworkOne.OptimizationFunctions
{
    public class SixHumpCamelBack : IOptimizationFunction
    {
        public bool IsFitness { get; set; }

        public double Resolve(double[] x, double[] y = null)
        {
            y = AdaptY(x);

            double sum = 0;
            for (var i = 0; i < Constants.NumberOfComponentsInSolution/2 - 1; i++)
            {
                var xPow = Math.Pow(x[i], 2);
                var yPow = Math.Pow(y[i], 2);
                var firstParanthesis = 4 - 2.1 * xPow + Math.Pow(x[i], 4 / 3);
                var xy = x[i] * y[i];
                var secondParanthesis = -4 + 4 * yPow;

                sum += firstParanthesis * xPow + xy + secondParanthesis * yPow;
            }

            var result = sum;

            return IsFitness ? 1 / (0.1 + result) : result;
        }

        private static double[] AdaptY(double[] x)
        {
            var y = new double[x.Length / 2];
            var j = 0;

            for (var i = x.Length / 2; i < x.Length; i++)
            {
                y[j] = x[i];
                j++;
            }
            return y;
        }
    }
}
