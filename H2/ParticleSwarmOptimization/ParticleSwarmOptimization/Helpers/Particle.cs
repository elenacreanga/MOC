using ParticleSwarmOptimization.Functions;

namespace ParticleSwarmOptimization.Helpers
{
    public class Particle
    {
        public double Fitness { get; set; }
        public Speed Speed { get; set; }
        public Position Position { get; set; }
        public IOptimizationFunction Function { get; set; }

       public Particle(IOptimizationFunction function)
        {
            Function = function;
        }

        public Particle(double fitness, Speed speed, Position position,
            IOptimizationFunction function)
        {
            Fitness = fitness;
            Speed = speed;
            Position = position;
            Function = function;
        }

        public double GetFitness()
        {
            Fitness = Function.Resolve(Position);
            return Fitness;
        }
    }
}
