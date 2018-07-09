using System;


namespace CSharpFunctionalExtensions
{
    public struct Option<T> : IEquatable<None>, IEquatable<Option<T>>
    {
        private readonly T _value;
        public T Value
        {
            get
            {
                if (IsNone)
                {
                    throw new InvalidOperationException();
                }

                return _value;
            }
        }

        // public static Option<T> None => new Option<T>();
        public static implicit operator Option<T>(None none)
        {
            return new Option<T>();
        }

        public static implicit operator Option<T>(Some<T> some)
        {
            return new Option<T>(some.Value);
        }

        public bool IsSome { get; }
        public bool IsNone => !IsSome;

        private Option(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
            
            _value = value;
            IsSome = true;
        }

        public static implicit operator Option<T>(T value)
        {
            return new Option<T>(value);
        }

        public static Option<T> From(T obj)
        {
            return new Option<T>(obj);
        }
        
        public TResult Match<TResult>(Func<TResult> none, Func<T, TResult> some)
        {
            return IsSome ? some(_value) : none();
        }

        public static bool operator ==(Option<T> option, T value)
        {
            if (option.IsNone)
            {
                return false;
            }

            return option.Value.Equals(value);
        }

        public static bool operator !=(Option<T> option, T value)
        {
            return !(option == value);
        }

        public static bool operator ==(Option<T> first, Option<T> second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Option<T> first, Option<T> second)
        {
            return !(first == second);
        }

        public bool Equals(None otherNone)
        {
            return IsNone;
        }

        public override bool Equals(object obj)
        {
            if (obj is T)
            {
                obj = new Option<T>((T)obj);
            }

            if (!(obj is Option<T>))
            {
                return false;
            }

            var other = (Option<T>)obj;
            return Equals(other);
        }

        public bool Equals(Option<T> other)
        {
            if (IsNone && other.IsNone)
            {
                return true;
            }

            if (IsNone || other.IsNone)
            {
                return false;
            }

            return _value.Equals(other._value);
        }

        public override int GetHashCode()
        {
            return IsNone ? 0 : _value.GetHashCode();
        }

        public override string ToString()
        {
            return IsNone ? "No value" : Value.ToString();
        }
    }
    
    public struct None
    {
        internal static readonly None Default = new None();
    }

    public struct Some<T>
    {
        internal T Value { get; }

        internal Some(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), ErrorMessages.ErrorWrapNullInSome);
            }

            Value = value;
        }
    }
}
