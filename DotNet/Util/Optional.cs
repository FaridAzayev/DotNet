using System;
using DotNet.Exceptions;

namespace DotNet.Util
{
    public class Optional
    {
        // Returns an Optional describing the given non-null value.
        public static Optional<T> Of<T>(T value) where T : class
        {
            return Optional<T>.Of(value);
        }


        // Returns an Optional describing the given value, if non-null, otherwise returns an empty Optional.
        public static Optional<T> OfNullable<T>(T value) where T : class
        {
            return Optional<T>.OfNullable(value);
        }
    }

    public class Optional<T> where T : class
    {
        public T Value { get; }

        private Optional(T value)
        {
            this.Value = value;
        }


        // If a value is present, returns the value, otherwise throws NoSuchElementException.
        public T Get()
        {
            if (IsEmpty())
            {
                throw new NoSuchElementException();
            }

            return this.Value;
        }


        // Returns an empty Optional instance. No value is present for this Optional.
        public static Optional<T> Empty()
        {
            return new Optional<T>(null);
        }


        //If a value is present, and the value matches the given predicate, returns an Optional describing the value, otherwise returns an empty Optional.
        public Optional<T> Filter(Predicate<T> predicate)
        {
            return IsPresent() && predicate(this.Value) ? this : Empty();
        }


        //If a value is present, performs the given action with the value, otherwise does nothing.
        public void IfPresent(Action<T> action)
        {
            if (this.Value != null)
            {
                action(this.Value);
            }
        }


        //If a value is present, performs the given action with the value, otherwise performs the given empty-based action.
        public void IfPresentOrElse(Action<T> action, Action emptyAction)
        {
            if (this.Value != null)
            {
                action(this.Value);
            }
            else
            {
                emptyAction();
            }
        }


        //If a value is not present, returns true, otherwise false.
        public bool IsEmpty()
        {
            return this.Value == null;
        }


        // If a value is present, returns true, otherwise false.
        public bool IsPresent()
        {
            return this.Value != null;
        }


        // If a value is present, returns an Optional describing (as if by ofNullable(T)) the result of applying the given mapping function to the value, otherwise returns an empty Optional.
        // If the mapping function returns a null result then this method returns an empty Optional.
        public Optional<K> Map<K>(Func<T, K> mapper) where K : class
        {
            return IsEmpty() ? Optional<K>.Empty() : Optional<K>.OfNullable(mapper(this.Value));
        }


        // Returns an Optional describing the given non-null value.
        public static Optional<T> Of(T value)
        {
            return value != null ? new Optional<T>(value) : throw new NullReferenceException();
        }


        // Returns an Optional describing the given value, if non-null, otherwise returns an empty Optional.
        public static Optional<T> OfNullable(T value)
        {
            return value == null ? Empty() : new Optional<T>(value);
        }


        //If a value is present, returns an Optional describing the value, otherwise returns an Optional produced by the supplying function.
        public Optional<T> Or(Func<T> supplier)
        {
            if (IsEmpty())
            {
                return Of(supplier());
            }

            return this;
        }


        //If a value is present, returns the value, otherwise returns other.
        public T OrElse(T other)
        {
            return IsEmpty() ? other : this.Value;
        }


        //If a value is present, returns the value, otherwise returns the result produced by the supplying function
        public T OrElseGet(Func<T> supplier)
        {
            return IsEmpty() ? supplier() : this.Value;
        }


        //If a value is present, returns the value, otherwise throws NoSuchElementException
        public T OrElseThrow()
        {
            if (IsEmpty())
            {
                throw new NoSuchElementException();
            }

            return this.Value;
        }


        //If a value is present, returns the value, otherwise throws exception from exceptionSupplier
        public T OrElseThrow(Func<Exception> exceptionSupplier)
        {
            if (IsEmpty())
            {
                throw exceptionSupplier();
            }

            return this.Value;
        }


        //Returns a non-empty string representation of this Optional suitable for debugging. The exact presentation format is unspecified and may vary between implementations and versions.
        public override string ToString()
        {
            return IsEmpty() ? "Empty" : this.Value.ToString();
        }


        //Returns the hash code of the value, if present, otherwise 0 (zero) if no value is present.
        public int HashCode()
        {
            return IsEmpty() ? 0 : this.Value.GetHashCode();
        }
    }
}