using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace DddExample.Domain.Exceptions
{
    public class ValidationException : CustomExceptionBase
    {
        public ValidationException(ValidationResult result) : this(result.Errors)
        {
        }

        public ValidationException(ICollection<ValidationFailure> failures)
        {
            var response = failures
                .Select(x => new ExceptionResponse
                {
                    Message = x.ErrorMessage,
                    Field = x.PropertyName
                })
                .ToList();

            Response = response;
            Message = GetMessage(response);
        }

        public ValidationException(ICollection<ExceptionResponse> response)
        {
            Response = response;
            Message = GetMessage(response);
        }

        public override int StatusCode => StatusCodes.Status400BadRequest;

        public override ICollection<ExceptionResponse> Response { get; }

        public override string Message { get; }

        private static string GetMessage(ICollection<ExceptionResponse> response)
        {
            var message = string.Join(", ", response.Select(x => $"{x.Field}: {x.Message}"));
            return message;
        }
    }
}