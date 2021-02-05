using LpSolveDotNet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SziCom.LpSolve
{
    public class Model
    {
        private readonly Dictionary<int, AbstractVariable> Variables = new Dictionary<int, AbstractVariable>();
        public LinearOptimizacionFunction Objetivo { get; private set; }
        public List<Restriccion> Restricciones { get; private set; } = new List<Restriccion>();
        public int RestrictionIndex { get; private set; } = 1;
        public int VariablesIndex { get; private set; }
        private double Scale { get; set; }

        public Variable<T> AddNewVariable<T>(T valueObject, string nombre, Action<T, double> bind) where T : class
        {
            var variable = new Variable<T>(valueObject, bind, ++VariablesIndex, nombre);
            Variables.Add(variable.Index, variable);
            return variable;
        }

        public Variable<T> AddNewVariable<T>(T valueObject, Func<T,string> nombre, Action<T, double> bind) where T : class
        {
            var variable = new Variable<T>(valueObject, bind, ++VariablesIndex, nombre(valueObject));
            Variables.Add(variable.Index, variable);
            return variable;
        }
        public Variable<T> AddNewVariable<T>(T valueObject, Func<T, string> nombre, Action<T, double> result, Action<T,double ,double> tillFrom ) where T : class
        {
            var variable = new Variable<T>(valueObject, result, tillFrom, ++VariablesIndex, nombre);
            Variables.Add(variable.Index, variable);
            return variable;
        }


        public Variable<T> AddNewVariable<T>(T valueObject, string nombre) where T : class
        {
            var variable = new Variable<T>(valueObject, ++VariablesIndex, nombre);
            Variables.Add(variable.Index, variable);
            return variable;
        }

        public IEnumerable<Variable<T>> AddNewVariables<T>(IEnumerable<T> collection, Func<T, string> nameFunction)
        {
            var result = new List<Variable<T>>();
            foreach (var item in collection)
            {
                var variable = new Variable<T>(item, ++VariablesIndex, nameFunction(item));
                Variables.Add(variable.Index, variable);
                result.Add(variable);
            }
            return result;
        }

        public void AddObjetiveFuction(Term term, string nombreRestriccion, LinearOptmizationType funcion)
        {
            this.Objetivo = new LinearOptimizacionFunction(term, nombreRestriccion, funcion);
        }
        public void AddObjetiveFuction(Term term, LinearOptmizationType funcion)
        {
            this.Objetivo = new LinearOptimizacionFunction(term, "OptimizationFunctionName", funcion);
        }

        public void AddRestriction(FinalTerm terms, string nombreRestriccion)
        {
            Restricciones.Add(new Restriccion(terms, nombreRestriccion, ++RestrictionIndex));
        }

        public void AgregarRestriccion(IEnumerable<FinalTerm> terms, string nombreRestriccion)
        {
            foreach (var t in terms)
            {
                Restricciones.Add(new Restriccion(t, nombreRestriccion, ++RestrictionIndex));
            }
        }

        public Term Restar<T>(IEnumerable<Variable<T>> variables)
        {
            return new Term(variables.ToDictionary(v => (AbstractVariable)v, v => -1.0));
        }

        public Result Run(double scale = 1.0)
        {
            LpSolveDotNet.LpSolve.Init();
            this.Scale = scale;

            using (LpSolveDotNet.LpSolve lp = LpSolveDotNet.LpSolve.make_lp(0, VariablesIndex))
            {

                lp.set_scaling(lpsolve_scale_algorithm.SCALE_EXTREME, lpsolve_scale_parameters.SCALE_EQUILIBRATE);

                AddRestrictions(lp, scale);
                RenameLPSolveColumns(lp);
                AddObjetiveFunction(lp, scale);
                ///Importante: El resultado no se escala, porque al escalar los coeficientes de las variables 
                ///y de la fucion objetivo, el resutaldo de la funcion objetivo ya esta correcto.
                var r = new Result(ToLpSolveContraintType(lp.solve()), Math.Round(lp.get_objective() , 2));
                Double[] result = new Double[lp.get_Ncolumns()];
                Double[] from = new Double[lp.get_Ncolumns()];
                Double[] till = new Double[lp.get_Ncolumns()];

                lp.get_variables(result);
                lp.get_sensitivity_obj(from, till);

                foreach (var v in Variables)
                {
                    v.Value.SetResult(result[v.Key - 1] / scale, from[v.Key - 1] / scale, till[v.Key - 1] / scale);
                }
                return r;
            }
        }



        private void RenameLPSolveColumns(LpSolveDotNet.LpSolve lp)
        {
            foreach (var v in Variables)
            {
                lp.set_col_name(v.Key, v.Value.Name);
            }
        }

        public Term Sum<T>(IEnumerable<Variable<T>> variables)
        {
            return new Term(variables.ToDictionary(v => (AbstractVariable)v, v => 1.0));
        }

        public Term SumWhere<T>(Func<Variable<T>, bool> conditional, IEnumerable<Variable<T>> variables)
        {
            return new Term(variables.Where(conditional).ToDictionary(v => (AbstractVariable)v, v => 1.0));
        }
        private void AddObjetiveFunction(LpSolveDotNet.LpSolve lp, double scale)
        {
            Int32[] variables = Objetivo.FunctionTerm.GetVariables();
            Double[] coeficientes = Objetivo.FunctionTerm.GetCoeficientes().Select(c=>c/scale).ToArray();

            if (!lp.set_obj_fnex(Objetivo.FunctionTerm.Count, coeficientes, variables))
            {
                throw new LpSolveExeption($"Restricciones Factory: Error al agregar la restriccion: {Objetivo.Name}");
            }
            if (Objetivo.Tipo == LinearOptmizationType.Maximizar)
            {
                lp.set_maxim();
            }

            if (Objetivo.Tipo == LinearOptmizationType.Minimizar) lp.set_minim();
        }

        private void AddRestrictions(LpSolveDotNet.LpSolve lp, double scale)
        {
            foreach (var r in Restricciones)
            {
                Int32[] variables = r.Termino.GetVariables();
                Double[] coeficientes = r.Termino.GetCoeficientes().Select(t=>t / scale).ToArray();

                if (!lp.add_constraintex(r.Termino.Count, coeficientes, variables, ToLpSolveContraintType(r.Termino.Restriction), r.Termino.RestrictionValue))
                {
                    throw new LpSolveExeption($"Restricciones Factory: Error al agregar la restriccion: {r.Nombre}");
                }

                lp.set_row_name(r.Indice, r.Nombre);
            }
        }

        private lpsolve_constr_types ToLpSolveContraintType(RestrictionType restriccion)
        {
            switch (restriccion)
            {
                case RestrictionType.Equals:
                    return lpsolve_constr_types.EQ;

                case RestrictionType.GreaterOrEqualThan:
                    return lpsolve_constr_types.GE;

                case RestrictionType.LessOrEqualThan:
                    return lpsolve_constr_types.LE;
            }

            throw new InvalidOperationException();
        }

        private SolutionResult ToLpSolveContraintType(lpsolve_return tipo)
        {
            switch (tipo)
            {
                case lpsolve_return.UNKNOWNERROR: return SolutionResult.UNKNOWNERROR;
                case lpsolve_return.DATAIGNORED: return SolutionResult.DATAIGNORED;
                case lpsolve_return.NOBFP: return SolutionResult.NOBFP;
                case lpsolve_return.NOMEMORY: return SolutionResult.NOMEMORY;
                case lpsolve_return.NOTRUN: return SolutionResult.NOTRUN;
                case lpsolve_return.OPTIMAL: return SolutionResult.OPTIMAL;
                case lpsolve_return.SUBOPTIMAL: return SolutionResult.SUBOPTIMAL;
                case lpsolve_return.INFEASIBLE: return SolutionResult.INFEASIBLE;
                case lpsolve_return.UNBOUNDED: return SolutionResult.UNBOUNDED;
                case lpsolve_return.DEGENERATE: return SolutionResult.DEGENERATE;
                case lpsolve_return.NUMFAILURE: return SolutionResult.NUMFAILURE;
                case lpsolve_return.USERABORT: return SolutionResult.USERABORT;
                case lpsolve_return.TIMEOUT: return SolutionResult.TIMEOUT;
                case lpsolve_return.PRESOLVED: return SolutionResult.PRESOLVED;
            }
            throw new InvalidOperationException();
        }
    }
}