using System.Collections.Generic;
using ParticleSwarmOptimization.Helpers;
using ParticleSwarmOptimization.Models;

namespace ParticleSwarmOptimization.Functions
{
    public interface IOptimizationFunction
    {
        double GetErrorTolerance();

        double Resolve(Position position);
        IList<Interval> GetIntervals();
        bool IsFitness { get; set; }
    }
}