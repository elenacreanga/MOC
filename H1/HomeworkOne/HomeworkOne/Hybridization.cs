using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using HomeworkOne.Helpers;
using HomeworkOne.OptimizationFunctions;

namespace HomeworkOne
{
    public class Hybridization
    {
        public float MutationProbability { private get; set; }
        public float CrossoverProbability { private get; set; }
        public int NumberOfChromosomes { private get; set; }

        private Interval _interval;

        public List<BitArray> GetResult(IOptimizationFunction optimizationFunction, int numberOfIterations,
            Interval interval, int precission)
        {
            var population = GenerateRandomPopulation(interval, precission);
            var chromosomeInfo = InitilizeChromosomesInfoList(population, interval, optimizationFunction);
            var stepsIndex = 0;
            var interationIndex = 0;

            _interval = interval;

            var individualFitnessResults = GetIndividualFitness(population, interval, optimizationFunction);
            var totalFitness = GetTotalFitness(individualFitnessResults);

            while (interationIndex < numberOfIterations && stepsIndex < Constants.NumberOfSteps)
            {
                var currentPopulation = EvolvePopulation(population, totalFitness, individualFitnessResults, optimizationFunction);

                //calculat fitnes pt fiecare si le pun impreuna, si din ele selectez cei mai buni cromozomi
                var chromosomeInfoForCurrentPopulation = InitilizeChromosomesInfoList(currentPopulation, interval,
                    optimizationFunction);
                chromosomeInfo.AddRange(chromosomeInfoForCurrentPopulation);

                //fitness = 1/ valoarea functiei de optimizare (rastrigin,...)
                var bestChromosomesInfoList =
                    chromosomeInfo.OrderByDescending(x => x.Fitness).Take(NumberOfChromosomes).ToList();
                var bestChromosomes = bestChromosomesInfoList.Select(x => x.Chromosomes).ToList();
                Console.WriteLine("Chromosome function value: {0}", (1 / bestChromosomesInfoList.First().Fitness - 0.1));

                individualFitnessResults = GetIndividualFitness(bestChromosomes, interval, optimizationFunction);
                var currentTotalFitness = GetTotalFitness(individualFitnessResults);

                if (currentTotalFitness <= totalFitness)
                {
                    interationIndex++;
                }
                else
                {
                    interationIndex = 0;
                    population = bestChromosomes;
                    totalFitness = currentTotalFitness;
                }
                stepsIndex++;
            }

            return population;
        }

        //extrag cei mai buni chromozomi (cu fitnesul cel mai mare)
        private List<ChromosomeData> InitilizeChromosomesInfoList(List<BitArray> population, Interval interval,
            IOptimizationFunction optimizationFunction)
        {
            var listWithChrmosomeInfo = population.Select(chromosome => new ChromosomeData
            {
                Chromosomes = chromosome,
                Fitness = GetFitness(interval, optimizationFunction, chromosome)
            }).ToList();
            return listWithChrmosomeInfo;
        }

        private List<BitArray> GenerateRandomPopulation(Interval interval, int precission)
        {
            var chromosomes = new List<BitArray>();
            for (var i = 0; i < NumberOfChromosomes; i++)
            {
                var generated = Mapper.GetRandomSolution(interval, precission);
                chromosomes.Add(generated);
            }
            return chromosomes;
        }

        //pt toti cromozomii selectez copiii; ca sa gasesc P ca un cromozom sa fie selectat, fac fitnesul lui / fitnesul total
        private List<BitArray> EvolvePopulation(List<BitArray> population, double totalFitness,
            double[] individualFitness, IOptimizationFunction optimizationFunction)
        {
            var childChromosomes = new List<BitArray>();
            for (var i = 0; i < NumberOfChromosomes; i++)
            {
                var firstChromosome = SelectChromosome(population, totalFitness, individualFitness);
                Thread.Sleep(2);
                var secondChromosome = SelectChromosome(population, totalFitness, individualFitness);
                var childChromosome = ApplyCrossOver(firstChromosome, secondChromosome);
                childChromosome = ApplyMutation(childChromosome);

                var hillClimbingChildChomosome = new HillClimbing().Resolve(optimizationFunction: optimizationFunction,
                                         interval: _interval,
                                         precission: 2,
                                         strategy: Strategy.Best,
                                         startSolution: childChromosome);
                childChromosomes.Add(hillClimbingChildChomosome);
            }
            return childChromosomes;
        }

        private BitArray ApplyMutation(BitArray chromosome)
        {
            var random = new Random();
            for (var i = 0; i < chromosome.Count; i++)
            {
                if (random.NextDouble() <= MutationProbability)
                {
                    chromosome[i] = !chromosome[i];
                }
            }
            return chromosome;
        }

        private BitArray ApplyCrossOver(BitArray chromosomeA, BitArray chromosomeB)
        {
            var child = new BitArray(chromosomeA.Count);
            var rng = new Random();
            for (var j = 0; j < chromosomeA.Count; j++)
            {
                var geneA = chromosomeA.Get(j);
                var geneB = chromosomeB.Get(j);

                var prob = rng.NextDouble();
                child.Set(j, prob < CrossoverProbability ? geneA : geneB);
            }
            return child;
        }

        private BitArray SelectChromosome(List<BitArray> population, double totalFitness, double[] individualFitness)
        {
            var individualSelectionProbabilities = new double[NumberOfChromosomes];
            for (var i = 0; i < NumberOfChromosomes; i++)
            {
                individualSelectionProbabilities[i] = individualFitness[i] / totalFitness;
            }

            var cumulativeSelectionProbabilities = new double[NumberOfChromosomes];
            cumulativeSelectionProbabilities[0] = 0;
            for (var i = 1; i < NumberOfChromosomes; i++)
            {
                cumulativeSelectionProbabilities[i] = cumulativeSelectionProbabilities[i - 1] +
                                                      individualSelectionProbabilities[i];
            }

            var random = new Random();
            for (var i = 0; i < NumberOfChromosomes; i++)
            {
                var r = random.NextDouble();
                for (var j = 0; j < NumberOfChromosomes - 1; j++)
                {
                    if (cumulativeSelectionProbabilities[j] <= r && r <= cumulativeSelectionProbabilities[j + 1])
                    {
                        return new BitArray(population[j]);
                    }
                }
                if (r >= cumulativeSelectionProbabilities[NumberOfChromosomes - 1])
                {
                    return new BitArray(population[population.Count - 1]);
                }
            }

            return null;
        }

        private static double GetTotalFitness(double[] individualFitness)
        {
            return individualFitness.Sum();
        }

        //folosite la selectarea parintilor pt noul cromozom (copil), in raport cu fitnesul total
        private double[] GetIndividualFitness(List<BitArray> population, Interval interval,
            IOptimizationFunction optimizationFunction)
        {
            var individualFitness = new double[NumberOfChromosomes];
            for (var i = 0; i < NumberOfChromosomes; i++)
            {
                var testedChromosome = population[i];
                var fitnessResult = GetFitness(interval, optimizationFunction, testedChromosome);
                individualFitness[i] = fitnessResult;
            }
            return individualFitness;
        }

        private static double GetFitness(Interval interval, IOptimizationFunction optimizationFunction,
            BitArray testedChromosome)
        {
            var realValues = Mapper.GetRealRepresentationOfBits(testedChromosome, interval);
            var fitnessResult = optimizationFunction.Resolve(realValues);
            return fitnessResult;
        }
    }
}
