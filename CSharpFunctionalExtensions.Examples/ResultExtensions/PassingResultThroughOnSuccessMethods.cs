using System;
using static CSharpFunctionalExtensions.F;
using Unit = System.ValueTuple;

namespace CSharpFunctionalExtensions.Examples.ResultExtensions
{
    public class PassingResultThroughOnSuccessMethods
    {
        public void Example1()
        {
            Result<DateTime> result = FunctionInt()
                .OnSuccess(x => FunctionString(x))
                .OnSuccess(x => FunctionDateTime(x));
        }

        public void Example2()
        {
            Result<DateTime> result = FunctionInt()
                .OnSuccess(() => FunctionString())
                .OnSuccess(x => FunctionDateTime(x));
        }

        private Result<int> FunctionInt()
        {
            return Success(1);
        }

        private Result<string> FunctionString(int intValue)
        {
            return Success("Ok");
        }

        private Result<string> FunctionString()
        {
            return Success("Ok");
        }

        private Result<DateTime> FunctionDateTime(string stringValue)
        {
            return Success(DateTime.Now);
        }
    }
}
