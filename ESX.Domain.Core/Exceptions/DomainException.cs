using System;
using System.Runtime.Serialization;

namespace ESX.Domain.Core.Exceptions
{
    [Serializable]
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
        public DomainException() : base() { }
        private DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}