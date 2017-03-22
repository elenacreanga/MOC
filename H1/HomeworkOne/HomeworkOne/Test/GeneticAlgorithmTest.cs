using System;
using HomeworkOne.Helpers;
using HomeworkOne.OptimizationFunctions;

namespace HomeworkOne.Test
{
    public static class GeneticAlgorithmTest
    {
        public static void GetResult(OptimizationFunction function)
        {
            Console.WriteLine("\nNumber of components:");
            Constants.NumberOfComponentsInSolution = Convert.ToInt32(Console.ReadLine());

            var geneticAlgorithm = new GeneticAlgorithm
            {
                MutationProbability = (float) 0.001,
                CrossoverProbability = (float) 0.7,
                NumberOfChromosomes = 10
            };
            Interval interval;
            switch (function)
            {
                case OptimizationFunction.Griewangk:
                    Console.WriteLine("Griewangk\n");
                    var fitnessGriewangk = new Griewangk
                    {
                        IsFitness = true
                    };
                    interval = new Interval(-600, 600);

                    geneticAlgorithm.GetResult(optimizationFunction: fitnessGriewangk,
                        numberOfIterations: 100,
                        interval: interval,
                        precission: 2);
                    Console.ReadKey();
                    break;
                case OptimizationFunction.Rastrigin:
                    Console.WriteLine("Rastrigin\n");
                    var fitnessRastrigin = new Rastrigin
                    {
                        IsFitness = true
                    };
                    interval = new Interval(-5.12, 5.12);

                    geneticAlgorithm.GetResult(optimizationFunction: fitnessRastrigin,
                        numberOfIterations: 100,
                        interval: interval,
                        precission: 2);
                    Console.ReadKey();
                    break;
                case OptimizationFunction.Rosenbrock:
                    Console.WriteLine("Rosenbrock\n");
                    var fitnessRosenbrock = new Rosenbrock
                    {
                        IsFitness = true
                    };
                    interval = new Interval(-2.048, 2.048);

                    geneticAlgorithm.GetResult(optimizationFunction: fitnessRosenbrock,
                        numberOfIterations: 100,
                        interval: interval,
                        precission: 2);
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Invalid number!");
                    break;
            }
        }
    }
}
