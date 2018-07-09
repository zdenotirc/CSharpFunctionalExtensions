using System;
using static CSharpFunctionalExtensions.F;

namespace CSharpFunctionalExtensions
{
    public static class OptionExt
    {
        public static Result<T> ToResult<T>(this Option<T> option, Error error)
        {
            return option.IsNone 
                ? Failure(error) 
                : Success(option.Value);
        }
        
        public static Result<T> ToResult<T>(this Option<T> opt, Func<Error> error)
        {
            return opt.Match(
                () => Failure(error()),
                (t) => Success(t));
        }

        public static T Unwrap<T>(this Option<T> option, T defaultValue = default(T))
        {
            return option.Unwrap(x => x, defaultValue);
        }

        public static TResult Unwrap<T, TResult>(
            this Option<T> option, Func<T, TResult> selector, TResult defaultValue = default(TResult))
        {
            return option.IsSome 
                ? selector(option.Value)
                : defaultValue;
        }

        public static Option<T> Where<T>(this Option<T> option, Func<T, bool> predicate)
        {
            if (option.IsNone)
            {
                return default(T);
            }

            return predicate(option.Value) ? option : default(T);
        }

        public static Option<TResult> Select<T, TResult>(this Option<T> option, Func<T, TResult> selector)
        {
            return option.IsNone ? default(TResult) : selector(option.Value);
        }

        public static Option<TResult> Select<T, TResult>(this Option<T> option, Func<T, Option<TResult>> selector)
        {
            return option.IsNone ? default(TResult) : selector(option.Value);
        }

        public static void Execute<T>(this Option<T> option, Action<T> action)
        {
            if (option.IsNone)
            {
                return;
            }

            action(option.Value);
        }
    }
}
