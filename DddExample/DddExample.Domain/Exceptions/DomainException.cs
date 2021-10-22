using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DddExample.Domain.Exceptions
{
    public class DomainException : CustomExceptionBase
    {
        public DomainException(string message)
        {
            Response = new[] { new ExceptionResponse { Message = message } };
            Message = message;
        }

        public override int StatusCode => StatusCodes.Status400BadRequest;
        
        public override ICollection<ExceptionResponse> Response { get; }
        
        public override string Message { get; }
    }
}