using System;

namespace CSharpFunctionalExtensions
{
    public static class ResultExt
    {
        public static Result<TResult> OnSuccess<T, TResult>(this Result<T> result, Func<T, TResult> func)
        {
            return result.IsFailure 
                ? Result.Fail<TResult>(result.Error) 
                : Result.Ok(func(result.Value));
        }

        public static Result<T> OnSuccess<T>(this Result result, Func<T> func)
        {
            return result.IsFailure 
                ? Result.Fail<T>(result.Error) 
                : Result.Ok(func());
        }

        public static Result<TResult> OnSuccess<T, TResult>(
            this Result<T> result, Func<T, Result<TResult>> func)
        {
            return result.IsFailure 
                ? Result.Fail<TResult>(result.Error) 
                : func(result.Value);
        }

        public static Result<T> OnSuccess<T>(this Result result, Func<Result<T>> func)
        {
            return result.IsFailure 
                ? Result.Fail<T>(result.Error) 
                : func();
        }

        public static Result<TResult> OnSuccess<T, TResult>(
            this Result<T> result, Func<Result<TResult>> func)
        {
            return result.IsFailure 
                ? Result.Fail<TResult>(result.Error) 
                : func();
        }

        public static Result OnSuccess<T>(this Result<T> result, Func<T, Result> func)
        {
            return result.IsFailure 
                ? Result.Fail(result.Error) 
                : func(result.Value);
        }

        public static Result OnSuccess(this Result result, Func<Result> func)
        {
            return result.IsFailure ? result : func();
        }

        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, string errorMessage)
        {
            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error);
            }

            return !predicate(result.Value) 
                ? Result.Fail<T>(errorMessage) 
                : Result.Ok(result.Value);
        }

        public static Result Ensure(this Result result, Func<bool> predicate, string errorMessage)
        {
            if (result.IsFailure)
            {
                return Result.Fail(result.Error);
            }

            return !predicate() 
                ? Result.Fail(errorMessage) 
                : Result.Ok();
        }

        public static Result<TResult> Map<T, TResult>(this Result<T> result, Func<T, TResult> func)
        {
            return result.IsFailure 
                ? Result.Fail<TResult>(result.Error) 
                : Result.Ok(func(result.Value));
        }

        public static Result<T> Map<T>(this Result result, Func<T> func)
        {
            return result.IsFailure 
                ? Result.Fail<T>(result.Error) 
                : Result.Ok(func());
        }

        public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            if (result.IsSuccess)
            {
                action(result.Value);
            }

            return result;
        }

        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsSuccess)
            {
                action();
            }

            return result;
        }

        public static T OnBoth<T>(this Result result, Func<Result, T> func)
        {
            return func(result);
        }

        public static TResult OnBoth<T, TResult>(
            this Result<T> result, Func<Result<T>, TResult> func)
        {
            return func(result);
        }

        public static Result<T> OnFailure<T>(this Result<T> result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        public static Result OnFailure(this Result result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        public static Result<T> OnFailure<T>(this Result<T> result, Action<Error> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }

        public static Result OnFailure(this Result result, Action<Error> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }
    }
}
