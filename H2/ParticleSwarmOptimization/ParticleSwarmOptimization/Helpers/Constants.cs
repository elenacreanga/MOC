namespace ParticleSwarmOptimization.Helpers
{
    public static class Constants
    {
        public const int SWARM_SIZE = 50;
        public const int MAX_ITERATION = 300;
        public const double W1_UPPER = 1.0;
        public const double W1_LOWER = 0.0;
        public const double W2 = 1.4;
        public const double W3 = 1.4;

        public const int PROBLEM_DIMENSION = 30;
        public static readonly double[] LOW = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        public static readonly double[] HIGH = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static readonly double[] SPEED = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };


        /*	int PROBLEM_DIMENSION = 2;
            double[] LOW = { 1, -1};
            double[] HIGH = { 4, 1 };
            double[] SPEED = { -1, 1 };*/
    }
}
