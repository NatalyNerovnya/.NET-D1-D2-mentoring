using System;

namespace BadServise.Exceptions
{
    [Serializable]
    public class GlobalException : Exception
    {
        public GlobalException() : base() { }

        public GlobalException(string message) : base(message) { }

        public GlobalException(string message, Exception inner) : base(message, inner) { }

        protected GlobalException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
