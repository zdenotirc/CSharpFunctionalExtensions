using System;
using FluentAssertions;
using Xunit;

using static CSharpFunctionalExtensions.F;
using Unit = System.ValueTuple;

namespace CSharpFunctionalExtensions.Tests.ResultTests
{
    public class FailedResultTests
    {
        [Fact]
        public void Can_create_a_non_generic_version()
        {
            Error error = "Error message";
            Result<Unit> result = Failure(error);

            result.Error.Should().Be(error);
            result.IsFailure.Should().Be(true);
            result.IsSuccess.Should().Be(false);
        }

        [Fact]
        public void Can_create_a_generic_version()
        {
            Error error = "Error message";
            Result<MyClass> result = Failure(error);

            result.Error.Should().Be(error);
            result.IsFailure.Should().Be(true);
            result.IsSuccess.Should().Be(false);
        }

        [Fact]
        public void Cannot_access_Value_property()
        {
            Result<MyClass> result = Failure("Error message");

            Action action = () => { MyClass myClass = result.Value; };

            action.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void Cannot_create_without_error_message()
        {
            Action action1 = () => { Failure(null); };
            Action action2 = () => { Failure(string.Empty); };
            Action action3 = () => { Failure<MyClass>(null); };
            Action action4 = () => { Failure<MyClass>(string.Empty); };

            action1.ShouldThrow<ArgumentNullException>();
            action2.ShouldThrow<ArgumentNullException>();
            action3.ShouldThrow<ArgumentNullException>();
            action4.ShouldThrow<ArgumentNullException>();
        }


        private class MyClass
        {
        }
    }
}
