namespace HomeworkOne.OptimizationFunctions
{
    public interface IOptimizationFunction
    {
        double Resolve(double[] x, double[] y = null);
    }
}
