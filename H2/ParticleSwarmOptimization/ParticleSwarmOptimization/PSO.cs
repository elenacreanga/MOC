using System;
using System.Collections.Generic;
using ParticleSwarmOptimization.Functions;
using ParticleSwarmOptimization.Helpers;

namespace ParticleSwarmOptimization
{
    public class PSO
    {
        private IList<Particle> swarm = new List<Particle>();
        private double[] p = new double[Constants.SWARM_SIZE];
        private IList<Position> pPosition = new List<Position>();
        private double g;
        private Position gPosition;
        private double[] fitnessList = new double[Constants.SWARM_SIZE];

        private readonly Random _random = new Random();

        public void Execute(IOptimizationFunction function)
        {
            InitializeSwarm(function);
            UpdateFitnessList();

            for (var i = 0; i < Constants.SWARM_SIZE; i++)
            {
                p[i] = fitnessList[i];
                pPosition.Add(swarm[i].Position);
            }

            var t = 0;
            double err = 9999;

            while (t < Constants.MAX_ITERATION && err > function.GetErrorTolerance())
            {
                for (var i = 0; i < Constants.SWARM_SIZE; i++)
                {
                    if (fitnessList[i] < p[i])
                    {
                        p[i] = fitnessList[i];
                        pPosition[i] = swarm[i].Position;
                    }
                }

                var bestParticleIndex = GetMinPos(fitnessList);
                if (t == 0 || fitnessList[bestParticleIndex] < g)
                {
                    g = fitnessList[bestParticleIndex];
                    gPosition = swarm[bestParticleIndex].Position;
                }

                var W1 = Constants.W1_UPPER
                            - (((double)t) / Constants.MAX_ITERATION)
                            * (Constants.W1_UPPER - Constants.W1_LOWER);
                //System.out.println("W1="+W1);

                for (var i = 0; i < Constants.SWARM_SIZE; i++)
                {
                    var r1 = _random.NextDouble();
                    var r2 = _random.NextDouble();

                    var particle = swarm[i];

                    double[] newSpeed = new double[Constants.PROBLEM_DIMENSION];
                    for (var dim = 0; dim < Constants.PROBLEM_DIMENSION; dim++)
                    {
                        newSpeed[dim] = (W1 * particle.Speed.Values[dim]) 
                                        + (r1 * Constants.W2)
                                        * (pPosition[i].Values[dim] - particle.Position.Values[dim])
                                        + (r2 * Constants.W3)
                                        * (gPosition.Values[dim] - particle.Position.Values[dim]);
                    }
                    var speed = new Speed(newSpeed);
                    particle.Speed = speed;

                    var newPosition = new double[Constants.PROBLEM_DIMENSION];
                    for (var dim = 0; dim < Constants.PROBLEM_DIMENSION; dim++)
                    {
                        newPosition[dim] = particle.Position.Values[dim] + newSpeed[dim];
                    }
                    var loc = new Position(newPosition);
                    particle.Position = loc;
                }

                err = function.Resolve(gPosition);
                //if (function.IsFitness)
                //{
                //    err = 1 / err - 0.1;
                //}
                Console.WriteLine("Iteration " + t + ", solution: "
                                   + function.Resolve(gPosition));

                t++;
                UpdateFitnessList();
            }
        }

        public void InitializeSwarm(IOptimizationFunction function)
        {
            for (var i = 0; i < Constants.SWARM_SIZE; i++)
            {
                var particle = new Particle(function);

                var loc = new double[Constants.PROBLEM_DIMENSION];
                for (var dim = 0; dim < Constants.PROBLEM_DIMENSION; dim++)
                {
                    var interval = function.GetIntervals()[dim];
                    loc[dim] = interval.LowerBound + _random.NextDouble()
                               * (interval.UpperBound - interval.LowerBound);
                }

                var position = new Position(loc);
                var speed = new Speed(Constants.SPEED);

                particle.Position = position;
                particle.Speed = speed;
                swarm.Add(particle);
            }
        }

        public void UpdateFitnessList()
        {
            for (var i = 0; i < Constants.SWARM_SIZE; i++)
            {
                fitnessList[i] = swarm[i].GetFitness();
            }
        }

        private static int GetMinPos(double[] list)
        {
            var pos = 0;
            var minValue = list[0];

            for (var i = 0; i < list.Length; i++)
            {
                if (list[i] < minValue)
                {
                    pos = i;
                    minValue = list[i];
                }
            }

            return pos;
        }
    }
}
