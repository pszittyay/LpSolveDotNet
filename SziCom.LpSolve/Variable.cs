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

        public Variable(T valueObject, Action<T, double> result, Action<T, double, double> tillFrom, int index, string name) : this(valueObject, index, name)
        {
            this.BindResult= result;
            this.BindTillFrom = tillFrom;
        }

        public Variable(T valueObject, Action<T, double> result, Action<T, double, double> tillFrom, int index, Func<T,string> name) : this(valueObject, index,name(valueObject))
        {
            this.BindResult = result;
            this.BindTillFrom = tillFrom;
        
        }

        internal override void SetResult(double result, double from, double till)
        {
            base.SetResult(result, from, till);
            if (BindResult != null) BindResult(ValueObject, result);
            if (BindTillFrom != null) BindTillFrom(ValueObject,  from, till);
        }



        public T ValueObject { get; }
        internal Action<T, double> BindResult { get; }
        internal Action<T, double, double > BindTillFrom { get; }


    }
}
