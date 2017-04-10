using HomeworkOne.Helpers;
using HomeworkOne.Test;

namespace HomeworkOne
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //afișează când se schimbă sol candidat curent
            HillClimbingTest.GetResult(Strategy.Best, OptimizationFunction.Rastrigin);
            //HillClimbingTest.GetResult(Strategy.Best, OptimizationFunction.Griewangk);
            //HillClimbingTest.GetResult(Strategy.Best, OptimizationFunction.Rosenbrock);
            //HillClimbingTest.GetResult(Strategy.Best, OptimizationFunction.SixHumpCamelBack);

            //valoarea celei mai bune soluții
            //GeneticAlgorithmTest.GetResult(OptimizationFunction.Rastrigin);
            //GeneticAlgorithmTest.GetResult(OptimizationFunction.Griewangk);
            //GeneticAlgorithmTest.GetResult(OptimizationFunction.Rosenbrock);

            HybridizationTest.GetResult(OptimizationFunction.Rastrigin);
            //HybridizationTest.GetResult(OptimizationFunction.Griewangk);
            //HybridizationTest.GetResult(OptimizationFunction.Rosenbrock);
        }
    }
}
