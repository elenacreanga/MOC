namespace ParticleSwarmOptimization.Models
{
    public class Interval
    {
        public double UpperBound { get; private set; }
        public double LowerBound { get; private set; }

        public Interval(double loweBound, double upperBound)
        {
            LowerBound = loweBound;
            UpperBound = upperBound;
        }
    }
}
