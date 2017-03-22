using System;
using System.Collections;
using HomeworkOne.Helpers;
using HomeworkOne.OptimizationFunctions;

namespace HomeworkOne.Test
{
    public static class HillClimbingTest
    {
        public static void GetResult(Strategy strategy, OptimizationFunction function)
        {
            Interval interval;
            BitArray startSolution = null;
            switch (function)
            {
                case OptimizationFunction.Griewangk:
                    var griewangk = new Griewangk();
                    InitializeConstants("Griewangk");
                    interval = new Interval(-600, 600);
                    startSolution = Mapper.GetRandomSolution(interval, Constants.Precission);
                    new HillClimbing().Resolve(griewangk, interval, Constants.Precission, strategy, startSolution);
                    Console.ReadKey();
                    break;
                case OptimizationFunction.Rastrigin:
                    var rastrigin = new Rastrigin();
                    InitializeConstants("Rastrigin");
                    interval = new Interval(-5.12, 5.12);
                    startSolution = Mapper.GetRandomSolution(interval, Constants.Precission);
                    new HillClimbing().Resolve(rastrigin, interval, Constants.Precission, strategy, startSolution);
                    Console.ReadKey();
                    break;
                case OptimizationFunction.Rosenbrock:
                    var rosenbrock = new Rosenbrock();
                    InitializeConstants("Rosenbrock");
                    interval = new Interval(-2.048, 2.048);
                    startSolution = Mapper.GetRandomSolution(interval, Constants.Precission);
                    new HillClimbing().Resolve(rosenbrock, interval, Constants.Precission, strategy, startSolution);
                    Console.ReadKey();
                    break;
                case OptimizationFunction.SixHumpCamelBack:
                    var sixHumpCamelBack = new SixHumpCamelBack();
                    InitializeConstants("SixHumpCamelBack");

                    interval = new Interval(-3, 3);
                    startSolution = Mapper.GetRandomSolution(interval, Constants.Precission);

                    interval = new Interval(-3, 3);
                    new HillClimbing().Resolve(sixHumpCamelBack,
                                         interval,
                                         Constants.Precission,
                                         strategy, startSolution);
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Invalid number!");
                    break;
            }
        }

        private static void InitializeConstants(string functionName)
        {
            Console.WriteLine("{0}\nNumber of components:", functionName);
            Constants.NumberOfComponentsInSolution = Convert.ToInt32(Console.ReadLine());
        }
    }
}
