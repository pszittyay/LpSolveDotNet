namespace SziCom.LpSolve
{
    public class Result
    {
        internal Result(SolutionResult solutionResult, double costoTotal)
        {
            Tipo = solutionResult;
            OptimizationFunctionResult = costoTotal;
        }

        public SolutionResult Tipo { get; private set; }
        public double OptimizationFunctionResult { get; private set; }
    }
}