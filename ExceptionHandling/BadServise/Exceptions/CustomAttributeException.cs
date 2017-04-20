using System;

namespace BadServise.Exceptions
{
    [Serializable]
    public class CustomAttributeException : Exception
    {
        public CustomAttributeException() : base() { }

        public CustomAttributeException(string message) : base(message) { }

        public CustomAttributeException(string message, Exception inner) : base(message, inner) { }

        protected CustomAttributeException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
