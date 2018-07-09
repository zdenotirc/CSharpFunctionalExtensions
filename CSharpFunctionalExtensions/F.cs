using System.Threading.Tasks;
using Unit = System.ValueTuple;

namespace CSharpFunctionalExtensions
{
    public static class F
    {
        public static Unit Unit() => default(Unit);

        public static Error Error(string message) => new Error(message);
        
        public static None None => None.Default;

        public static Result<T> Success<T>(T value) => new Result<T>(value);

        public static Result<Unit> Success() => new Result<Unit>(Unit());

        public static Failure Failure(Error error) => new Failure(error);
        
        public static Result<T> Failure<T>(Error error) => new Failure(error);

        public static Task<T> Async<T>(T t) => Task.FromResult(t);

        public static Result<Unit> FirstFailureOrSuccess<T>(params Result<T>[] results)
        {
            foreach (var result in results)
            {
                if (result.IsFailure)
                {
                    return Failure(result.Error);
                }
            }

            return Success();
        }
    }
}