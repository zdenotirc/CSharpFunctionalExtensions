using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.Tests.OptionTests
{
    public class EqualityTests
    {
        [Fact]
        public void Two_maybes_of_the_same_content_are_equal()
        {
            var instance = new MyClass();
            Option<MyClass> maybe1 = instance;
            Option<MyClass> maybe2 = instance;

            bool equals1 = maybe1.Equals(maybe2);
            bool equals2 = ((object)maybe1).Equals(maybe2);
            bool equals3 = maybe1 == maybe2;
            bool equals4 = maybe1 != maybe2;
            bool equals5 = maybe1.GetHashCode() == maybe2.GetHashCode();

            equals1.Should().BeTrue();
            equals2.Should().BeTrue();
            equals3.Should().BeTrue();
            equals4.Should().BeFalse();
            equals5.Should().BeTrue();
        }

        [Fact]
        public void Two_maybes_are_not_equal_if_differ()
        {
            Option<MyClass> maybe1 = new MyClass();
            Option<MyClass> maybe2 = new MyClass();

            bool equals1 = maybe1.Equals(maybe2);
            bool equals2 = ((object)maybe1).Equals(maybe2);
            bool equals3 = maybe1 == maybe2;
            bool equals4 = maybe1 != maybe2;
            bool equals5 = maybe1.GetHashCode() == maybe2.GetHashCode();

            equals1.Should().BeFalse();
            equals2.Should().BeFalse();
            equals3.Should().BeFalse();
            equals4.Should().BeTrue();
            equals5.Should().BeFalse();
        }

        [Fact]
        public void Two_empty_maybes_are_equal()
        {
            Option<MyClass> maybe1 = null;
            Option<MyClass> maybe2 = null;

            bool equals1 = maybe1.Equals(maybe2);
            bool equals2 = ((object)maybe1).Equals(maybe2);
            bool equals3 = maybe1 == maybe2;
            bool equals4 = maybe1 != maybe2;
            bool equals5 = maybe1.GetHashCode() == maybe2.GetHashCode();

            equals1.Should().BeTrue();
            equals2.Should().BeTrue();
            equals3.Should().BeTrue();
            equals4.Should().BeFalse();
            equals5.Should().BeTrue();
        }

        [Fact]
        public void Two_maybes_are_not_equal_if_one_of_them_empty()
        {
            Option<MyClass> maybe1 = new MyClass();
            Option<MyClass> maybe2 = null;

            bool equals1 = maybe1.Equals(maybe2);
            bool equals2 = ((object)maybe1).Equals(maybe2);
            bool equals3 = maybe1 == maybe2;
            bool equals4 = maybe1 != maybe2;
            bool equals5 = maybe1.GetHashCode() == maybe2.GetHashCode();

            equals1.Should().BeFalse();
            equals2.Should().BeFalse();
            equals3.Should().BeFalse();
            equals4.Should().BeTrue();
            equals5.Should().BeFalse();
        }

        [Fact]
        public void Can_compare_maybe_to_underlying_type()
        {
            var instance = new MyClass();
            Option<MyClass> option = instance;

            bool equals1 = option.Equals(instance);
            bool equals2 = ((object)option).Equals(instance);
            bool equals3 = option == instance;
            bool equals4 = option != instance;
            bool equals5 = option.GetHashCode() == instance.GetHashCode();

            equals1.Should().BeTrue();
            equals2.Should().BeTrue();
            equals3.Should().BeTrue();
            equals4.Should().BeFalse();
            equals5.Should().BeTrue();
        }

        [Fact]
        public void Can_compare_underlying_type_to_maybe()
        {
            var instance = new MyClass();
            Option<MyClass> option = instance;

            bool equals1 = instance == option;
            bool equals2 = instance != option;

            equals1.Should().BeTrue();
            equals2.Should().BeFalse();
        }


        private class MyClass
        {
        }
    }
}
