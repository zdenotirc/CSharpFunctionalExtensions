using FluentAssertions;
using Xunit;
using static CSharpFunctionalExtensions.F;
using Unit = System.ValueTuple;

namespace CSharpFunctionalExtensions.Tests.ResultTests
{
    public class CombineMethodTests
    {
        [Fact]
        public void FirstFailureOrSuccess_returns_the_first_failed_result()
        {
            Error error1 = "Failure 1";
            Error error2 = "Failure 2";

            Result<Unit> result1 = Success(Unit());
            Result<Unit> result2 = Failure(error1);
            Result<Unit> result3 = Failure(error2);

            var result = FirstFailureOrSuccess(result1, result2, result3);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(error1);
        }

        [Fact]
        public void FirstFailureOrSuccess_returns_Ok_if_no_failures()
        {
            var result1 = Success(Unit());
            var result2 = Success(Unit());
            var result3 = Success(Unit());
            var result4 = Success(1);

            var result = FirstFailureOrSuccess(result1, result2, result3);

            result.IsSuccess.Should().BeTrue();
        }
    }
}
