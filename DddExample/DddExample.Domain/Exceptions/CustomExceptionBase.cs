using System;
using System.Collections.Generic;

namespace DddExample.Domain.Exceptions
{
    public abstract class CustomExceptionBase : Exception
    {
        public abstract int StatusCode { get; }
        
        public abstract ICollection<ExceptionResponse> Response { get; }
    }
}