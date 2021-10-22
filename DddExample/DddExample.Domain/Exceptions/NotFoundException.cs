using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DddExample.Domain.Exceptions
{
    public class NotFoundException : CustomExceptionBase
    {
        public NotFoundException(string entityName, int entityId)
        {
            var message = $"Не удалось найти {entityName} с id = {entityId}";
            Response = new[] { new ExceptionResponse { Message = message } };
            Message = message;
        }
        
        public override int StatusCode => StatusCodes.Status404NotFound;
        
        public override ICollection<ExceptionResponse> Response { get; }
        
        public override string Message { get; }
    }
}