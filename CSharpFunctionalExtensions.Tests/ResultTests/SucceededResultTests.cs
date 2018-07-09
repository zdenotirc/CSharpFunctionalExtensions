using System;
using FluentAssertions;
using Xunit;

using static CSharpFunctionalExtensions.F;
using Unit = System.ValueTuple;

namespace CSharpFunctionalExtensions.Tests.ResultTests
{
    public class SucceededResultTests
    {
        [Fact]
        public void Can_create_a_non_generic_version()
        {
            Result<Unit> result = Success(Unit());

            result.IsFailure.Should().Be(false);
            result.IsSuccess.Should().Be(true);
        }

        [Fact]
        public void Can_create_a_generic_version()
        {
            var myClass = new MyClass();

            Result<MyClass> result = Success(myClass);

            result.IsFailure.Should().Be(false);
            result.IsSuccess.Should().Be(true);
            result.Value.Should().Be(myClass);
        }

        [Fact]
        public void Cannot_create_without_Value()
        {
            Action action = () => { Success((MyClass)null); };

            action.ShouldThrow<ArgumentNullException>();;
        }

        [Fact]
        public void Cannot_access_Error_non_generic_version()
        {
            Result<Unit> result = Success(Unit());

            Action action = () =>
            {
                Error error = result.Error;
            };

            action.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void Cannot_access_Error_generic_version()
        {
            Result<MyClass> result = Success(new MyClass());

            Action action = () =>
            {
                Error error = result.Error;
            };

            action.ShouldThrow<InvalidOperationException>();
        }


        private class MyClass
        {
        }
    }
}
