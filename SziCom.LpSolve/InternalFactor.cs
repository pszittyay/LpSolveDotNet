namespace SziCom.LpSolve
{
    public class InternalFactor
    {
        public InternalFactor()
        {
            this.Factor = 1;
        }
        public InternalFactor(double factor)
        {
            this.Factor = factor;
        
        }

        public double Factor { get; private set; }
        
    }
}