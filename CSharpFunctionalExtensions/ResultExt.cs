using System;

namespace CSharpFunctionalExtensions
{
    public static class ResultExt
    {
        public static Result<TResult, TError> OnSuccess<TValue, TResult, TError>(
            this Result<TValue, TError> result,
            Func<TValue, TResult> func) where TError : class
        {
            return result.IsFailure 
                ? Result.Fail<TResult, TError>(result.Error)
                : Result.Ok<TResult, TError>(func(result.Value));
        }

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

        public static Result<TResult, TError> OnSuccess<TValue, TResult, TError>(
            this Result<TValue, TError> result,
            Func<TValue, Result<TResult, TError>> func) where TError : class
        {
            return result.IsFailure 
                ? Result.Fail<TResult, TError>(result.Error) 
                : func(result.Value);
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

        public static Result<TResult, TError> OnSuccess<TValue, TResult, TError>(
            this Result<TValue, TError> result,
            Func<Result<TResult, TError>> func) where TError : class
        {
            return result.IsFailure 
                ? Result.Fail<TResult, TError>(result.Error) 
                : func();
        }

        public static Result<TResult> OnSuccess<T, TResult>(
            this Result<T> result, Func<Result<TResult>> func)
        {
            return result.IsFailure 
                ? Result.Fail<TResult>(result.Error) 
                : func();
        }

        public static Result<TResult> OnSuccess<TValue, TResult, TError>(
            this Result<TValue, TError> result,
            Func<TValue, Result<TResult>> func) where TError : class
        {
            return result.IsFailure 
                ? Result.Fail<TResult, TError>(result.Error) 
                : func(result.Value);
        }

        public static Result OnSuccess<TValue, TResult, TError>(
            this Result<TValue, TError> result,
            Func<TValue, Result> func) where TError : class
        {
            return result.IsFailure 
                ? Result.Fail<TResult, TError>(result.Error) 
                : func(result.Value);
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

        public static Result<TValue, TError> Ensure<TValue, TError>(
            this Result<TValue, TError> result,
            Func<TValue, bool> predicate, TError errorObject) where TError : class
        {
            if (result.IsFailure)
            {
                return Result.Fail<TValue, TError>(result.Error);
            }

            return !predicate(result.Value) 
                ? Result.Fail<TValue, TError>(errorObject) 
                : Result.Ok<TValue, TError>(result.Value);
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

        public static Result<TResult, TError> Map<TValue, TResult, TError>(
            this Result<TValue, TError> result,
            Func<TValue, TResult> func) where TError : class
        {
            return result.IsFailure 
                ? Result.Fail<TResult, TError>(result.Error) 
                : Result.Ok<TResult, TError>(func(result.Value));
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

        public static Result<TValue, TError> OnSuccess<TValue, TError>(
            this Result<TValue, TError> result,
            Action<TValue> action) where TError : class
        {
            if (result.IsSuccess)
            {
                action(result.Value);
            }

            return result;
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

        public static TValue OnBoth<TValue, TError>(
            this Result<TValue, TError> result,
            Func<Result<TValue, TError>, TValue> func) where TError : class
        {
            return func(result);
        }

        public static Result<TValue, TError> OnFailure<TValue, TError>(
            this Result<TValue, TError> result,
            Action action) where TError : class
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
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

        public static Result<TValue, TError> OnFailure<TValue, TError>(
            this Result<TValue, TError> result,
            Action<TError> action) where TError : class
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }

        public static Result<T> OnFailure<T>(this Result<T> result, Action<string> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }

        public static Result OnFailure(this Result result, Action<string> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }
    }
}
