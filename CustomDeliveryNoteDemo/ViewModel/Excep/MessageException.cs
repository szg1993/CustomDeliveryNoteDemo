using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Excep
{
    public class MessageException : Exception
    {
        public MessageException() { }
        public MessageException(string message) : base(message) { }
        public MessageException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public MessageException(string message, Exception innerException) : base(message, innerException) { }
    }
}
