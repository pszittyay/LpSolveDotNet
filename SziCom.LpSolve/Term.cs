using System;
using System.Collections.Generic;
using System.Linq;

namespace SziCom.LpSolve
{
    public class Term
    {
        private Dictionary<AbstractVariable, double> innerDictionary = new Dictionary<AbstractVariable, double>();

        internal Dictionary<AbstractVariable, double> GetDictionary() => this.innerDictionary;
        internal Term(AbstractVariable v)
        {
            innerDictionary.Add(v, 1);
        }
        public  Term()
        {
            
        }
        internal Term(Term a)
        {
            this.innerDictionary = a.GetDictionary();
        }
        internal Term(Dictionary<AbstractVariable, double> a)
        {
            foreach (var item in a)
            {
                innerDictionary.Add(item.Key, item.Value);
            }
        }

        internal Term(Dictionary<AbstractVariable, double> a, Dictionary<AbstractVariable, double> b)
        {
            foreach (var itemA in a)
            {
                innerDictionary.Add(itemA.Key, itemA.Value);
            }

            foreach (var itemB in b)
            {
                innerDictionary.Add(itemB.Key, itemB.Value);
            }
        }

        public static Term operator +(Term a, Term b)
        {
            return new Term(a.GetDictionary(), b.GetDictionary());
        }

        public static Term operator -(Term a, Term b)
        {
            return new Term(a.GetDictionary(), b.GetDictionary().ToDictionary(k => k.Key, v => v.Value * -1));
        }

        public static Term operator *(Term a, double coeficiente)
        {
            return new Term(a.GetDictionary().ToDictionary(k => k.Key, v => v.Value * coeficiente));
        }

        public static Term operator *(double coeficiente, Term a)
        {
            return new Term(a.GetDictionary().ToDictionary(k => k.Key, v => v.Value * coeficiente));
        }

        public static Term operator /(Term a, double coeficiente)
        {
            return new Term(a.GetDictionary().ToDictionary(k => k.Key, v => v.Value / coeficiente));
        }

        public static Term operator /(double coeficiente, Term a)
        {
            return new Term(a.GetDictionary().ToDictionary(k => k.Key, v => v.Value / coeficiente));
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

        internal Int32[] GetVariables()
        {
            return innerDictionary.Keys.Select(k => k.Index).ToArray();
        }
        internal Double[] GetCoeficientes()
        {
            return innerDictionary.Values.ToArray();
        }

        internal Int32 Count => innerDictionary.Count;

        internal double GetFactor(AbstractVariable var) => innerDictionary[var];
    }
}