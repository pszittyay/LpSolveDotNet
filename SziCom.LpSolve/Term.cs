using System;
using System.Collections.Generic;
using System.Linq;

namespace SziCom.LpSolve
{
    public class Term : Dictionary<AbstractVariable, double>
    {
        internal Term(AbstractVariable v)
        {
            Add(v, 1);
        }

        internal Term(Dictionary<AbstractVariable, double> a)
        {
            foreach (var item in a)
            {
                Add(item.Key, item.Value);
            }
        }

        internal Term(Dictionary<AbstractVariable, double> a, Dictionary<AbstractVariable, double> b)
        {
            foreach (var itemA in a)
            {
                Add(itemA.Key, itemA.Value);
            }

            foreach (var itemB in b)
            {
                Add(itemB.Key, itemB.Value);
            }
        }

        public static Term operator +(Term a, Term b)
        {
            return new Term(a, b);
        }

        public static Term operator -(Term a, Term b)
        {
            return new Term(a, b.ToDictionary(k => k.Key, v => v.Value * -1));
        }

        public static Term operator *(Term a, double coeficiente)
        {
            return new Term(a.ToDictionary(k => k.Key, v => v.Value * coeficiente));
        }

        public static Term operator *(double coeficiente, Term a)
        {
            return new Term(a.ToDictionary(k => k.Key, v => v.Value * coeficiente));
        }

        public static Term operator /(Term a, double coeficiente)
        {
            return new Term(a.ToDictionary(k => k.Key, v => v.Value / coeficiente));
        }

        public static Term operator /(double coeficiente, Term a)
        {
            return new Term(a.ToDictionary(k => k.Key, v => v.Value / coeficiente));
        }

        public static FinalTerm operator >=(Term a, double valor)
        {
            return new FinalTerm(a, RestrictionType.GreaterOrEqualThan, valor);
        }

        public static FinalTerm operator <=(Term a, double valor)
        {
            return new FinalTerm(a, RestrictionType.LessOrEqualThan, valor);
        }

        public static FinalTerm operator ==(Term a, double valor)
        {
            return new FinalTerm(a, RestrictionType.Equals, valor);
        }

        public static FinalTerm operator !=(Term a, double valor)
        {
            throw new NotImplementedException();
        }

        public static FinalTerm operator >=(Term a, int valor)
        {
            return new FinalTerm(a, RestrictionType.GreaterOrEqualThan, valor);
        }

        public static FinalTerm operator <=(Term a, int valor)
        {
            return new FinalTerm(a, RestrictionType.LessOrEqualThan, valor);
        }

        public static FinalTerm operator ==(Term a, int valor)
        {
            return new FinalTerm(a, RestrictionType.Equals, valor);
        }

        public static FinalTerm operator !=(Term a, int valor)
        {
            throw new NotImplementedException();
        }

        //public static implicit operator Term(AbstractVariable v)
        //{
        //    return new Term(v);
        //}
    }
}