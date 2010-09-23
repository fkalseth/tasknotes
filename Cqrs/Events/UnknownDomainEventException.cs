using System;

namespace Cqrs.Events
{
    public class UnknownDomainEventException : Exception
    {
        public UnknownDomainEventException(string s) : base(s)
        {}
    }
}