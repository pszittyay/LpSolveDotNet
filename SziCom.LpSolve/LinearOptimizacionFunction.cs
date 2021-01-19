namespace SziCom.LpSolve
{
    public class LinearOptimizacionFunction
    {
        public Term FunctionTerm { get; private set; }
        public string Name { get; private set; }

        public LinearOptmizationType Tipo { get; private set; }

        internal LinearOptimizacionFunction(Term termino, string nombre, LinearOptmizationType tipo)
        {
            this.FunctionTerm = termino;
            this.Name = nombre;
            this.Tipo = tipo;
        }
    }
}