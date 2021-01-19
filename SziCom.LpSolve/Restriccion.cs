namespace SziCom.LpSolve
{
    public class Restriccion
    {
        public FinalTerm Termino { get; private set; }
        public string Nombre { get; private set; }

        public int Indice { get; private set; }

        public Restriccion(FinalTerm termino, string nombre, int indice)
        {
            this.Termino = termino;
            this.Nombre = nombre;
            this.Indice = indice;
        }
    }
}