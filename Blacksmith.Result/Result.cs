using Blacksmith.Exceptions;
using Blacksmith.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith
{
    public class Result : AbstractDomain
    {
        protected Result(bool success, IEnumerable<Exception> exceptions) : base()
        {
            if (success)
                this.assert.isNull(exceptions);
            else
                this.assert.isNotNull(exceptions);

            this.Success = success;

            this.Exceptions = (exceptions ?? Enumerable.Empty<Exception>())
                .ToList()
                .AsReadOnly();
        }

        public Result() : this(true, null) { }

        public Result(Exception error, params Exception[] errors) 
            : this(false, new Exception[] { error }.Concat(errors)) { }

        public Result(IEnumerable<Exception> errors) : this(false, errors) { }

        public bool Success { get; }

        public IReadOnlyList<Exception> Exceptions { get; }

        public static implicit operator Result(Exception exception)
        {
            return new Result(exception);
        }
    }

    public class Result<T> : Result
    {
        private readonly T value;

        private Result(bool success, IEnumerable<Exception> exceptions, T value) : base(success, exceptions)
        {
            this.value = value;
        }

        public Result(T value) : this(true, null, value) { }

        public Result(Exception error, params Exception[] errors) 
            : this(false, new Exception[] { error }.Concat(errors), default(T)) { }

        public Result(IEnumerable<Exception> errors) : this(false, errors, default(T)) { }

        public T Value
        {
            get
            {
                base.isTrue<ValueRequestedOnUnsuccessResultException>(this.Success);
                return this.value;
            }
        }

        public static implicit operator Result<T>(T value)
        {
            return new Result<T>(value);
        }

        public static implicit operator Result<T>(Exception exception)
        {
            return new Result<T>(exception);
        }
    }
}
