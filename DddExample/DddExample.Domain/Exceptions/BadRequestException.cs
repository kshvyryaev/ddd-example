using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DddExample.Domain.Exceptions
{
    public class BadRequestException : CustomExceptionBase
    {
        public BadRequestException(string message)
        {
            Response = new[] { new ExceptionResponse { Message = message } };
            Message = message;
        }

        public override int StatusCode => StatusCodes.Status400BadRequest;
        
        public override ICollection<ExceptionResponse> Response { get; }
        
        public override string Message { get; }
    }
}