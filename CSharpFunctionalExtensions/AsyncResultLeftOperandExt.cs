using System;
using System.Threading.Tasks;
using static CSharpFunctionalExtensions.F;

namespace CSharpFunctionalExtensions
{
    /// <summary>
    /// Extentions for async operations where the task appears in the left operand only
    /// </summary>
    public static class AsyncResultLeftOperandExt
    {
        public static async Task<Result<TResult>> OnSuccess<T, TResult>(
            this Task<Result<T>> resultTask, Func<T, TResult> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }

        /*
        public static async Task<Result<T>> OnSuccess<T>(
            this Task<Result> resultTask, Func<T> func, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }
        */

        public static async Task<Result<TResult>> OnSuccess<T, TResult>(
            this Task<Result<T>> resultTask, Func<T, Result<TResult>> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }

        /*
        public static async Task<Result<T>> OnSuccess<T>(
            this Task<Result> resultTask, Func<Result<T>> func, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }
        */

        public static async Task<Result<TResult>> OnSuccess<T, TResult>(
            this Task<Result<T>> resultTask, Func<Result<TResult>> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }

        /*
        public static async Task<Result> OnSuccess<T>(
            this Task<Result<T>> resultTask, Func<T, Result> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }
        
        public static async Task<Result> Then<T>(
            this Task<Result<T>> resultTask, Func<T, Result> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Then(func);
        }
        
        public static async Task<Result> OnSuccess(
            this Task<Result> resultTask, Func<Result> func, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(func);
        }
        */

        public static async Task<Result<T>> Ensure<T>(
            this Task<Result<T>> resultTask, Func<T, bool> predicate, string errorMessage, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Ensure(predicate, errorMessage);
        }

        /*
        public static async Task<Result> Ensure(
            this Task<Result> resultTask, Func<bool> predicate, string errorMessage, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Ensure(predicate, errorMessage);
        }*/

        public static async Task<Result<TResult>> Map<T, TResult>(
            this Task<Result<T>> resultTask, Func<T, TResult> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Map(func);
        }

        /*
        public static async Task<Result<T>> Map<T>(
            this Task<Result> resultTask, Func<T> func, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.Map(func);
        }*/

        public static async Task<Result<T>> OnSuccess<T>(
            this Task<Result<T>> resultTask, Action<T> action, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(action);
        }
        
        public static async Task<Result<T>> Then<T>(
            this Task<Result<T>> resultTask, Action<T> action, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(action);
        }

        /*
        public static async Task<Result> OnSuccess(
            this Task<Result> resultTask, Action action, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnSuccess(action);
        }

        public static async Task<T> OnBoth<T>(
            this Task<Result> resultTask, Func<Result, T> func, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnBoth(func);
        }
        */
        
        
        public static async Task<TResult> Match<T, TResult>(
            this Task<Result<T>> resultTask, Func<T, TResult> func, Func<Error, TResult> fail, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            return result.Match(func, fail);
        }

        /*
        public static async Task<TResult> OnBoth<T, TResult>(
            this Task<Result<T>> resultTask, Func<Result<T>, TResult> func, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnBoth(func);
        }*/
        
        public static async Task<Result<T>> OnFailure<T>(
            this Task<Result<T>> resultTask, Action action, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnFailure(action);
        }

        /*
        public static async Task<Result> OnFailure(
            this Task<Result> resultTask, Action action, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnFailure(action);
        }*/

        public static async Task<Result<T>> OnFailure<T>(
            this Task<Result<T>> resultTask, Action<Error> action, bool continueOnCapturedContext = true)
        {
            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnFailure(action);
        }

        /*
        public static async Task<Result> OnFailure(
            this Task<Result> resultTask, Action<Error> action, bool continueOnCapturedContext = true)
        {
            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnFailure(action);
        }*/
    }
}
