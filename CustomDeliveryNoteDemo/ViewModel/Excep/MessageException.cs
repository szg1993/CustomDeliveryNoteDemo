using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Excep
{
    class MessageException : Exception
    {
        #region Ctors

        public MessageException()
        {

        }

        public MessageException(string message) : base(message)
        {

        }

        public MessageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        public MessageException(string message, Exception innerException) : base(message, innerException)
        {

        }

        #endregion
    }
}
