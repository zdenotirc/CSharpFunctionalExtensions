using System;


namespace CSharpFunctionalExtensions
{
    public struct Option<T> : IEquatable<Option<T>>
    {
        private readonly T _value;
        public T Value
        {
            get
            {
                if (HasNoValue)
                    throw new InvalidOperationException();

                return _value;
            }
        }

        public static Option<T> None => new Option<T>();

        public bool HasValue => _value != null;
        public bool HasNoValue => !HasValue;

        private Option(T value)
        {
            _value = value;
        }

        public static implicit operator Option<T>(T value)
        {
            return new Option<T>(value);
        }

        public static Option<T> From(T obj)
        {
            return new Option<T>(obj);
        }

        public static bool operator ==(Option<T> option, T value)
        {
            if (option.HasNoValue)
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
            if (HasNoValue && other.HasNoValue)
            {
                return true;
            }

            if (HasNoValue || other.HasNoValue)
            {
                return false;
            }

            return _value.Equals(other._value);
        }

        public override int GetHashCode()
        {
            return HasNoValue ? 0 : _value.GetHashCode();
        }

        public override string ToString()
        {
            return HasNoValue ? "No value" : Value.ToString();
        }
    }
}
