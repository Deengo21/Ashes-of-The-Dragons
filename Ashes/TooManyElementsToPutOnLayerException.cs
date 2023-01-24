using System;
using System.Runtime.Serialization;

namespace Ashes
{
    [Serializable]
    public class TooManyElementsToPutOnLayerException : Exception
    {
        public TooManyElementsToPutOnLayerException()
        {
        }

        public TooManyElementsToPutOnLayerException(string? message) : base(message)
        {
        }

        public TooManyElementsToPutOnLayerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TooManyElementsToPutOnLayerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}