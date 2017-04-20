using System;

namespace BadServise.Exceptions
{
    [Serializable]
    public class ControllerLevelException : Exception
    {
        public ControllerLevelException() : base() { }

        public ControllerLevelException(string message) : base(message) { }

        public ControllerLevelException(string message, Exception inner) : base(message, inner) { }

        protected ControllerLevelException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
