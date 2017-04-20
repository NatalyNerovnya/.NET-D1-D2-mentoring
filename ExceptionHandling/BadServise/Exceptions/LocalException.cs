using System;

namespace BadServise.Exceptions
{
    public class LocalException : Exception
    {
        public LocalException() : base() { }

        public LocalException(string message) : base(message) { }

        public LocalException(string message, Exception inner) : base(message, inner) { }

        protected LocalException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
