using System;
using System.Collections.Generic;
using System.Linq;

namespace SziCom.LpSolve
{

    public class Term
    {
        private readonly Dictionary<AbstractVariable, InternalFactor> innerDictionary = new Dictionary<AbstractVariable, InternalFactor>();
        private double Adding;
        internal Dictionary<AbstractVariable, InternalFactor> GetDictionary() => this.innerDictionary;
        internal Term(AbstractVariable v)
        {
            innerDictionary.Add(v, new InternalFactor());
        }
        public Term()
        {
            Adding = 0;
        }
        internal Term(Term a)
        {
            Adding = a.Adding;
            this.innerDictionary = a.GetDictionary();
        }
        internal Term(Dictionary<AbstractVariable, InternalFactor> a)
        {
            foreach (var item in a)
            {
                innerDictionary.Add(item.Key, item.Value);
            }
        }

        internal Term(Dictionary<AbstractVariable, InternalFactor> a, Dictionary<AbstractVariable, InternalFactor> b)
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
        public static Term operator +(Term a, double b)
        {
            a.Adding += b;
            return a;
        }
        public static Term operator +(double a, Term b)
        {
            return b + a;
        }
        public static Term operator +(Term a, int b)
        {
            a.Adding += b;
            return a;
        }
        public static Term operator +(int a, Term b)
        {
            return b + a;
        }
        public static Term operator -(Term a, Term b)
        {
            return new Term(a.GetDictionary(), b.GetDictionary().ToDictionary(k => k.Key, v => new InternalFactor(-1)));
        }
        public static Term operator -(Term a, double b)
        {
            a.Adding -= b;
            return a;
        }
        public static Term operator -(double a, Term b)
        {
            return b + a;
        }
        public static Term operator -(Term a, int b)
        {
            a.Adding -= b;
            return a;
        }
        public static Term operator -(int a, Term b)
        {
            return b + a;
        }
        public static Term operator *(Term a, double coeficiente)
        {
            return new Term(a.GetDictionary().ToDictionary(k => k.Key, v => new InternalFactor(v.Value.Factor * coeficiente)));
        }

        public static Term operator *(double coeficiente, Term a)
        {
            return new Term(a.GetDictionary().ToDictionary(k => k.Key, v => new InternalFactor(v.Value.Factor * coeficiente)));
        }

        public static Term operator /(Term a, double coeficiente)
        {
            return new Term(a.GetDictionary().ToDictionary(k => k.Key, v => new InternalFactor(v.Value.Factor / coeficiente)));
        }

        public static Term operator /(double coeficiente, Term a)
        {
            return new Term(a.GetDictionary().ToDictionary(k => k.Key, v => new InternalFactor(v.Value.Factor / coeficiente)));
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
        public static FinalTerm operator <=(Term a, Term b)
        {
            return new FinalTerm(a - b, RestrictionType.LessOrEqualThan, 0);
        }
        public static FinalTerm operator >=(Term a, Term b)
        {
            return new FinalTerm(a - b, RestrictionType.GreaterOrEqualThan, 0);
        }
        public static FinalTerm operator ==(Term a, int valor)
        {
            return new FinalTerm(a, RestrictionType.Equals, valor);
        }
        public static FinalTerm operator ==(Term a, Term b)
        {
            return new FinalTerm(a - b, RestrictionType.Equals, 0);
        }
        public static FinalTerm operator !=(Term a, Term b)
        {
            throw new NotImplementedException();
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
            return innerDictionary.Values.Select(t => t.Factor).ToArray();
        }
        internal Double GetAdding()
        {
            return Adding;
        }

        internal Int32 Count => innerDictionary.Count;

        internal double GetFactor(AbstractVariable var) => innerDictionary[var].Factor;
    }
}