using System;
using System.Collections.Generic;

using static CSharpFunctionalExtensions.F;
using Unit = System.ValueTuple;

namespace CSharpFunctionalExtensions
{
    public struct Failure
    {
        internal readonly Error Error;

        public Failure(Error error)
        {
            if (error == null)
            {
                throw new ArgumentNullException(nameof(error), ErrorMessages.ErrorObjectIsNotProvidedForFailure);
            }
                
            if (string.IsNullOrEmpty(error.Message))
            {
                throw new ArgumentNullException(nameof(error), ErrorMessages.ErrorMessageIsNotProvidedForFailure);
            }
            
            Error = error;
        }
    }

    public struct Result<T>
    {
        private readonly T _value;
        private readonly Error _error;

        public bool IsFailure { get; }
        public bool IsSuccess => !IsFailure;
        
        public Error Error
        {
            get
            {
                if (IsSuccess)
                {
                    throw new InvalidOperationException("There is no error message for success.");
                }

                return _error;
            }
        }

        public T Value
        {
            get
            {
                if (!IsSuccess)
                {
                    throw new InvalidOperationException("There is no value for failure.");
                }

                return _value;
            }
        }
        
        internal Result(T value)
            :this(false, value, default(Error))
        {
        }
        
        private Result(Error error)
            :this(true, default(T), error)
        {
        }

        private Result(bool isFailure, T value, Error error)
        {
            if (!isFailure && value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (isFailure)
            {
                if (error == null)
                {
                    throw new ArgumentNullException(nameof(error), ErrorMessages.ErrorObjectIsNotProvidedForFailure);
                }
                
                if (string.IsNullOrEmpty(error.Message))
                {
                    throw new ArgumentNullException(nameof(error), ErrorMessages.ErrorMessageIsNotProvidedForFailure);
                }
            }
            else
            {
                if (error != null)
                {
                    throw new ArgumentException(ErrorMessages.ErrorObjectIsProvidedForSuccess, nameof(error));
                }
            }

            IsFailure = isFailure;
            _error = error;
            _value = value;
        }

        public static implicit operator Result<T>(Error error) 
            => new Result<T>(error);
      
        public static implicit operator Result<T>(Failure failure) 
            => new Result<T>(failure.Error);
      
        public static implicit operator Result<T>(T value) => Success(value);

        public TResult Match<TResult>(Func<Error, TResult> failure, Func<T, TResult> success)
            => IsSuccess ? success(Value) : failure(Error);

        public Unit Match(Action<Error> failure, Action<T> success)
            => Match(failure.ToFunc(), success.ToFunc());

        public IEnumerator<T> AsEnumerable()
        {
            if (IsSuccess) yield return Value;
        }
        
        // the Return function for Result
        public static Func<T, Result<T>> Return = Success;

        public static Result<T> Fail(Error error)
            => new Result<T>(error);
    }
}
