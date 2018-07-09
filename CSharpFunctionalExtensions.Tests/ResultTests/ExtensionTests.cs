using FluentAssertions;
using Xunit;

using static CSharpFunctionalExtensions.F;
using Unit = System.ValueTuple;

namespace CSharpFunctionalExtensions.Tests.ResultTests
{
    public class ExtensionTests
    {
        private static readonly string _errorMessage = "this failed";

        [Fact]
        public void Should_execute_action_on_failure()
        {
            bool myBool = false;

            var myResult = Failure<Unit>(_errorMessage);
            myResult.OnFailure(() => myBool = true);

            myBool.Should().Be(true);
        }

        [Fact]
        public void Should_execute_action_on_generic_failure()
        {
            bool myBool = false;

            Result<MyClass> myResult = Failure(_errorMessage);
            myResult.OnFailure(() => myBool = true);

            myBool.Should().Be(true);
        }

        [Fact]
        public void Should_exexcute_action_with_result_on_generic_failure()
        {
            Error myError = _errorMessage;

            Result<MyClass> myResult = Failure(myError);
            myResult.OnFailure(error => myError = error);

            myError.Should().Be(myError);
        }

        private class MyClass
        {
            public string Property { get; set; }
        }
    }
}
