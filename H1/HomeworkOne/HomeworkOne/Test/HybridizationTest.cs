using System;
using System.Collections;
using System.Collections.Generic;
using HomeworkOne.Helpers;
using HomeworkOne.OptimizationFunctions;

namespace HomeworkOne.Test
{
    public class HybridizationTest
    {
        public static void GetResult(OptimizationFunction function)
        {
            Console.WriteLine("\nNumber of components:");
            Constants.NumberOfComponentsInSolution = Convert.ToInt32(Console.ReadLine());

            var hybridization = new Hybridization()
            {
                MutationProbability = (float)0.001,
                CrossoverProbability = (float)0.7,
                NumberOfChromosomes = 10
            };
            List<BitArray> result;
            Interval interval;

            switch (function)
            {
                case OptimizationFunction.Griewangk:
                    Console.WriteLine("Griewangk\n");
                    var fitnessGriewangk = new Griewangk
                    {
                        IsFitness = true
                    };
                    var griewangk = new Griewangk();
                    interval = new Interval(-600, 600);

                    result = hybridization.GetResult(optimizationFunction: fitnessGriewangk,
                                                        numberOfIterations: 10,
                                                        interval: interval,
                                                        precission: 2);

                    //new HillClimbing().Resolve(optimizationFunction: griewangk,
                    //                     interval: interval,
                    //                     precission: 2,
                    //                     strategy: Strategy.Best,
                    //                     startSolution: result[1]);

                    Console.ReadKey();
                    break;
                case OptimizationFunction.Rastrigin:
                    Console.WriteLine("Rastrigin\n");

                    var fitnessRastrigin = new Rastrigin
                    {
                        IsFitness = true
                    };
                    interval = new Interval(-5.12, 5.12);
                   
                    var rastrigin = new Rastrigin();
                    result = hybridization.GetResult(optimizationFunction: fitnessRastrigin,
                                                        numberOfIterations: 10,
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
                    var rosenbrock = new Rosenbrock();
                    interval = new Interval(-2.048, 2.048);

                    result = hybridization.GetResult(optimizationFunction: fitnessRosenbrock,
                                                        numberOfIterations: 10,
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
