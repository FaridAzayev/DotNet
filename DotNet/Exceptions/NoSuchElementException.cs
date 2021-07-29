using System;
using System.Runtime.Serialization;

#nullable enable
namespace DotNet.Exceptions
{
    public class NoSuchElementException : Exception
    {
        public NoSuchElementException()
        {
        }

        protected NoSuchElementException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public NoSuchElementException(string? message) : base(message)
        {
        }

        public NoSuchElementException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}