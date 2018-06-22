using System;

namespace CSharpFunctionalExtensions
{
    public static class OptionExt
    {
        public static Result<T> ToResult<T>(this Option<T> option, string errorMessage)
        {
            return option.HasNoValue 
                ? Result.Fail<T>(errorMessage) 
                : Result.Ok(option.Value);
        }

        public static T Unwrap<T>(this Option<T> option, T defaultValue = default(T))
        {
            return option.Unwrap(x => x, defaultValue);
        }

        public static TResult Unwrap<T, TResult>(
            this Option<T> option, Func<T, TResult> selector, TResult defaultValue = default(TResult))
        {
            return option.HasValue 
                ? selector(option.Value)
                : defaultValue;
        }

        public static Option<T> Where<T>(this Option<T> option, Func<T, bool> predicate)
        {
            if (option.HasNoValue)
            {
                return default(T);
            }

            return predicate(option.Value) ? option : default(T);
        }

        public static Option<TResult> Select<T, TResult>(this Option<T> option, Func<T, TResult> selector)
        {
            return option.HasNoValue ? default(TResult) : selector(option.Value);
        }

        public static Option<TResult> Select<T, TResult>(this Option<T> option, Func<T, Option<TResult>> selector)
        {
            return option.HasNoValue ? default(TResult) : selector(option.Value);
        }

        public static void Execute<T>(this Option<T> option, Action<T> action)
        {
            if (option.HasNoValue)
            {
                return;
            }

            action(option.Value);
        }
    }
}
