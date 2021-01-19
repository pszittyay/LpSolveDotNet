using System;

namespace SziCom.LpSolve
{
    public class Variable<T> : AbstractVariable
    {

        public Variable(T valueObject, int index, string name) : base(index, name)
        {
            this.ValueObject = valueObject;
        }
        public Variable(T valueObject, Action<T, double> bindResult, int index, string name) : this(valueObject, index, name)
        {
            this.BindResult = bindResult;
        }

        internal override void SetResult(double result, double from, double till)
        {
            base.SetResult(result, from, till);
            if (BindResult != null) BindResult(ValueObject, result);
        }



        public T ValueObject { get; }
        public Action<T, double> BindResult { get; }

        
    }
}
