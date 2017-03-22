namespace HomeworkOne.Helpers
{
    public static class Constants
    {
        public const int NumberOfSteps = 100;

        public static int NumberOfComponentsInSolution { get; set; }

        public static int NrOfBitsInComponent { get; set; }

        public const int Precission = 1;
    }

    public enum Strategy
    {
        First = 1,
        Best
    }

    public enum OptimizationFunction
    {
        Griewangk = 1,
        Rastrigin,
        Rosenbrock,
        SixHumpCamelBack
    }
}
