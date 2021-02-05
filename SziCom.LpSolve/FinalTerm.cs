using System.Collections.Generic;

namespace SziCom.LpSolve
{
    public class FinalTerm : Term
    {
        internal RestrictionType Restriction { get; private set; }
        internal double RestrictionValue { get; private set; }

        internal FinalTerm(Term finalTerm, RestrictionType restriction, double restrictionValue) : base(finalTerm)
        {
            Restriction = restriction;
            RestrictionValue = restrictionValue;
        }
    }
}