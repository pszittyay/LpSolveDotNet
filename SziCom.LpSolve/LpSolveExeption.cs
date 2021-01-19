using System;

namespace SziCom.LpSolve
{
    public class LpSolveExeption : Exception
    {
        public LpSolveExeption()
        {
        }

        public LpSolveExeption(String message) : base(message)
        {
        }

        public LpSolveExeption(String message, Exception innerException) : base(message, innerException)
        {
        }
    }
}