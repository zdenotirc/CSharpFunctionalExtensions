using System.Threading.Tasks;


namespace CSharpFunctionalExtensions
{
    public static class AsyncOptionExt
    {
        public static async Task<Result<T>> ToResult<T>(
            this Task<Option<T>> optionTask, string errorMessage, bool continueOnCapturedContext = true)
            where T : class
        {
            Option<T> option = await optionTask.ConfigureAwait(continueOnCapturedContext);
            
            return option.ToResult(errorMessage);
        }
    }
}
