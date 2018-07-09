using System;
using FluentAssertions;
using Xunit;
using static CSharpFunctionalExtensions.F;

namespace CSharpFunctionalExtensions.Tests.OptionTests
{
    public class BasicTests
    {
        [Fact]
        public void Can_create_a_nullable_maybe()
        {
            Option<MyClass> option = null;

            option.IsSome.Should().BeFalse();
            option.IsNone.Should().BeTrue();
        }

        [Fact]
        public void Can_create_a_maybe_none()
        {
            Option<MyClass> option = F.None;

            option.IsSome.Should().BeFalse();
            option.IsNone.Should().BeTrue();
        }

        [Fact]
        public void Nullable_maybe_is_same_as_maybe_none()
        {
            Option<MyClass> nullableOption = null;
            Option<MyClass> optionNone = F.None;

            nullableOption.Should().Be(optionNone);
        }

        [Fact]
        public void Cannot_access_Value_if_none()
        {
            Option<MyClass> option = null;

            Action action = () =>
            {
                MyClass myClass = option.Value;
            };

            action.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void Can_create_a_non_nullable_maybe()
        {
            var instance = new MyClass();

            Option<MyClass> option = instance;

            option.IsSome.Should().BeTrue();
            option.IsNone.Should().BeFalse();
            option.Value.Should().Be(instance);
        }

        [Fact]
        public void ToString_returns_No_Value_if_no_value()
        {
            Option<MyClass> option = null;

            string str = option.ToString();

            str.Should().Be("No value");
        }

        [Fact]
        public void ToString_returns_underlying_objects_string_representation()
        {
            Option<MyClass> option = new MyClass();

            string str = option.ToString();

            str.Should().Be("My custom class");
        }


        private class MyClass
        {
            public override string ToString()
            {
                return "My custom class";
            }
        }
    }
}
