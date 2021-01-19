namespace SziCom.LpSolve
{
    public enum LinearOptmizationType
    {
        Minimizar,
        Maximizar,
    }

    public enum SolutionResult
    {
        //
        // Resumen:
        //     Undefined internal error
        UNKNOWNERROR = -5,

        //
        // Resumen:
        //     Invalid input data provided
        DATAIGNORED = -4,

        //
        // Resumen:
        //     No basis factorization package
        NOBFP = -3,

        //
        // Resumen:
        //     Out of memory
        NOMEMORY = -2,

        //
        // Resumen:
        //     Solver has not run, usually because of an empty model.
        NOTRUN = -1,

        //
        // Resumen:
        //     An optimal solution was obtained
        OPTIMAL = 0,

        //
        // Resumen:
        //     The model is sub-optimal. Only happens if there are integer variables and there
        //     is already an integer solution found. The solution is not guaranteed the most
        //     optimal one.
        //     A timeout occured (set via set_timeout or with the -timeout option in lp_solve)
        //     set_break_at_first was called so that the first found integer solution is found
        //     (-f option in lp_solve)
        //     set_break_at_value was called so that when integer solution is found that is
        //     better than the specified value that it stops (-o option in lp_solve)
        //     set_mip_gap was called (-g/-ga/-gr options in lp_solve) to specify a MIP gap
        //     An abort callback is installed (LpSolveDotNet.LpSolve.put_abortfunc(LpSolveDotNet.ctrlcfunc,System.IntPtr))
        //     and this callback returned true
        //     At some point not enough memory could not be allocated
        SUBOPTIMAL = 1,

        //
        // Resumen:
        //     The model is infeasible
        INFEASIBLE = 2,

        //
        // Resumen:
        //     The model is unbounded
        UNBOUNDED = 3,

        //
        // Resumen:
        //     The model is degenerative
        DEGENERATE = 4,

        //
        // Resumen:
        //     Numerical failure encountered
        NUMFAILURE = 5,

        //
        // Resumen:
        //     The abort callback returned true. LpSolveDotNet.LpSolve.put_abortfunc(LpSolveDotNet.ctrlcfunc,System.IntPtr)
        USERABORT = 6,

        //
        // Resumen:
        //     A timeout occurred. A timeout was set via LpSolveDotNet.LpSolve.set_timeout(System.Int32)
        TIMEOUT = 7,

        //
        // Resumen:
        //     The model could be solved by presolve. This can only happen if presolve is active
        //     via LpSolveDotNet.LpSolve.set_presolve(LpSolveDotNet.lpsolve_presolve,System.Int32)
        PRESOLVED = 9,

        //
        // Resumen:
        //     Accuracy error encountered
        ACCURACYERROR = 25
    }
}