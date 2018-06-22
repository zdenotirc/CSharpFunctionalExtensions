using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.Tests.OptionTests
{
    public class ExtensionsTests
    {
        [Fact]
        public void Unwrap_extracts_value_if_not_null()
        {
            var instance = new MyClass();
            Option<MyClass> option = instance;

            MyClass myClass = option.Unwrap();

            myClass.Should().Be(instance);
        }

        [Fact]
        public void Unwrap_extracts_null_if_no_value()
        {
            Option<MyClass> option = null;

            MyClass myClass = option.Unwrap();

            myClass.Should().BeNull();
        }

        [Fact]
        public void Can_use_selector_in_Unwrap()
        {
            Option<MyClass> option = new MyClass { Property = "Some value" };

            string value = option.Unwrap(x => x.Property);

            value.Should().Be("Some value");
        }

        [Fact]
        public void Can_use_default_value_in_Unwrap()
        {
            Option<string> option = null;

            string value = option.Unwrap("");

            value.Should().Be("");
        }

        [Fact]
        public void ToResult_returns_success_if_value_exists()
        {
            var instance = new MyClass();
            Option<MyClass> option = instance;

            Result<MyClass> result = option.ToResult("Error");

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(instance);
        }

        [Fact]
        public void ToResult_returns_failure_if_no_value()
        {
            Option<MyClass> option = null;
            Error error = "Error";

            Result<MyClass> result = option.ToResult(error);

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(error);
        }

        [Fact]
        public void Where_returns_value_if_predicate_returns_true()
        {
            var instance = new MyClass { Property = "Some value" };
            Option<MyClass> option = instance;

            Option<MyClass> maybe2 = option.Where(x => x.Property == "Some value");

            maybe2.HasValue.Should().BeTrue();
            maybe2.Value.Should().Be(instance);
        }

        [Fact]
        public void Where_returns_no_value_if_predicate_returns_false()
        {
            var instance = new MyClass { Property = "Some value" };
            Option<MyClass> option = instance;

            Option<MyClass> maybe2 = option.Where(x => x.Property == "Different value");

            maybe2.HasValue.Should().BeFalse();
        }

        [Fact]
        public void Where_returns_no_value_if_initial_maybe_is_null()
        {
            Option<MyClass> option = null;

            Option<MyClass> maybe2 = option.Where(x => x.Property == "Some value");

            maybe2.HasValue.Should().BeFalse();
        }

        [Fact]
        public void Select_returns_new_maybe()
        {
            Option<MyClass> option = new MyClass { Property = "Some value" };

            Option<string> maybe2 = option.Select(x => x.Property);

            maybe2.HasValue.Should().BeTrue();
            maybe2.Value.Should().Be("Some value");
        }

        [Fact]
        public void Select_returns_no_value_if_no_value_passed_in()
        {
            Option<MyClass> option = null;

            Option<string> maybe2 = option.Select(x => x.Property);

            maybe2.HasValue.Should().BeFalse();
        }

        [Fact]
        public void Bind_returns_new_maybe()
        {
            Option<MyClass> option = new MyClass { Property = "Some value" };

            Option<string> maybe2 = option.Select(x => GetPropertyIfExists(x));

            maybe2.HasValue.Should().BeTrue();
            maybe2.Value.Should().Be("Some value");
        }

        [Fact]
        public void Bind_returns_no_value_if_internal_method_returns_no_value()
        {
            Option<MyClass> option = new MyClass { Property = null };

            Option<string> maybe2 = option.Select(x => GetPropertyIfExists(x));

            maybe2.HasValue.Should().BeFalse();
        }

        [Fact]
        public void Execute_executes_action()
        {
            string property = null;
            Option<MyClass> option = new MyClass { Property = "Some value" };

            option.Execute(x => property = x.Property);

            property.Should().Be("Some value");
        }

        [Fact]
        public void Unwrap_supports_NET_value_types()
        {
            Option<MyClass> option = new MyClass { IntProperty = 42 };

            int integer = option.Select(x => x.IntProperty).Unwrap();

            integer.Should().Be(42);
        }

        [Fact]
        public void Unwrap_returns_default_for_NET_value_types()
        {
            Option<MyClass> option = null;

            int integer = option.Select(x => x.IntProperty).Unwrap();

            integer.Should().Be(0);
        }


        private static Option<string> GetPropertyIfExists(MyClass myClass)
        {
            return myClass.Property;
        }


        private class MyClass
        {
            public string Property { get; set; }
            public int IntProperty { get; set; }
        }
    }
}
