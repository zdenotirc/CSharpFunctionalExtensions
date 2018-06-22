using System;

namespace CSharpFunctionalExtensions
{
    internal static class ResultMessages
    {
        public static readonly string ErrorObjectIsNotProvidedForFailure =
            "You have tried to create a failure result, but error object appeared to be null, please review the code, generating error object.";

        public static readonly string ErrorObjectIsProvidedForSuccess =
            "You have tried to create a success result, but error object was also passed to the constructor, please try to review the code, creating a success result.";
        
        public static readonly string ErrorMessageIsNotProvidedForFailure = "There must be error message for failure.";

        public static readonly string ErrorMessageIsProvidedForSuccess = "There should be no error message for success.";
    }


    public struct Result
    {
        private static readonly Result OkResult = new Result(false, null);
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
        
        private Result(bool isFailure, Error error)
        {
            if (isFailure)
            {
                if (error == null)
                {
                    throw new ArgumentNullException(nameof(error), ResultMessages.ErrorObjectIsNotProvidedForFailure);
                }

                if (string.IsNullOrEmpty(error.Message))
                {
                    throw new ArgumentNullException(nameof(error), ResultMessages.ErrorMessageIsNotProvidedForFailure);
                }

            }
            else
            {
                if (error != null)
                {
                    throw new ArgumentException(ResultMessages.ErrorObjectIsProvidedForSuccess, nameof(error));
                }
            }

            IsFailure = isFailure;
            _error = error;
        }

        public static Result Ok()
        {
            return OkResult;
        }

        public static Result Fail(Error error)
        {
            return new Result(true, error);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(false, value, null);
        }

        public static Result<T> Fail<T>(Error error)
        {
            return new Result<T>(true, default(T), error);
        }

        public static Result FirstFailureOrSuccess(params Result[] results)
        {
            foreach (Result result in results)
            {
                if (result.IsFailure)
                {
                    return Fail(result.Error);
                }
            }

            return Ok();
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

        internal Result(bool isFailure, T value, Error error)
        {
            if (!isFailure && value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (isFailure)
            {
                if (error == null)
                {
                    throw new ArgumentNullException(nameof(error), ResultMessages.ErrorObjectIsNotProvidedForFailure);
                }
                
                if (string.IsNullOrEmpty(error.Message))
                {
                    throw new ArgumentNullException(nameof(error), ResultMessages.ErrorMessageIsNotProvidedForFailure);
                }
            }
            else
            {
                if (error != null)
                {
                    throw new ArgumentException(ResultMessages.ErrorObjectIsProvidedForSuccess, nameof(error));
                }
            }

            IsFailure = isFailure;
            _error = error;
            _value = value;
        }

        public static implicit operator Result(Result<T> result)
        {
            return result.IsSuccess 
                ? Result.Ok() 
                : Result.Fail(result.Error);
        }
    }
}
