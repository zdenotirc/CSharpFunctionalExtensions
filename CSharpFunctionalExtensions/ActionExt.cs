using System;

using static CSharpFunctionalExtensions.F;
using Unit = System.ValueTuple;

namespace CSharpFunctionalExtensions
{
    public static class ActionExt
    {
        public static Func<Unit> ToFunc(this Action action)
            => () => { action(); return Unit(); };

        public static Func<T, Unit> ToFunc<T>(this Action<T> action)
            => t => { action(t); return Unit(); };
    }
}