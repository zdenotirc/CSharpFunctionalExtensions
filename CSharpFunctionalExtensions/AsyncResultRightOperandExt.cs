using System;
using System.Threading.Tasks;

namespace CSharpFunctionalExtensions
{
    /// <summary>
    ///     Extentions for async operations where the task appears in the right operand only
    /// </summary>
    public static class AsyncResultRightOperandExt
    {
        public static async Task<Result<TResult>> OnSuccess<T, TResult>(
            this Result<T> result, Func<T, Task<TResult>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
            {
                return Result.Fail<TResult>(result.Error);
            }

            TResult value = await func(result.Value).ConfigureAwait(continueOnCapturedContext);

            return Result.Ok(value);
        }

        public static async Task<Result<T>> OnSuccess<T>(
            this Result result, Func<Task<T>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error);
            }

            T value = await func().ConfigureAwait(continueOnCapturedContext);

            return Result.Ok(value);
        }

        public static async Task<Result<TResult>> OnSuccess<T, TResult>(
            this Result<T> result, Func<T, Task<Result<TResult>>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
            {
                return Result.Fail<TResult>(result.Error);
            }

            return await func(result.Value).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<T>> OnSuccess<T>(
            this Result result, Func<Task<Result<T>>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error);
            }

            return await func().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<TResult>> OnSuccess<T, TResult>(
            this Result<T> result, Func<Task<Result<TResult>>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
            {
                return Result.Fail<TResult>(result.Error);
            }

            return await func().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result> OnSuccess<T>(
            this Result<T> result, Func<T, Task<Result>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
            {
                return Result.Fail(result.Error);
            }

            return await func(result.Value).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result> OnSuccess(this Result result, Func<Task<Result>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
                return result;

            return await func().ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<T>> Ensure<T>(
            this Result<T> result, Func<T, Task<bool>> predicate, string errorMessage, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error);
            }

            if (!await predicate(result.Value).ConfigureAwait(continueOnCapturedContext))
            {
                return Result.Fail<T>(errorMessage);
            }

            return Result.Ok(result.Value);
        }

        public static async Task<Result> Ensure(
            this Result result, Func<Task<bool>> predicate, string errorMessage, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
            {
                return Result.Fail(result.Error);
            }

            if (!await predicate().ConfigureAwait(continueOnCapturedContext))
            {
                return Result.Fail(errorMessage);
            }

            return Result.Ok();
        }

        public static async Task<Result<TResult>> Map<T, TResult>(
            this Result<T> result, Func<T, Task<TResult>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
            {
                return Result.Fail<TResult>(result.Error);
            }

            TResult value = await func(result.Value).ConfigureAwait(continueOnCapturedContext);

            return Result.Ok(value);
        }

        public static async Task<Result<T>> Map<T>(
            this Result result, Func<Task<T>> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
            {
                return Result.Fail<T>(result.Error);
            }

            T value = await func().ConfigureAwait(continueOnCapturedContext);

            return Result.Ok(value);
        }

        public static async Task<Result<T>> OnSuccess<T>(
            this Result<T> result, Func<T, Task> action, bool continueOnCapturedContext = true)
        {
            if (result.IsSuccess)
            {
                await action(result.Value).ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<Result> OnSuccess(
            this Result result, Func<Task> action, bool continueOnCapturedContext = true)
        {
            if (result.IsSuccess)
            {
                await action().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<T> OnBoth<T>(
            this Result result, Func<Result, Task<T>> func, bool continueOnCapturedContext = true)
        {
            return await func(result).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<TResult> OnBoth<T, TResult>(
            this Result<T> result, Func<Result<T>, Task<TResult>> func, bool continueOnCapturedContext = true)
        {
            return await func(result).ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task<Result<T>> OnFailure<T>(
            this Result<T> result, Func<Task> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
            {
                await func().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<Result> OnFailure(
            this Result result, Func<Task> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
            {
                await func().ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }

        public static async Task<Result<T>> OnFailure<T>(
            this Result<T> result, Func<string, Task> func, bool continueOnCapturedContext = true)
        {
            if (result.IsFailure)
            {
                await func(result.Error).ConfigureAwait(continueOnCapturedContext);
            }

            return result;
        }
    }
}
