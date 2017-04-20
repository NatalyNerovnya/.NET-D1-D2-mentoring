using System;

namespace BadServise.Exceptions
{
    [Serializable]
    public class AttributeException : Exception
    {
        public AttributeException() : base() { }

        public AttributeException(string message) : base(message) { }

        public AttributeException(string message, Exception inner) : base(message, inner) { }

        protected AttributeException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
