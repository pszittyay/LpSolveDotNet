using System;

namespace SziCom.LpSolve
{

    public class Variable<T> : AbstractVariable
    {

        public Variable(T valueObject, int index, string name, bool binary = false) : base(index, name, binary)
        {
            this.ValueObject = valueObject;
        }
        public Variable(T valueObject, Action<T, double> bindResult, int index, string name, bool binary = false) : this(valueObject, index, name, binary)
        {
            this.BindResult = bindResult;
        }

        public Variable(T valueObject, Action<T, double> result, Action<T, double, double> tillFrom, int index, string name, bool binary = false) : this(valueObject, index, name, binary)
        {
            this.BindResult = result;
            this.BindTillFrom = tillFrom;
        }

        public Variable(T valueObject, Action<T, double> result, Action<T, double, double> tillFrom, int index, Func<T, string> name) : this(valueObject, index, name(valueObject))
        {
            this.BindResult = result;
            this.BindTillFrom = tillFrom;

        }

        internal override void SetResult(double result, double from, double till)
        {
            base.SetResult(result, from, till);
            if (BindResult != null) BindResult(ValueObject, result);
            if (BindTillFrom != null) BindTillFrom(ValueObject, from, till);
        }

        public T ValueObject { get; }
        internal Action<T, double> BindResult { get; }
        internal Action<T, double, double> BindTillFrom { get; }


    }
}
