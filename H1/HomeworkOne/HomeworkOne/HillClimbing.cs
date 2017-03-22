using System;
using System.Collections;
using HomeworkOne.Helpers;
using HomeworkOne.OptimizationFunctions;

namespace HomeworkOne
{
    public class HillClimbing
    {
        public BitArray Resolve(IOptimizationFunction optimizationFunction,
            Interval interval,
            int precission,
            Strategy strategy,
            BitArray startSolution)
        {
            var bestSolution = startSolution;
            var bestSolutionRealRepresentation = Mapper.GetRealRepresentationOfBits(bestSolution, interval);
            var bestFunctionValue = optimizationFunction.Resolve(bestSolutionRealRepresentation);

            Console.WriteLine("Best value to start: {0}", bestFunctionValue);
            var newInterval = interval;
            for (var i = 0; i < Constants.NumberOfSteps; i++)
            {
                var localBestSolution = bestSolution;
                var localBestFunctionValue = bestFunctionValue;

                for (var j = 0; j < Constants.NumberOfComponentsInSolution * Constants.NrOfBitsInComponent; j++)
                {
                    var potentialSolution = Mapper.AlterSolution(localBestSolution, j);

                    var potentialSolutionRealRepresentation = Mapper.GetRealRepresentationOfBits(potentialSolution, newInterval);
                    var functionValue = optimizationFunction.Resolve(potentialSolutionRealRepresentation);

                    if (functionValue < localBestFunctionValue)
                    {
                        localBestFunctionValue = functionValue;
                        localBestSolution = potentialSolution;

                        Console.WriteLine("Local optimizationFunction value: " + localBestFunctionValue);

                        if (strategy == Strategy.First)
                            break;
                    }
                }

                if (localBestFunctionValue < bestFunctionValue)
                {
                    bestFunctionValue = localBestFunctionValue;
                    bestSolution = localBestSolution;
                }
            }

            Console.WriteLine("Best value using Hill Climbing: " + bestFunctionValue);
            Console.WriteLine("Best solution:");
            Mapper.PrintBitArray(bestSolution);
            return bestSolution;
        }
    }
}
